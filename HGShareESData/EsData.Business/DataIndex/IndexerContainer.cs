using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using EsData.Configs;
using EsData.Utils.Log;

namespace EsData.Business.DataIndex
{
    /// <summary>
    /// 
    /// </summary>
    public class IndexerContainer
    {
        private readonly ILog _log;
        private static IContainer _iocContainer;
        public IndexerContainer(ILog log)
        {
            _log = log;
            IndexConfigs indexConfigs=IndexConfigHelper.IndexConfigs;
            if (_iocContainer == null && indexConfigs != null && indexConfigs.Count>0)
            {
                var builder = new ContainerBuilder();
                //已加载
                //var fromTypes = AppDomain.CurrentDomain.GetAssemblies()
                //    .SelectMany(a => a.GetTypes());
                //注册文件
                var assemblys = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "DevelopmentLayer\\")
               .GetFiles("*.dll").Select(r => Assembly.LoadFrom(r.FullName)).ToArray();

                List<Type> types=new List<Type>();
                foreach (var assembly in assemblys)
                {
                    types.AddRange(assembly.GetTypes());
                }
                var fromTypes = types;

                //注入
                foreach (var indexConfig in indexConfigs)
                {
                    var config = (IndexConfig)indexConfig;
                    var fromType = fromTypes.FirstOrDefault(n => n.FullName == config.TypeName);
                    if (fromType == null)
                    {
                        _log.Error(config.IndexType+" 的实现"+config.TypeName+" 未找到！");
                        continue;
                    }
                    builder
                    .RegisterType(fromType)
                    .As<IIndexer>()
                    .Named<IIndexer>(config.IndexType.ToLower());
                }
                _iocContainer = builder.Build();
            }
        }
        /// <summary>
        /// 需要分页索引器
        /// </summary>
        /// <param name="indexType"></param>
        /// <param name="pagesize"></param>
        /// <param name="es"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public  IIndexer ResolveIndexer(string indexType, int pagesize, IEsHandle es)
        {

            if(!_iocContainer.IsRegisteredWithName<IIndexer>(indexType.ToLower()))
                throw new Exception(indexType+"的实现为注册！");

            return _iocContainer.ResolveNamed<IIndexer>(indexType.ToLower(), new Parameter[]
            {
                new TypedParameter(typeof(int),pagesize),
                new TypedParameter(typeof(IEsHandle),es), 
                new TypedParameter(typeof(ILog),_log)
            });
        }
    }
}

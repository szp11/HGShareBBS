using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using EsData.Business.DataSync.Synchronizer;
using EsData.Configs;
using EsData.Core;
using EsData.Utils.Log;

namespace EsData.Business.DataSync
{
    public class SynchronizerContainer
    {
        private ILog _log;
        private static IContainer _iocContainer;
        public SynchronizerContainer(ILog log)
        {
            _log = log;
            IndexSyncConfigs syncConfigs = IndexSyncConfigHelper.IndexSyncConfigs;

            if (_iocContainer == null && syncConfigs!=null && syncConfigs.Count>0)
            {
                var builder = new ContainerBuilder();

                //var fromTypes = AppDomain.CurrentDomain.GetAssemblies()
                //    .SelectMany(a => a.GetTypes());
                //注册文件
                var assemblys = new DirectoryInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "DevelopmentLayer\\")
               .GetFiles("*.dll").Select(r => Assembly.LoadFrom(r.FullName)).ToArray();

                List<Type> types = new List<Type>();
                foreach (var assembly in assemblys)
                {
                    types.AddRange(assembly.GetTypes());
                }
                var fromTypes = types;
                //注入
                foreach (var indexSyncConfig in syncConfigs)
                {
                    var config = (IndexSyncConfig)indexSyncConfig;

                    var fromType = fromTypes.FirstOrDefault(n => n.FullName == config.TypeName);
                    if (fromType == null)
                    {
                        _log.Error(config.DbConnectionKey + config.TableName + config.IndexName + " 的实现" + config.TypeName + " 未找到！");
                        continue;
                    }
                    builder
                    .RegisterType(fromType)
                    .As<ISynchronizer>()
                    .Named<ISynchronizer>((config.DbConnectionKey + config.TableName+config.IndexName).ToLower());
                }

                _iocContainer = builder.Build();
            }
        }

        /// <summary>
        /// 同步器
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="indexName"></param>
        /// <returns></returns>
        public ISynchronizer ResolveSynchronizer(DataChangeMsg msg,string indexName)
        {
            string name = (msg.DbConnectionKey + msg.TableName + indexName).ToLower();

            if(_iocContainer.IsRegisteredWithName(name,typeof(ISynchronizer)))
                return _iocContainer.ResolveNamed<ISynchronizer>(name);
            else
            {
                return new DefaultSynchronizer();
            }
        }

    }
}

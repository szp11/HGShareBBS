using System;
using HGShare.Container;
using HGShare.Log;

namespace HGShare.Backstage
{
    public class IocContainer
    {
        private static readonly IContainer Container = new AutofacAdapter();
        /// <summary>
        /// 注册实现
        /// </summary>
        public static void RegisterServices()
        {
            Container
                .Register(_ => Container)
                .Register<ILog, Log4Net>();
        }

        #region logservice
        /// <summary>
        /// log实现
        /// </summary>
        /// <returns></returns>
        public static ILog LogService()
        {
            return Container.Resolve<ILog>("configName", "Logger");
        }
        #endregion
    }
}
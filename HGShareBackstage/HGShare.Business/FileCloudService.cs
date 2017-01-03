using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Com.FileCloud;
using HGShare.Com.Interface;
using HGShare.Container;

namespace HGShare.Business
{
    /// <summary>
    /// 文件云存储
    /// </summary>
    public class FileCloudService
    {
        static FileCloudService()
        {
            RegisterServices();
        }

        private static readonly IContainer Container = new AutofacAdapter();
        /// <summary>
        /// 注册实现
        /// </summary>
        public static void RegisterServices()
        {
            Container
                .Register(_ => Container)
                .Register<IFileCloud, QiNiu>();
        }

        #region FileCloud
        /// <summary>
        /// FileCloud实现
        /// </summary>
        /// <returns></returns>
        public static IFileCloud GetFileCloudService()
        {
            return Container.Resolve<IFileCloud>();
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.Common;

namespace HGShare.Site.Config
{
    /// <summary>
    /// 邮件使用模板配置
    /// </summary>
    public class EmailTemplateConfig
    {
        /// <summary>
        /// 邮箱激活模板id
        /// </summary>
        public static int EmailActivateTemplate
        {
            get { return Configuration.AppSettingsToInt("EmailActivateTemplate"); }
        }
        /// <summary>
        /// 找回密码邮件模板id
        /// </summary>
        public static int RetrievePasswordEmailTemplate
        {
            get { return Configuration.AppSettingsToInt("RetrievePasswordEmailTemplate"); }
        }
    }
}

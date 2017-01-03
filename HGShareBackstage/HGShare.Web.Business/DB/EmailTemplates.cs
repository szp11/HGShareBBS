using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.Web.Business.DB
{
    public class EmailTemplates:IEmailTemplates
    {
        /// <summary>
        /// 根据id获取邮件模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmailTemplateVModel GeEmailTemplate(int id)
        {
            return
                HGShare.Business.EmailTemplates.EmailTemplateInfoToVModel(
                    HGShare.Business.EmailTemplates.GetEmailTemplateInfo(id));
        }
    }
}

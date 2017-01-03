using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HGShare.Web.Interface
{
    public interface IEmailTemplates
    {
        /// <summary>
        /// 根据id获取邮件模板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        VWModel.EmailTemplateVModel GeEmailTemplate(int id);

    }
}

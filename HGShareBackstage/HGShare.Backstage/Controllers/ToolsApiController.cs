using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HGShare.Backstage.Controllers.Base;
using HGShare.Common;
using HGShare.Site.ActionResult;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 工具接口
    /// </summary>
    [Description("工具接口")]
    [RoleAuthorizeIgnore]
    public class ToolsApiController : BaseController
    {
        /// <summary>
        /// 汉字转拼音
        /// </summary>
        /// <param name="value">汉字</param>
        /// <returns>拼音</returns>
        [Description("汉字转拼音")]
        public JsonResult ConvertToPinYin(string value)
        {
            var result = new JsonResultModel<string> {ResultState = true};
            if (string.IsNullOrEmpty(value))
                return Json(result);
            value = Server.UrlDecode(value);
            result.Body = PinYinHelper.ConvertToPinYin(value);
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Net;
using System.Web.Mvc;
using HGShare.Enums;
using HGShare.Site.ActionResult;
using HGShare.VWModel;

namespace HGShare.Backstage.Controllers.Base
{
    public class BaseToolsController : Controller
    {
         /// <summary>
         /// 自适应返回（针对 Ajax返回 json）
         /// </summary>
         /// <param name="msg"></param>
         /// <param name="status"></param>
         /// <param name="url"></param>
         /// <returns></returns>
        public ActionResult AutoResult(string msg, bool status, string url="")
        {

            if (Request.IsAjaxRequest()) //ajax
                return new JsonResult
                {
                    Data = new JsonResultModel
                    {
                        Message = msg,
                        ResultState = status
                    }
                };
            if (string.IsNullOrEmpty(url))
                url = 
                    Url.Action("Tips","Tips") 
                    +"?"
                    +( new TipsVModel
                    {
                        type = TipsType.Wran,
                        msg = WebUtility.UrlEncode(msg),
                        state = status
                    }.UrlParameters);
            return new RedirectResult(url,false);
        }

        /// <summary>
        /// 是否无需校验权限
        /// </summary>
        /// <returns></returns>
        public static bool IsRoleAuthorizeIgnore(ControllerBase controller, ActionDescriptor actionDescriptor)
        {
            var noAuthorizeAttributesC = GetAttribute<RoleAuthorizeIgnore>(controller);
            if (noAuthorizeAttributesC != null)
                return true;
            var noAuthorizeAttributes = GetAttribute<RoleAuthorizeIgnore>(actionDescriptor);
            if (noAuthorizeAttributes.Length > 0)
                return true;
            return false;
        }
        /// <summary>
        /// 是否是系统自动请求
        /// </summary>
        /// <returns></returns>
        public static bool IsAutoRequestIgnore(ControllerBase controller, ActionDescriptor actionDescriptor)
        {
            var noAuthorizeAttributesC = GetAttribute<AutoRequestIgnore>(controller);
            if (noAuthorizeAttributesC != null)
                return true;
            var noAuthorizeAttributes = GetAttribute<AutoRequestIgnore>(actionDescriptor);
            if (noAuthorizeAttributes.Length > 0)
                return true;
            return false;
        }

        #region 特性获取
        /// <summary>
        /// 得到控制器自定义特性
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        private static Attribute GetAttribute<T>(ControllerBase controller)
        {
            return Attribute.GetCustomAttribute(controller.GetType(), typeof(T));
        }

        /// <summary>
        /// 得到ActionDescriptor自定义特性
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        private static object[] GetAttribute<T>(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetCustomAttributes(typeof(T), false);
        }
        #endregion
    }

    #region 特性
    /// <summary>
    /// 无需校验权限
    /// </summary>
    public class RoleAuthorizeIgnore : Attribute
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// 系统自动请求
    /// </summary>
    public class AutoRequestIgnore : Attribute
    {
        public string Name { get; set; }
    }
    #endregion
}
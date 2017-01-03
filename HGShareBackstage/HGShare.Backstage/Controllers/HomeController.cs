using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.Model;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 系统主页
    /// </summary>
    [RoleAuthorizeIgnore]
    [Description("系统框架")]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [Description("首页")]
        public ActionResult Index()
        {
            
            ViewBag.UserName = string.IsNullOrEmpty(CurrentUserInfo.NickName)?CurrentUserInfo.Name:CurrentUserInfo.NickName;
            return View();
        }
        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [Description("主页")]
        public ActionResult Main()
        {
            return View();
        }
        /// <summary>
        /// 全局ICO图标显示页
        /// </summary>
        /// <returns></returns>
        [Description("全局ICO图标显示页")]
        public ActionResult Icos()
        {
            return View();
        }
    }
}
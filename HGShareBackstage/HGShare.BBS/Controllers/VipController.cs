using System.ComponentModel;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.Utils.Interface;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.BBS.Controllers
{
    [Description("Vip出入口")]
    public class VipController : BaseController
    {
        private static readonly ILogin UsersLogin = IcoReader.Service<ILogin>();
        private static readonly IVip Vip = IcoReader.Service<IVip>();
        private static readonly IVerifyCode VerCode = IcoReader.Service<IVerifyCode>();
        /// <summary>
        /// 登陆页
        /// </summary>
        /// <param name="backUrl"></param>
        /// <returns></returns>
        [Description("Vip登陆页")]
        [HttpGet]
        public ActionResult Login(string backUrl)
        {
            string refurl = Url.Action("Index", "Home");
            if (!string.IsNullOrEmpty(backUrl))
            {
                refurl = backUrl;
            }
            else if (Request.UrlReferrer != null)
            {
                refurl = Request.UrlReferrer.ToString();
            }
            #region 登陆后的重定向一些特殊规则
            //找回密码页面
            if (refurl.IndexOf(Url.Action("forgetpassword", "user")) > -1)
            {
                refurl = Url.Action("Index", "Home");
            }
            if (refurl.IndexOf(Url.Action("reg", "vip")) > -1)
            {
                refurl = Url.Action("Index", "Home");
            }
            #endregion

            ViewBag.BackUrl = refurl;



            if (UsersLogin.CheckUserIsLoginAndGetUserInfo()!=null)
                return new RedirectResult(refurl);
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="backUrl"></param>
        /// <param name="vercode"></param>
        /// <returns></returns>
        [Description("Vip登陆")]
        [HttpPost]
        public JsonResult Login(string userName, string password, string backUrl, string vercode)
        {
            if (string.IsNullOrEmpty(vercode))
                return Json(new JsonResultModel {Message = "请输入验证码！"});
            if (!VerCode.CheckVerifyCode(vercode))
                return Json(new JsonResultModel{Message = "验证码错误，请重新输入！"});
            var user=UsersLogin.LoginByUserName(userName, password);
            if (user == null || user.IsNull)
                return Json(new JsonResultModel { Message = "用户名或密码错误！"});
            if (string.IsNullOrEmpty(backUrl))
                backUrl = Url.Action("Index", "Home");

            return Json(new JsonResultModel { Message = "登陆成功", ResultState = true, Action = backUrl });
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="backUrl"></param>
        /// <returns></returns>
        [Description("Vip注册页")]
        [HttpGet]
        public ActionResult Reg(string backUrl)
        {
            string refurl = Url.Action("Index", "Home");
            if (!string.IsNullOrEmpty(backUrl))
            {
                refurl = backUrl;
            }
            else if (Request.UrlReferrer != null)
            {
                refurl = Request.UrlReferrer.ToString();
            }
            ViewBag.BackUrl = refurl;
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <param name="vercode"></param>
        /// <returns></returns>
        [Description("Vip注册")]
        [HttpPost]
        public JsonResult Reg(UserVModel model, string vercode)
        {
            if (string.IsNullOrEmpty(vercode))
                return Json(new JsonResultModel { Message = "请输入验证码！" });
            if (!VerCode.CheckVerifyCode(vercode))
                return Json(new JsonResultModel { Message = "验证码错误，请重新输入！" });
            if (Vip.UserNameIsHave(model.Name, null))
                return Json(new JsonResultModel { Message = "用户名已存在"});
            if (Vip.NickNameIsHave(model.NickName, null))
                return Json(new JsonResultModel {Message = "昵称已存在"});
            if (Vip.EmailIsHave(model.Email, null))
                return Json(new JsonResultModel {Message = "邮箱已存在"});
            if (!ModelState.IsValid)
                return Json(new JsonResultModel {Message = ModelStateHelper.GetAllErrorMessage(ModelState) });
            bool status= Vip.AddUserInfo(model);
            if (status)
            {
                string refurl = Url.Action("Login", "Vip");
                if (Request["backUrl"]!=null)
                    refurl += "?backUrl=" + Request["backUrl"];
                return Json(new JsonResultModel {ResultState = true, Action = refurl,Message = "注册成功,快去登陆吧!"});
            }

            return Json(new JsonResultModel { Message = "注册失败"});
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [Description("Vip登出")]
        public ActionResult LogOut()
        {
            UsersLogin.LogOut();
            string refurl = Url.Action("Index", "Home");
            if (Request.UrlReferrer != null)
            {
                refurl = Request.UrlReferrer.ToString();
            }
            #region 退出登陆后的重定向一些特殊规则
            //找回密码页面
            if (refurl.IndexOf(Url.Action("forgetpassword", "user")) > -1)
            {
                refurl = Url.Action("Login", "Vip");
            }
            #endregion

            return new RedirectResult(refurl);
        }
        
        /// <summary>
        /// 定时刷新登陆用户缓存信息
        /// </summary>
        /// <returns></returns>
        [Description("定时刷新登陆用户缓存信息")]
        [HttpPost]
        [AutoRequest]
        public EmptyResult RefreshUserInfo()
        {
            if(UserIsLogin)
                UsersLogin.RefreshCookieUserInfo(CurrentUserInfo.Id);
            return new EmptyResult();
        }
    }
}
using System.ComponentModel;
using System.Threading;
using System.Web.Mvc;
using HGShare.Business;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.VWModel;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 登陆
    /// </summary>
    [Description("登陆")]
    public class LoginController : Controller
    {
        // GET: Login
        [Description("登陆界面")]
        public ActionResult Index()
        {
            if (Users.CheckUserIsLogin())
                return Redirect(Url.Action("Index", "Home"));
            var model = new LoginVModel();
            #if DEBUG
            model.Username = "admin";
            model.Password = "000000";
            #endif


            return View(model);
        }
        [HttpPost]
        [Description("登陆")]
        public JsonResult Login(LoginVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                if (Users.IsHaveUser(model.Username))
                {
                    var userInfo = Users.GetUserInfo(model.Username, model.Password);
                    if (userInfo != null)
                    {
                        Users.Login(userInfo);
                        Users.UpdateOnLineTime(userInfo.Id);
                        result.ResultState = true;
                    }
                    else
                    {
                        result.ResultState = false;
                        result.Message = "用户名或密码错误！";
                    }
                }
                else
                {
                    result.ResultState = false;
                    result.Message = "用户不存在！";
                }
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        /// <summary>
        /// 退出登陆
        /// </summary>
        /// <returns></returns>
        [Description("登出")]
        public ActionResult LogOut()
        {
            Users.LogOut();
            return RedirectToAction("Index");
        }
    }
}
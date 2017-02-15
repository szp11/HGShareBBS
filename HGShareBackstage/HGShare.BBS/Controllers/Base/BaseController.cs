using System.Globalization;
using System.Web.Mvc;
using HGShare.Site;
using HGShare.Site.Config;
using HGShare.Utils.Interface;
using HGShare.VWModel;
using HGShare.Web.Interface;
using HGShare.Web.ServiceManager;

namespace HGShare.BBS.Controllers.Base
{
    public class BaseController : BaseToolsController
    {
        private static readonly ILogin UsersLogin = IcoReader.Service<ILogin>();
        private static readonly IUsers Users = IcoReader.Service<IUsers>();
        private static readonly IUserAccessLogsPublic UserAccessLogsPublic = IcoReader.Service<IUserAccessLogsPublic>();
        public static ILog Log = IcoReader.Service<ILog>("configName", "Logger");
        /// <summary>
        /// 当前登录用户的cookie存储信息
        /// </summary>
        public UserVModel CurrentUserInfo;
        /// <summary>
        /// 用户是否已经登陆
        /// </summary>
        public bool UserIsLogin = false;

        #region 需要的验证判断
        /// <summary>
        /// 操作所需验证条件
        /// </summary>
        public RequestRoleAuthorize RequestRoleAuthorize { get; private set; }
        #endregion

        /// <summary>
        /// 用户检测
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //提取所有验证
            RequestRoleAuthorize = new RequestRoleAuthorize(filterContext.Controller, filterContext.ActionDescriptor);
            //cookie 中json 信息
            CurrentUserInfo = UsersLogin.CheckUserIsLoginAndGetUserInfo();
            //cookie 中校验过的userid
            string userId = EncryptCookies.GetValue(CookieConfig.UserIdCkName);
            //正常登陆
            if (CurrentUserInfo != null && CurrentUserInfo.Id.ToString(CultureInfo.InvariantCulture)==userId)
            {
                UserIsLogin = true;
                RequestRoleAuthorize = new RequestRoleAuthorize(filterContext.Controller, filterContext.ActionDescriptor);
                if (RequestRoleAuthorize.HaveVerification)
                {
                    #region 用户信息
                    var user = Users.GetUserById(CurrentUserInfo.Id);
                    if (user == null || user.IsNull)
                    {
                        UsersLogin.LogOut();
                        const string msg = "您的账户已不存在,如有疑问请联系管理员!";
                        filterContext.Result = AutoResult(msg, false, Url.Action("Warning", "Tips", new { msg }));
                    }
                    #endregion

                    #region 校验禁用状态
                    if (RequestRoleAuthorize.IsDisableVerification && user.Disable)
                    {
                        const string msg = "您的账户已被禁用,如有疑问请联系管理员!";
                        filterContext.Result = AutoResult(msg, false, Url.Action("Warning", "Tips", new { msg }));
                    }

                    #endregion
                    
                    #region 需要邮箱激活验证
                    if (RequestRoleAuthorize.IsEmailActivatedVerification && !user.EmailStatus)
                    {
                        const string msg = "请激活邮箱后再进行操作,如有疑问请联系管理员!";
                        filterContext.Result = AutoResult(msg, false, Url.Action("activate", "user"),true);
                    }
                    #endregion
                }
            }
            else if (RequestRoleAuthorize.HaveVerification)//未登录时 如果需要权限判断 则跳出
            {
                UsersLogin.LogOut();//无法得到用户信息就登出清空原有cookie再次登陆
                //需要验证
                filterContext.Result = AutoResult("请重新登陆后操作！", false, Url.Action("Login", "Vip"));
            }
            #region 全局数据
            ViewData["CurrentPartUserInfo"] = CurrentUserInfo;
            ViewData["WebName"] = WebSysConfig.WebName;
            ViewData["IsLogin"] = UserIsLogin;
            #endregion
        }
        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ////系统请求不记录
            //if (RequestRoleAuthorize == null || RequestRoleAuthorize.IsAutoRequest)
            //    return;
            ////用户请求日志
            //UserAccessLogsPublic.AddAsync(new UserAccessLogVModel
            //{
            //    Ip = Common.Fetch.Ip,
            //    Url = Request.Url==null?null:Request.Url.ToString(),
            //    Referer = Request.UrlReferrer == null ? null : Request.UrlReferrer.ToString(),
            //    Type = 0,
            //    UserAgent = Request.UserAgent,
            //    UserId = CurrentUserInfo == null ? 0 : CurrentUserInfo.Id
            //});
        }
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
        }
        #region 异常处理
        /// <summary>
        /// 执行异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            //异常
            string exception = "错误：" + filterContext.Exception.Message;
            if (Request.Url != null) exception += "\r\nURL：" + Request.Url;
            exception += "\r\n设备：" + Request.UserAgent;
            exception += "\r\n用户IP地址：" + Request.UserHostAddress;
            exception += "\r\n堆栈跟踪信息：" + filterContext.Exception.StackTrace;
            Log.Error(exception);
            const string msg = "诶哟，把我累坏了，稍后再试吧！";
            filterContext.ExceptionHandled = true;
            filterContext.Result = AutoResult(msg, false, Url.Action("Warning", "Tips", new { msg }));
        }
        #endregion
    }
}
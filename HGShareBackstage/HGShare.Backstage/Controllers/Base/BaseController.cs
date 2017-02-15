using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HGShare.Business;
using HGShare.Log;
using HGShare.Model;

namespace HGShare.Backstage.Controllers.Base
{
    public class BaseController : LogBaseController
    {
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        public UserInfo CurrentUserInfo;

        /// <summary>
        /// 当前登陆用户所有权限集合
        /// </summary>
        public List<ModulInfo> CurrentUserModuls;

        #region 用户信息检测

        /// <summary>
        /// 检测用户信息
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //检测登陆状态
            var userInfo = Users.GetCurrentLoginUserInfo();
            //log
#pragma warning disable 4014
            AddActionLogAsync(filterContext, userInfo);
#pragma warning restore 4014
            if (userInfo == null)
            {
                Users.LogOut();//无法得到用户信息就登出清空原有cookie再次登陆
                filterContext.Result =AutoResult("登陆失败请重新登陆！",false,Url.Action("index","login"));return;
            }
            CurrentUserInfo = userInfo;
            //用户角色
            var roleInfo = Roles.GetRoleInfo(CurrentUserInfo.RoleId);
            if (roleInfo == null)
            {
                filterContext.Result =AutoResult("角色不存在！", false); 
                return;
            }
            //所有权限
            CurrentUserModuls = roleInfo.IsSuper ? Moduls.GetAllModul() : Moduls.GetIsShowDisplayListByRoleId(CurrentUserInfo.RoleId);
            //装载全局数据
            InitViewData(CurrentUserModuls, CurrentUserInfo);
            //校验权限 排除无需校验权限请求
            if (IsRoleAuthorizeIgnore(filterContext.Controller, filterContext.ActionDescriptor))
                return;
            //超级角色
            if (roleInfo.IsSuper)
                return;
            //开始校验 权限
            if (CurrentUserInfo == null && !roleInfo.IsSuper)
            {
                filterContext.Result =AutoResult( "无任何权限！", false); 
                return;
            }
            
            string actionName = filterContext.RouteData.GetRequiredString("action");
            string controllerName = filterContext.RouteData.GetRequiredString("controller");
            if (
                !CurrentUserModuls.Any(
                    n =>
                        !string.IsNullOrEmpty(n.Controller) &&
                        !string.IsNullOrEmpty(n.Action) &&
                        n.Controller.ToLower() == controllerName.ToLower() &&
                        n.Action.ToLower() == actionName.ToLower()
                    ))
            {
                filterContext.Result = AutoResult("您没有权限！", false); 
            }
        }

        #endregion
        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
#pragma warning disable 4014
            UpdateUserLoginAndActionTimeAsync(filterContext: filterContext, userInfo: CurrentUserInfo);
#pragma warning restore 4014
        }

        /// <summary>
        /// 装载数据
        /// </summary>
        private void InitViewData(List<ModulInfo> currentUserModuls, UserInfo currentUserInfo)
        {
            if (currentUserModuls != null)
                ViewData["CurrentUserModuls"] = currentUserModuls;
            if (currentUserInfo != null)
                ViewData["CurrentUserInfo"] = currentUserInfo;
        }

        
    }

}
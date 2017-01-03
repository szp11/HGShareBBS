using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Core;
using HGShare.Business;
using HGShare.Log;
using HGShare.Model;
using Newtonsoft.Json;

namespace HGShare.Backstage.Controllers.Base
{
    public class LogBaseController : BaseToolsController
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public static ILog Log = IocContainer.LogService();

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
            filterContext.ExceptionHandled = true;
            filterContext.Result = AutoResult("诶哟，我不小心崩溃了！", false);
        }
        #endregion
        /// <summary>
        /// 记录访问日志
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="userInfo"></param>
        protected static async Task AddActionLogAsync(ActionExecutingContext filterContext, UserInfo userInfo)
        {
            await Task.Run(()=>
            {
                //忽略系统请求
                if (!IsAutoRequestIgnore(filterContext.Controller, filterContext.ActionDescriptor))
                {
                    var loginfo = InitAdminLogInfo(filterContext, userInfo);
                    Log.Info(JsonConvert.SerializeObject(loginfo));
                    //插入log
                    AdminLogs.AddAdminLog(loginfo);
                }
            });
        }
        /// <summary>
        /// 初始化Log信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        private static AdminLogInfo InitAdminLogInfo(ActionExecutingContext filterContext, UserInfo userInfo)
        {
            string actionName = filterContext.RouteData.GetRequiredString("action");
            string controllerName = filterContext.RouteData.GetRequiredString("controller");
            string actionId = string.Empty;
            var actionparamenters = filterContext.ActionParameters;

            var actionIdEntyy = actionparamenters.Where(n => n.Key.ToLower() == "id").ToList();
            if (actionIdEntyy.Any())
            {
                actionId = actionIdEntyy[0].Value == null ? "" : actionIdEntyy[0].Value.ToString();
            }
            var parameters = Newtonsoft.Json.JsonConvert.SerializeObject(actionparamenters);
            string method = "get";
            if (filterContext.HttpContext.Request.HttpMethod.Equals("post", StringComparison.OrdinalIgnoreCase))
            {
                method = "post";
            }
            string url = filterContext.HttpContext.Request.Url == null ? "" : filterContext.HttpContext.Request.Url.ToString();

            //操作描述
            var cdsc = (DescriptionAttribute)Attribute.GetCustomAttribute(filterContext.Controller.GetType(), typeof(DescriptionAttribute));
            var atts = filterContext.ActionDescriptor.GetCustomAttributes(typeof(DescriptionAttribute), false);
            DescriptionAttribute adsc = null;
            if (atts.Length > 0)
                adsc = ((DescriptionAttribute)atts[0]);

            //插入log
            return new AdminLogInfo
            {
                UserId = userInfo == null ? 0 : userInfo.Id,
                Controllers = controllerName,
                Action = actionName,
                InTime = DateTime.Now,
                Url = url,
                Ip = filterContext.HttpContext.Request.UserHostAddress,
                Parameter = parameters,
                ActionId = actionId,
                Method = method,
                IsAjax = filterContext.HttpContext.Request.IsAjaxRequest(),
                UserAgent = filterContext.HttpContext.Request.UserAgent,
                ControllersDsc = cdsc == null ? null : cdsc.Description,
                ActionDsc = adsc == null ? null : adsc.Description,
            };
        }

        /// <summary>
        /// 更新用户的在线时间和最后操作时间
        /// </summary>
        /// <param name="filterContext"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static async Task UpdateUserLoginAndActionTimeAsync(ActionExecutedContext filterContext, UserInfo userInfo)
        {
            if (userInfo != null)
            {
                if (!IsAutoRequestIgnore(filterContext.Controller, filterContext.ActionDescriptor))
                   await Users.UpdateActionTimeAndOnLineTime(userInfo.Id);
                else
                   await Users.UpdateOnLineTime(userInfo.Id);
            }
        }
    }
}
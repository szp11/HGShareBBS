using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using HGShare.BBS.Controllers.Base;
using HGShare.Common;
using HGShare.Utils.Interface;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.BBS.Controllers
{
    public class ApiController : BaseController
    {
        private static readonly IUserAccessLogsPublic UserAccessLogsPublic = IcoReader.Service<IUserAccessLogsPublic>();
        private static readonly IVerifyCode VerCode = IcoReader.Service<IVerifyCode>();

        /// <summary>
        /// 跳转至指定位置
        /// </summary>
        /// <param name="actionName"></param>
        /// <param name="controllerName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public ActionResult GoToUrl(string actionName, string controllerName,string values)
        {
            var routeValueDictionary = new RouteValueDictionary();
            //解析
            if (!string.IsNullOrEmpty(values))
            {
                var va = values.Split('|');
                foreach (string t in va)
                {
                    if(string.IsNullOrEmpty(t))
                        continue;
                    var vas = t.Split(':');
                    routeValueDictionary.Add(vas[0],vas[1]);
                }
            }
            var url = Url.Action(actionName, controllerName, routeValueDictionary);
            return Redirect(url);
        }
        /// <summary>
        /// 访问日志统计
        /// </summary>
        /// <returns></returns>
        [AutoRequest]
        public async Task<ActionResult> VisitLog(string url, string reReferer)
        {
            //用户请求日志
           await UserAccessLogsPublic.AddAsync(new UserAccessLogVModel
            {
                Ip = Fetch.Ip,
                Url = url,
                Referer = reReferer,
                Type = 1,
                UserAgent = Request.UserAgent,
                UserId = CurrentUserInfo == null ? 0 : CurrentUserInfo.Id
            });
            return new EmptyResult();
        }
        /// <summary>
        /// 验证码
        /// </summary>
        /// <returns></returns>
        [Description("验证码")]
        [AutoRequest]
        [HttpGet]
        public ActionResult VerifyCode()
        {
            return File(VerCode.GetVerifyCode(), @"image/Gif");
        }
    }
}
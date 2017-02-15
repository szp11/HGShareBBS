using System.Web.Mvc;
using HGShare.Site.ActionResult;
using HGShare.Web.ServiceManager;

namespace HGShare.BBS.Controllers.Base
{
    public class BaseToolsController : Controller
    {
        /// <summary>
        /// 容器读取
        /// </summary>
        public static IIcoReader IcoReader;

        static BaseToolsController()
        {
            if(IcoReader==null)
                IcoReader=new IocContainer();
        }

        #region 自动返回类型

        /// <summary>
        /// 自动返回类型
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="status"></param>
        /// <param name="url"></param>
        /// <param name="ajaxIsJump">ajax请求时是否调整至url</param>
        /// <returns></returns>
        public ActionResult AutoResult(string msg, bool status, string url, bool ajaxIsJump=false)
        {
            if (Request.IsAjaxRequest()) //ajax
                return new JsonResult
                  {
                      Data = new JsonResultModel
                      {
                          Message = msg,
                          ResultState = status,
                          Action = ajaxIsJump?url:string.Empty
                      },JsonRequestBehavior = JsonRequestBehavior.AllowGet
                  };
            if (!string.IsNullOrEmpty(url))
                return new RedirectResult(url);

            return new EmptyResult();
        }
        #endregion
        /// <summary>
        /// 显示警告页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="goUrl"></param>
        public void ShowWarning(string msg, string goUrl=null)
        {
            if (msg == null)
                msg = "错误操作~，~";

            string url = Url.Action("warning", "tips", new { msg = Server.UrlEncode(msg),url=Server.UrlEncode(goUrl) });

            HttpContext.Response.Redirect(url, true);
            HttpContext.Response.End();
        }
    }

    
}
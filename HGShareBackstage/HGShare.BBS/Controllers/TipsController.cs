using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;

namespace HGShare.BBS.Controllers
{
    public class TipsController : BaseController
    {
        /// <summary>
        /// 404页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Tip404()
        {
            return View();
        }

        /// <summary>
        /// 警告信息页
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ActionResult Warning(string msg,string url)
        {
            ViewBag.Url = Server.UrlDecode(url);
            ViewBag.Msg = Server.UrlDecode(msg);
            return View();
        }
    }
}
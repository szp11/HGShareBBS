using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.Site.ActionResult;
using HGShare.VWModel;
using HGShare.Web.Interface;

namespace HGShare.BBS.Controllers
{
    public class ArticleTypeController : BaseController
    {
        private static readonly IArticleTypes ArticleTypes = IocContainer.Service<IArticleTypes>();
        // GET: ArticleType
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 文章类型选择页面
        /// </summary>
        /// <param name="pid">要默认选中的id</param>
        /// <param name="did">要默认禁用的id</param>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize]
        [Description("文章类型选择页面")]
        public ActionResult ArticleTypeSelect()
        {
            var list = ArticleTypes.GetArticleTypeTreeVModel(true);
            return Json(new JsonResultModel<List<TreeVModel>>() { ResultState = true, Body = list }, JsonRequestBehavior.AllowGet);
        }
    }
}
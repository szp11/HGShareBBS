using System.Linq;
using System.Web.Mvc;
using System.ComponentModel;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.VWModel;
using Webdiyer.WebControls.Mvc;

namespace HGShare.Backstage.Controllers
{
	/// <summary>
    /// Article管理
    /// </summary>
    [Description("Article管理")]
    public class ArticlesController : BaseController
    {
        /// <summary>
        /// Article列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Article列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Articles.ArticleInfosToVModels(Articles.GetArticlePageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<ArticleVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="id">自增id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Article操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = Articles.DeleteArticle(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个Article操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = Articles.DeleteArticles(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Article
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Article页面")]
        public ActionResult Add()
        {
            var vm = new ArticleVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Article
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Article操作")]
        public JsonResult Add(ArticleVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Articles.AddArticle(Articles.ArticleVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Article页面")]
        public ActionResult Edit(int id)
        {
            var model = Articles.ArticleInfoToVModel(Articles.GetArticleInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Article
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Article操作")]
        public JsonResult Edit(ArticleVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Articles.UpdateArticle(Articles.ArticleVModelToInfo(model)) > 0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
    }
}


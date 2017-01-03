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
    /// ArticleType管理
    /// </summary>
    [Description("ArticleType管理")]
    public class ArticleTypesController : BaseController
    {
        /// <summary>
        /// ArticleType列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("ArticleType列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = ArticleTypes.ArticleTypeInfosToVModels(ArticleTypes.GetArticleTypePageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<ArticleTypeVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除ArticleType
        /// </summary>
        /// <param name="id">类型Id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除ArticleType操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = ArticleTypes.DeleteArticleType(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除ArticleType
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个ArticleType操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = ArticleTypes.DeleteArticleTypes(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加ArticleType
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加ArticleType页面")]
        public ActionResult Add()
        {
            var vm = new ArticleTypeVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加ArticleType
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加ArticleType操作")]
        public JsonResult Add(ArticleTypeVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = ArticleTypes.AddArticleType(ArticleTypes.ArticleTypeVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改ArticleType
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改ArticleType页面")]
        public ActionResult Edit(int id)
        {
            var model = ArticleTypes.ArticleTypeInfoToVModel(ArticleTypes.GetArticleTypeInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改ArticleType
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改ArticleType操作")]
        public JsonResult Edit(ArticleTypeVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = ArticleTypes.UpdateArticleType(ArticleTypes.ArticleTypeVModelToInfo(model)) > 0;
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


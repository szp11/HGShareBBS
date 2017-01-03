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
    /// Tag管理
    /// </summary>
    [Description("Tag管理")]
    public class TagsController : BaseController
    {
        /// <summary>
        /// Tag列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Tag列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Tags.TagInfosToVModels(Tags.GetTagPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<TagVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Tag操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = Tags.DeleteTag(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除Tag
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个Tag操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = Tags.DeleteTags(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Tag
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Tag页面")]
        public ActionResult Add()
        {
            var vm = new TagVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Tag
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Tag操作")]
        public JsonResult Add(TagVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Tags.AddTag(Tags.TagVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Tag
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Tag页面")]
        public ActionResult Edit(int id)
        {
            var model = Tags.TagInfoToVModel(Tags.GetTagInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Tag
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Tag操作")]
        public JsonResult Edit(TagVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Tags.UpdateTag(Tags.TagVModelToInfo(model)) > 0;
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


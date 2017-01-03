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
    /// Area管理
    /// </summary>
    [Description("Area管理")]
    public class AreasController : BaseController
    {
        /// <summary>
        /// Area列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Area列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Areas.AreaInfosToVModels(Areas.GetAreaPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<AreaVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Area
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Area操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = Areas.DeleteArea(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除Area
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个Area操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = Areas.DeleteAreas(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Area
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Area页面")]
        public ActionResult Add()
        {
            var vm = new AreaVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Area
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Area操作")]
        public JsonResult Add(AreaVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Areas.AddArea(Areas.AreaVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Area
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Area页面")]
        public ActionResult Edit(int id)
        {
            var model = Areas.AreaInfoToVModel(Areas.GetAreaInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Area
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Area操作")]
        public JsonResult Edit(AreaVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Areas.UpdateArea(Areas.AreaVModelToInfo(model)) > 0;
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


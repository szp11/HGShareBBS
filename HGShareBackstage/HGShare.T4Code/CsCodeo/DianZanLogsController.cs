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
    /// DianZanLog管理
    /// </summary>
    [Description("DianZanLog管理")]
    public class DianZanLogsController : BaseController
    {
        /// <summary>
        /// DianZanLog列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("DianZanLog列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = DianZanLogs.DianZanLogInfosToVModels(DianZanLogs.GetDianZanLogPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<DianZanLogVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除DianZanLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除DianZanLog操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = DianZanLogs.DeleteDianZanLog(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除DianZanLog
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个DianZanLog操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = DianZanLogs.DeleteDianZanLogs(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加DianZanLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加DianZanLog页面")]
        public ActionResult Add()
        {
            var vm = new DianZanLogVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加DianZanLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加DianZanLog操作")]
        public JsonResult Add(DianZanLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = DianZanLogs.AddDianZanLog(DianZanLogs.DianZanLogVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改DianZanLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改DianZanLog页面")]
        public ActionResult Edit(int id)
        {
            var model = DianZanLogs.DianZanLogInfoToVModel(DianZanLogs.GetDianZanLogInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改DianZanLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改DianZanLog操作")]
        public JsonResult Edit(DianZanLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = DianZanLogs.UpdateDianZanLog(DianZanLogs.DianZanLogVModelToInfo(model)) > 0;
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


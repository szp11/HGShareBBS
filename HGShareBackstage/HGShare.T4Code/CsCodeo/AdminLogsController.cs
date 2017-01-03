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
    /// AdminLog管理
    /// </summary>
    [Description("AdminLog管理")]
    public class AdminLogsController : BaseController
    {
        /// <summary>
        /// AdminLog列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("AdminLog列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = AdminLogs.AdminLogInfosToVModels(AdminLogs.GetAdminLogPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<AdminLogVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除AdminLog
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除AdminLog操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = AdminLogs.DeleteAdminLog(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除AdminLog
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个AdminLog操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = AdminLogs.DeleteAdminLogs(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加AdminLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加AdminLog页面")]
        public ActionResult Add()
        {
            var vm = new AdminLogVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加AdminLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加AdminLog操作")]
        public JsonResult Add(AdminLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = AdminLogs.AddAdminLog(AdminLogs.AdminLogVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改AdminLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改AdminLog页面")]
        public ActionResult Edit(int id)
        {
            var model = AdminLogs.AdminLogInfoToVModel(AdminLogs.GetAdminLogInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改AdminLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改AdminLog操作")]
        public JsonResult Edit(AdminLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = AdminLogs.UpdateAdminLog(AdminLogs.AdminLogVModelToInfo(model)) > 0;
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


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
    /// UserAccessLog管理
    /// </summary>
    [Description("UserAccessLog管理")]
    public class UserAccessLogsController : BaseController
    {
        /// <summary>
        /// UserAccessLog列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("UserAccessLog列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = UserAccessLogs.UserAccessLogInfosToVModels(UserAccessLogs.GetUserAccessLogPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserAccessLogVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除UserAccessLog
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除UserAccessLog操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = UserAccessLogs.DeleteUserAccessLog(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除UserAccessLog
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个UserAccessLog操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = UserAccessLogs.DeleteUserAccessLogs(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加UserAccessLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加UserAccessLog页面")]
        public ActionResult Add()
        {
            var vm = new UserAccessLogVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加UserAccessLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加UserAccessLog操作")]
        public JsonResult Add(UserAccessLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = UserAccessLogs.AddUserAccessLog(UserAccessLogs.UserAccessLogVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改UserAccessLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改UserAccessLog页面")]
        public ActionResult Edit(int id)
        {
            var model = UserAccessLogs.UserAccessLogInfoToVModel(UserAccessLogs.GetUserAccessLogInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改UserAccessLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改UserAccessLog操作")]
        public JsonResult Edit(UserAccessLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = UserAccessLogs.UpdateUserAccessLog(UserAccessLogs.UserAccessLogVModelToInfo(model)) > 0;
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


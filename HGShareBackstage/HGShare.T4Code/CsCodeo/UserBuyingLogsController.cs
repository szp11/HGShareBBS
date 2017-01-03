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
    /// UserBuyingLog管理
    /// </summary>
    [Description("UserBuyingLog管理")]
    public class UserBuyingLogsController : BaseController
    {
        /// <summary>
        /// UserBuyingLog列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("UserBuyingLog列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = UserBuyingLogs.UserBuyingLogInfosToVModels(UserBuyingLogs.GetUserBuyingLogPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserBuyingLogVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除UserBuyingLog
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除UserBuyingLog操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = UserBuyingLogs.DeleteUserBuyingLog(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除UserBuyingLog
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个UserBuyingLog操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = UserBuyingLogs.DeleteUserBuyingLogs(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加UserBuyingLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加UserBuyingLog页面")]
        public ActionResult Add()
        {
            var vm = new UserBuyingLogVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加UserBuyingLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加UserBuyingLog操作")]
        public JsonResult Add(UserBuyingLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = UserBuyingLogs.AddUserBuyingLog(UserBuyingLogs.UserBuyingLogVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改UserBuyingLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改UserBuyingLog页面")]
        public ActionResult Edit(int id)
        {
            var model = UserBuyingLogs.UserBuyingLogInfoToVModel(UserBuyingLogs.GetUserBuyingLogInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改UserBuyingLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改UserBuyingLog操作")]
        public JsonResult Edit(UserBuyingLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = UserBuyingLogs.UpdateUserBuyingLog(UserBuyingLogs.UserBuyingLogVModelToInfo(model)) > 0;
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


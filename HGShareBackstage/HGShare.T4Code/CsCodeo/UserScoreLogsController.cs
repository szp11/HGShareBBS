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
    /// UserScoreLog管理
    /// </summary>
    [Description("UserScoreLog管理")]
    public class UserScoreLogsController : BaseController
    {
        /// <summary>
        /// UserScoreLog列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("UserScoreLog列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = UserScoreLogs.UserScoreLogInfosToVModels(UserScoreLogs.GetUserScoreLogPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserScoreLogVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除UserScoreLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除UserScoreLog操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = UserScoreLogs.DeleteUserScoreLog(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除UserScoreLog
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个UserScoreLog操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = UserScoreLogs.DeleteUserScoreLogs(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加UserScoreLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加UserScoreLog页面")]
        public ActionResult Add()
        {
            var vm = new UserScoreLogVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加UserScoreLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加UserScoreLog操作")]
        public JsonResult Add(UserScoreLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = UserScoreLogs.AddUserScoreLog(UserScoreLogs.UserScoreLogVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改UserScoreLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改UserScoreLog页面")]
        public ActionResult Edit(int id)
        {
            var model = UserScoreLogs.UserScoreLogInfoToVModel(UserScoreLogs.GetUserScoreLogInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改UserScoreLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改UserScoreLog操作")]
        public JsonResult Edit(UserScoreLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = UserScoreLogs.UpdateUserScoreLog(UserScoreLogs.UserScoreLogVModelToInfo(model)) > 0;
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


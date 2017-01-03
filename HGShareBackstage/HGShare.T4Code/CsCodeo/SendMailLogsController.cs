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
    /// SendMailLog管理
    /// </summary>
    [Description("SendMailLog管理")]
    public class SendMailLogsController : BaseController
    {
        /// <summary>
        /// SendMailLog列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("SendMailLog列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = SendMailLogs.SendMailLogInfosToVModels(SendMailLogs.GetSendMailLogPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<SendMailLogVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除SendMailLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除SendMailLog操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = SendMailLogs.DeleteSendMailLog(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除SendMailLog
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个SendMailLog操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = SendMailLogs.DeleteSendMailLogs(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加SendMailLog
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加SendMailLog页面")]
        public ActionResult Add()
        {
            var vm = new SendMailLogVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加SendMailLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加SendMailLog操作")]
        public JsonResult Add(SendMailLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = SendMailLogs.AddSendMailLog(SendMailLogs.SendMailLogVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改SendMailLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改SendMailLog页面")]
        public ActionResult Edit(int id)
        {
            var model = SendMailLogs.SendMailLogInfoToVModel(SendMailLogs.GetSendMailLogInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改SendMailLog
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改SendMailLog操作")]
        public JsonResult Edit(SendMailLogVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = SendMailLogs.UpdateSendMailLog(SendMailLogs.SendMailLogVModelToInfo(model)) > 0;
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


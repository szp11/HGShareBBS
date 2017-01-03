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
    /// Notify管理
    /// </summary>
    [Description("Notify管理")]
    public class NotifysController : BaseController
    {
        /// <summary>
        /// Notify列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Notify列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Notifys.NotifyInfosToVModels(Notifys.GetNotifyPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<NotifyVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Notify
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Notify操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = Notifys.DeleteNotify(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除Notify
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个Notify操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = Notifys.DeleteNotifys(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Notify
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Notify页面")]
        public ActionResult Add()
        {
            var vm = new NotifyVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Notify
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Notify操作")]
        public JsonResult Add(NotifyVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Notifys.AddNotify(Notifys.NotifyVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Notify
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Notify页面")]
        public ActionResult Edit(int id)
        {
            var model = Notifys.NotifyInfoToVModel(Notifys.GetNotifyInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Notify
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Notify操作")]
        public JsonResult Edit(NotifyVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Notifys.UpdateNotify(Notifys.NotifyVModelToInfo(model)) > 0;
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


using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.Model;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.VWModel;
using Webdiyer.WebControls.Mvc;

namespace HGShare.Backstage.Controllers
{
    /// <summary>
    /// 访问日志管理
    /// </summary>
    [Description("访问日志管理")]
    public class AdminLogsController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("访问日志列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = AdminLogs.AdminLogInfosToVModels(AdminLogs.GetAdminLogPageList(pageIndex, pageSize, null, null, out pageCount, out count));
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
        [Description("删除访问日志操作")]
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
        [Description("删除多个访问日志操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = AdminLogs.DeleteAdminLogs(ids) > 0 };
            return Json(result);
        }
    }
}


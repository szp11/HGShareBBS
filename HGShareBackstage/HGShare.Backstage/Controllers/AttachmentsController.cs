using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
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
    /// 附件管理
    /// </summary>
    [Description("附件管理")]
    public class AttachmentsController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("附件列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = Attachments.AttachmentInfosToVModels(Attachments.GetAttachmentPageList(pageIndex, pageSize, null, null, out pageCount, out count));
            PagedList<AttachmentVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
        /// <summary>
        /// 删除Attachment
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除附件操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = Attachments.DeleteAttachment(id) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 删除Attachment
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除多个附件操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = Attachments.DeleteAttachments(ids) > 0 };
            return Json(result);
        }
        ///// <summary>
        ///// 添加Attachment
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Add()
        //{
        //    var vm = new AttachmentVModel();
        //    return View(vm);
        //}
        ///// <summary>
        ///// 添加Attachment
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult Add(AttachmentVModel model)
        //{
        //    var result = new JsonResultModel();
        //    if (ModelState.IsValid)
        //    {
        //        //add 
        //        result.ResultState = Attachments.AddAttachment(Attachments.AttachmentVModelToInfo(model)) > 0;
        //    }
        //    else
        //    {
        //        result.ResultState = false;
        //        result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
        //    }

        //    return Json(result);
        //}
        ///// <summary>
        ///// 修改Attachment
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var model = Attachments.AttachmentInfoToVModel(Attachments.GetAttachmentInfo(id));
        //    return View(model);
        //}
        ///// <summary>
        ///// 修改Attachment
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //public JsonResult Edit(AttachmentVModel model)
        //{
        //    var result = new JsonResultModel();
        //    if (ModelState.IsValid)
        //    {
        //        //edit user
        //        result.ResultState = Attachments.UpdateAttachment(Attachments.AttachmentVModelToInfo(model)) > 0;
        //    }
        //    else
        //    {
        //        result.ResultState = false;
        //        result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
        //    }

        //    return Json(result);
        //}
    }
}


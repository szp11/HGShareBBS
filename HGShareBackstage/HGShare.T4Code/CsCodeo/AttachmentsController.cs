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
    /// Attachment管理
    /// </summary>
    [Description("Attachment管理")]
    public class AttachmentsController : BaseController
    {
        /// <summary>
        /// Attachment列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Attachment列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Attachments.AttachmentInfosToVModels(Attachments.GetAttachmentPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<AttachmentVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Attachment
        /// </summary>
        /// <param name="id">附件Id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Attachment操作")]
        public JsonResult Delete(long id)
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
		[Description("删除多个Attachment操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = Attachments.DeleteAttachments(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Attachment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Attachment页面")]
        public ActionResult Add()
        {
            var vm = new AttachmentVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Attachment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Attachment操作")]
        public JsonResult Add(AttachmentVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Attachments.AddAttachment(Attachments.AttachmentVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Attachment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Attachment页面")]
        public ActionResult Edit(int id)
        {
            var model = Attachments.AttachmentInfoToVModel(Attachments.GetAttachmentInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Attachment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Attachment操作")]
        public JsonResult Edit(AttachmentVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Attachments.UpdateAttachment(Attachments.AttachmentVModelToInfo(model)) > 0;
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


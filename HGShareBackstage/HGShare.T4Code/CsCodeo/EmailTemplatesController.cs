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
    /// EmailTemplate管理
    /// </summary>
    [Description("EmailTemplate管理")]
    public class EmailTemplatesController : BaseController
    {
        /// <summary>
        /// EmailTemplate列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("EmailTemplate列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = EmailTemplates.EmailTemplateInfosToVModels(EmailTemplates.GetEmailTemplatePageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<EmailTemplateVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除EmailTemplate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除EmailTemplate操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = EmailTemplates.DeleteEmailTemplate(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除EmailTemplate
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个EmailTemplate操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = EmailTemplates.DeleteEmailTemplates(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加EmailTemplate
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加EmailTemplate页面")]
        public ActionResult Add()
        {
            var vm = new EmailTemplateVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加EmailTemplate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加EmailTemplate操作")]
        public JsonResult Add(EmailTemplateVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = EmailTemplates.AddEmailTemplate(EmailTemplates.EmailTemplateVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改EmailTemplate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改EmailTemplate页面")]
        public ActionResult Edit(int id)
        {
            var model = EmailTemplates.EmailTemplateInfoToVModel(EmailTemplates.GetEmailTemplateInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改EmailTemplate
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改EmailTemplate操作")]
        public JsonResult Edit(EmailTemplateVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = EmailTemplates.UpdateEmailTemplate(EmailTemplates.EmailTemplateVModelToInfo(model)) > 0;
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


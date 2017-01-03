using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HGShare.Business;
using HGShare.Model;
using HGShare.VWModel;

namespace HGShare.Backstage.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult UploadImageView(Guid uploadid)
        {
            ViewBag.Guid = uploadid;
            IList<AttachmentVModel> list = Attachments.AttachmentInfosToVModels(Attachments.GetAttachmentByGuid(uploadid));
            return PartialView(list);
        }
    }
}
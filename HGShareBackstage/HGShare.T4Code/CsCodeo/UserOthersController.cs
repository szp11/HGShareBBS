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
    /// UserOther管理
    /// </summary>
    [Description("UserOther管理")]
    public class UserOthersController : BaseController
    {
        /// <summary>
        /// UserOther列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("UserOther列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = UserOthers.UserOtherInfosToVModels(UserOthers.GetUserOtherPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserOtherVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除UserOther
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除UserOther操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = UserOthers.DeleteUserOther(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除UserOther
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个UserOther操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = UserOthers.DeleteUserOthers(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加UserOther
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加UserOther页面")]
        public ActionResult Add()
        {
            var vm = new UserOtherVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加UserOther
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加UserOther操作")]
        public JsonResult Add(UserOtherVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = UserOthers.AddUserOther(UserOthers.UserOtherVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改UserOther
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改UserOther页面")]
        public ActionResult Edit(int id)
        {
            var model = UserOthers.UserOtherInfoToVModel(UserOthers.GetUserOtherInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改UserOther
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改UserOther操作")]
        public JsonResult Edit(UserOtherVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = UserOthers.UpdateUserOther(UserOthers.UserOtherVModelToInfo(model)) > 0;
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


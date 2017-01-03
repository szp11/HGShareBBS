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
    /// User管理
    /// </summary>
    [Description("User管理")]
    public class UsersController : BaseController
    {
        /// <summary>
        /// User列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("User列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Users.UserInfosToVModels(Users.GetUserPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除User
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除User操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = Users.DeleteUser(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除User
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个User操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = Users.DeleteUsers(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加User页面")]
        public ActionResult Add()
        {
            var vm = new UserVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加User操作")]
        public JsonResult Add(UserVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Users.AddUser(Users.UserVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改User页面")]
        public ActionResult Edit(int id)
        {
            var model = Users.UserInfoToVModel(Users.GetUserInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改User
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改User操作")]
        public JsonResult Edit(UserVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Users.UpdateUser(Users.UserVModelToInfo(model)) > 0;
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


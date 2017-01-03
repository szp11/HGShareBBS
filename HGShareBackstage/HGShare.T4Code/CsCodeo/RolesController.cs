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
    /// Role管理
    /// </summary>
    [Description("Role管理")]
    public class RolesController : BaseController
    {
        /// <summary>
        /// Role列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Role列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Roles.RoleInfosToVModels(Roles.GetRolePageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<RoleVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Role操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = Roles.DeleteRole(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除Role
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个Role操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = Roles.DeleteRoles(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Role页面")]
        public ActionResult Add()
        {
            var vm = new RoleVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Role操作")]
        public JsonResult Add(RoleVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Roles.AddRole(Roles.RoleVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Role页面")]
        public ActionResult Edit(int id)
        {
            var model = Roles.RoleInfoToVModel(Roles.GetRoleInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Role
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Role操作")]
        public JsonResult Edit(RoleVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Roles.UpdateRole(Roles.RoleVModelToInfo(model)) > 0;
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


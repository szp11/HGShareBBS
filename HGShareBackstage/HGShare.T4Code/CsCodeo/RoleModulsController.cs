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
    /// RoleModul管理
    /// </summary>
    [Description("RoleModul管理")]
    public class RoleModulsController : BaseController
    {
        /// <summary>
        /// RoleModul列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("RoleModul列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = RoleModuls.RoleModulInfosToVModels(RoleModuls.GetRoleModulPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<RoleModulVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除RoleModul
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除RoleModul操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = RoleModuls.DeleteRoleModul(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除RoleModul
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个RoleModul操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = RoleModuls.DeleteRoleModuls(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加RoleModul
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加RoleModul页面")]
        public ActionResult Add()
        {
            var vm = new RoleModulVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加RoleModul
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加RoleModul操作")]
        public JsonResult Add(RoleModulVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = RoleModuls.AddRoleModul(RoleModuls.RoleModulVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改RoleModul
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改RoleModul页面")]
        public ActionResult Edit(int id)
        {
            var model = RoleModuls.RoleModulInfoToVModel(RoleModuls.GetRoleModulInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改RoleModul
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改RoleModul操作")]
        public JsonResult Edit(RoleModulVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = RoleModuls.UpdateRoleModul(RoleModuls.RoleModulVModelToInfo(model)) > 0;
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


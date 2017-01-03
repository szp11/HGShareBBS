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
    /// Modul管理
    /// </summary>
    [Description("Modul管理")]
    public class ModulsController : BaseController
    {
        /// <summary>
        /// Modul列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("Modul列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = Moduls.ModulInfosToVModels(Moduls.GetModulPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<ModulVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除Modul
        /// </summary>
        /// <param name="id">模块ID</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除Modul操作")]
        public JsonResult Delete(int id)
        {
            var result = new JsonResultModel { ResultState = Moduls.DeleteModul(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除Modul
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个Modul操作")]
        public JsonResult Deletes(int [] ids)
        {
            var result = new JsonResultModel { ResultState = Moduls.DeleteModuls(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加Modul
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加Modul页面")]
        public ActionResult Add()
        {
            var vm = new ModulVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Modul
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加Modul操作")]
        public JsonResult Add(ModulVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Moduls.AddModul(Moduls.ModulVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改Modul
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改Modul页面")]
        public ActionResult Edit(int id)
        {
            var model = Moduls.ModulInfoToVModel(Moduls.GetModulInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Modul
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改Modul操作")]
        public JsonResult Edit(ModulVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Moduls.UpdateModul(Moduls.ModulVModelToInfo(model)) > 0;
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


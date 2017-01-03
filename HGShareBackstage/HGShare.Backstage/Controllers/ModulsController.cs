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
    /// 模块管理
    /// </summary>
    [Description("模块管理")]
    public class ModulsController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("模块列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = Moduls.ModulInfosToVModels(Moduls.GetModulPageList(pageIndex, pageSize, null, null, out pageCount, out count));
            result.ForEach(n => { 
                if(n.PId!=0){
                    var modulinfo = Moduls.GetModulInfo(n.PId);
                        if(modulinfo!=null)
                            n.PName=modulinfo.ModulName;
                }
            });
            
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
        [Description("删除模块操作")]
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
        [Description("删除多个模块操作")]
        public JsonResult Deletes(int[] ids)
        {
            var result = new JsonResultModel { ResultState = Moduls.DeleteModuls(ids) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 添加Modul
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加模块页面")]
        public ActionResult Add()
        {
            var vm = new ModulVModel
            {
                IsDisplay = true,
                IsShow = true,
                PName = "顶级模块"
            };
            return View(vm);
        }
        /// <summary>
        /// 添加Modul
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("添加模块操作")]
        public JsonResult Add(ModulVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Moduls.AddModul(Moduls.ModulVModelToInfo(model)) > 0;
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
        [Description("修改模块页面")]
        public ActionResult Edit(int id)
        {
            var model = Moduls.ModulInfoToVModel(Moduls.GetModulInfo(id));
            if (model.PId != 0)
                model.PName = Moduls.GetModulInfo(model.PId).ModulName;

            return View(model);
        }
        /// <summary>
        /// 修改Modul
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("修改模块操作")]
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

        /// <summary>
        /// 模块选择
        /// </summary>
        /// <param name="id">要排除的模块id</param>
        /// <param name="pid">要默认选中的模块id</param>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorizeIgnore]
        [Description("选择模块页面")]
        public ActionResult ModulSelect(int? id,int? pid)
        {
            var moduls = Moduls.ModulInfosToTreeVModels(Moduls.GetAllModul(id));
            //选中
            //Moduls.SelectTreeNode(moduls, pid);
            ViewBag.SelectedId = pid;
            return View(moduls);
        }
        /// <summary>
        /// 根据父级生成排序值（排序值只会越来越大，越大的越靠前）
        /// </summary>
        /// <param name="pid">父级id</param>
        /// <param name="id">编辑时可排除自身所占位置</param>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorizeIgnore]
        [Description("获取模块排序值")]
        public JsonResult GetOrderNumber(int pid, int? id)
        {
            var result = new JsonResultModel<int> {ResultState = true, Body = Moduls.GetOrderNumber(pid, id)};
            return Json(result);
        }
    }
}


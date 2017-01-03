using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Mvc;
using HGShare.Backstage.Controllers.Base;
using HGShare.Business;
using HGShare.Model;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.Site.Config;
using HGShare.VWModel;
using Newtonsoft.Json;
using Webdiyer.WebControls.Mvc;

namespace HGShare.Backstage.Controllers
{   
    /// <summary>
    /// 角色管理
    /// </summary>
    [Description("角色管理")]
    public class RolesController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("角色列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = Roles.RoleInfosToVModels(Roles.GetRolePageList(pageIndex, pageSize, null, null, out pageCount, out count));
            PagedList<RoleVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除角色操作")]
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
        public JsonResult Deletes(int[] ids)
        {
            var result = new JsonResultModel { ResultState = Roles.DeleteRoles(ids) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加角色页面")]
        public ActionResult Add()
        {
            var vm = new RoleVModel(){IsSuper = false};
            return View(vm);
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("添加角色操作")]
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
        [Description("修改角色页面")]
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
        [Description("修改角色操作")]
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

        /// <summary>
        /// 分配资源
        /// </summary>
        /// <param name="id">角色id</param>
        /// <returns></returns>
        [HttpGet]
        [Description("分配模块页面")]
        public ActionResult AllocationModul(int id)
        {
            //查询已有模块 在前端进行默认选中
            int[] mIds = RoleModuls.GetMIds(id);
            ViewBag.SelectedIds = JsonConvert.SerializeObject(mIds.Select(n => "tree_node_" + n).ToList());
            //查询资源树
            var moduls = Moduls.ModulInfosToTreeVModels(Moduls.GetAllModul(id));
            ViewBag.RId = id;
            return View(moduls);
        }
        /// <summary>
        /// 分配资源
        /// </summary>
        /// <param name="rid"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分配模块操作")]
        public ActionResult AllocationModul(int rid,string ids)
        {
            var result = new JsonResultModel()
            {
                ResultState = true
            };
            
            //根据角色id删除原有关系
            RoleModuls.DeleteRoleModuls(rid);
            //根据选中的模块新建关系
            List<RoleModulInfo> rolemoduls = ids.Split(',').Where(n=>!string.IsNullOrEmpty(n)).Select(n => new RoleModulInfo
            {
                RId = rid,
                MId = int.Parse(n)

            }).ToList();

            RoleModuls.AddRoleModuls(rolemoduls);
            return Json(result);
        }
    }
}


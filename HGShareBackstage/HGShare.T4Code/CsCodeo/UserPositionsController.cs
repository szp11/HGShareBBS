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
    /// UserPosition管理
    /// </summary>
    [Description("UserPosition管理")]
    public class UserPositionsController : BaseController
    {
        /// <summary>
        /// UserPosition列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("UserPosition列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = UserPositions.UserPositionInfosToVModels(UserPositions.GetUserPositionPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserPositionVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除UserPosition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除UserPosition操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = UserPositions.DeleteUserPosition(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除UserPosition
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个UserPosition操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = UserPositions.DeleteUserPositions(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加UserPosition
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加UserPosition页面")]
        public ActionResult Add()
        {
            var vm = new UserPositionVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加UserPosition
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加UserPosition操作")]
        public JsonResult Add(UserPositionVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = UserPositions.AddUserPosition(UserPositions.UserPositionVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改UserPosition
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改UserPosition页面")]
        public ActionResult Edit(int id)
        {
            var model = UserPositions.UserPositionInfoToVModel(UserPositions.GetUserPositionInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改UserPosition
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改UserPosition操作")]
        public JsonResult Edit(UserPositionVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = UserPositions.UpdateUserPosition(UserPositions.UserPositionVModelToInfo(model)) > 0;
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


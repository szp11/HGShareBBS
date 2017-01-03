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
    /// UserActivateToken管理
    /// </summary>
    [Description("UserActivateToken管理")]
    public class UserActivateTokensController : BaseController
    {
        /// <summary>
        /// UserActivateToken列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
		[Description("UserActivateToken列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count,pageCount;
            var result = UserActivateTokens.UserActivateTokenInfosToVModels(UserActivateTokens.GetUserActivateTokenPageList(pageIndex, pageSize,null,null,out pageCount, out count));
            PagedList<UserActivateTokenVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
		/// <summary>
        /// 删除UserActivateToken
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除UserActivateToken操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = UserActivateTokens.DeleteUserActivateToken(id) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 删除UserActivateToken
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
		[Description("删除多个UserActivateToken操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = UserActivateTokens.DeleteUserActivateTokens(ids) > 0 };
            return Json(result);
        }
		/// <summary>
        /// 添加UserActivateToken
        /// </summary>
        /// <returns></returns>
        [HttpGet]
		[Description("添加UserActivateToken页面")]
        public ActionResult Add()
        {
            var vm = new UserActivateTokenVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加UserActivateToken
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("添加UserActivateToken操作")]
        public JsonResult Add(UserActivateTokenVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = UserActivateTokens.AddUserActivateToken(UserActivateTokens.UserActivateTokenVModelToInfo(model))>0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
		 /// <summary>
        /// 修改UserActivateToken
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
		[Description("修改UserActivateToken页面")]
        public ActionResult Edit(int id)
        {
            var model = UserActivateTokens.UserActivateTokenInfoToVModel(UserActivateTokens.GetUserActivateTokenInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改UserActivateToken
        /// </summary>
        /// <returns></returns>
        [HttpPost]
		[Description("修改UserActivateToken操作")]
        public JsonResult Edit(UserActivateTokenVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = UserActivateTokens.UpdateUserActivateToken(UserActivateTokens.UserActivateTokenVModelToInfo(model)) > 0;
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


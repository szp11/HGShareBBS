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
    /// Comment管理
    /// </summary>
    [Description("Comment管理")]
    public class CommentsController : BaseController
    {
        /// <summary>
        /// Comment列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("Comment列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = Comments.GetCommentPageList(pageIndex, pageSize, null, null, out pageCount, out count);
            PagedList<CommentVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
        /// <summary>
        /// 删除Comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除Comment操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = Comments.DeleteComment(id) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 删除Comment
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除多个Comment操作")]
        public JsonResult Deletes(long[] ids)
        {
            var result = new JsonResultModel { ResultState = Comments.DeleteComments(ids) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 添加Comment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加Comment页面")]
        public ActionResult Add()
        {
            var vm = new CommentVModel();
            return View(vm);
        }
        /// <summary>
        /// 添加Comment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("添加Comment操作")]
        public JsonResult Add(CommentVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //add 
                result.ResultState = Comments.AddComment(Comments.CommentVModelToInfo(model)) > 0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        /// <summary>
        /// 修改Comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("修改Comment页面")]
        public ActionResult Edit(int id)
        {
            var model = Comments.CommentInfoToVModel(Comments.GetCommentInfo(id));
            return View(model);
        }
        /// <summary>
        /// 修改Comment
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("修改Comment操作")]
        public JsonResult Edit(CommentVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                //edit user
                result.ResultState = Comments.UpdateComment(Comments.CommentVModelToInfo(model)) > 0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        /// <summary>
        /// 审核文章
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("审核评论")]
        public JsonResult Verified(long id, bool state, string reason)
        {
            reason = Server.UrlDecode(reason);
            bool status = Comments.UpdateState(id, CurrentUserInfo.Id, state, reason);

            return Json(new JsonResultModel { ResultState = status, Message = (status ? "审核完成!" : "审核失败!") });
        }
        /// <summary>
        /// 审核文章
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("审核评论")]
        public JsonResult Verifieds(long[] ids, bool state, string reason)
        {
            reason = Server.UrlDecode(reason);
            bool status = Comments.UpdateState(ids, CurrentUserInfo.Id, state, reason);

            return Json(new JsonResultModel { ResultState = status, Message = (status ? "审核完成!" : "审核失败!") });
        }

        /// <summary>
        /// 查看评论内容页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("查看评论内容页面")]
        public ActionResult Content(int id)
        {
            var model = Comments.CommentInfoToVModel(Comments.GetCommentInfo(id));
            return View((object)model.Content);
        }
    }
}


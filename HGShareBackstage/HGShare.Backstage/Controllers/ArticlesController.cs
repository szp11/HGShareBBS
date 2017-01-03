using System;
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
    /// 文章管理
    /// </summary>
    [Description("文章管理")]
    public class ArticlesController : BaseController
    {
        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="page">页码</param>
        /// <returns></returns>
        [Description("文章列表")]
        public ActionResult Index(int? page)
        {
            int pageIndex = page.HasValue ? page.Value : 1;
            int pageSize = PageConfig.BackstagePageSize;
            int count, pageCount;
            var result = Articles.GetArticlePageList(pageIndex, pageSize, null, null, out pageCount, out count);
            PagedList<ArticleVModel> pl = result.ToPagedList(pageIndex, pageSize);
            pl.TotalItemCount = count;
            pl.CurrentPageIndex = pageIndex;
            return View(pl);
        }
        /// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除文章操作")]
        public JsonResult Delete(long id)
        {
            var result = new JsonResultModel { ResultState = Articles.DeleteArticle(id) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        [HttpPost]
        [Description("删除多个文章操作")]
        public JsonResult Deletes(long [] ids)
        {
            var result = new JsonResultModel { ResultState = Articles.DeleteArticles(ids) > 0 };
            return Json(result);
        }
        /// <summary>
        /// 添加Article
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Description("添加文章页面")]
        public ActionResult Add()
        {
            var vm = new ArticleVModel { Guid = Guid.NewGuid() };
            return View(vm);
        }
        /// <summary>
        /// 添加Article
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("添加文章操作")]
        public JsonResult Add(ArticleVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                model.UserId = CurrentUserInfo.Id;
                //add 
                result.ResultState = Articles.AddArticle(Articles.ArticleVModelToInfo(model)) > 0;
            }
            else
            {
                result.ResultState = false;
                result.Message = ModelStateHelper.GetAllErrorMessage(ModelState);
            }

            return Json(result);
        }
        /// <summary>
        /// 修改Article
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Description("修改文章页面")]
        public ActionResult Edit(long id)
        {
            var model = Articles.ArticleInfoToVModel(Articles.GetArticleInfo(id));
            var typeinfo= ArticleTypes.GetArticleTypeInfo(model.Type);
            model.TypeName = typeinfo != null ? typeinfo.Name : "未知";
            return View(model);
        }
        /// <summary>
        /// 修改Article
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Description("修改文章操作")]
        public JsonResult Edit(ArticleVModel model)
        {
            var result = new JsonResultModel();
            if (ModelState.IsValid)
            {
                model.LastEditUserId = CurrentUserInfo.Id;
                //edit user
                result.ResultState = Articles.UpdateArticle(Articles.ArticleVModelToInfo(model)) > 0;
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
        [Description("审核文章")]
        public JsonResult Verified(long id, bool state, string reason)
        {
            reason = Server.UrlDecode(reason);
            bool status=Articles.UpdateState(id, CurrentUserInfo.Id, state, reason);

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
        [Description("审核文章")]
        public JsonResult Verifieds(long [] ids, bool state, string reason)
        {
            reason = Server.UrlDecode(reason);
            bool status = Articles.UpdateState(ids, CurrentUserInfo.Id, state, reason);

            return Json(new JsonResultModel { ResultState = status, Message = (status ? "审核完成!" : "审核失败!") });
        }
        /// <summary>
        /// 关闭/打开评文章论
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isCloseComment"></param>
        /// <param name="reason"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("关闭/打开评文章论")]
        public JsonResult CloseComment(long id, bool isCloseComment, string reason)
        {
            reason = Server.UrlDecode(reason);
            bool status = Articles.UpdateIsCloseComment(id, CurrentUserInfo.Id, isCloseComment, reason);

            return Json(new JsonResultModel { ResultState = status, Message = (status ? "完成!" : "失败!") });
        }

        /// <summary>
        /// 文章加精/取消加精
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("加精/取消加精")]
        public JsonResult IsJiaJing(long id, bool state)
        {
            bool status = Articles.UpdateIsJiaJing(id, CurrentUserInfo.Id, state);

            return Json(new JsonResultModel { ResultState = status, Message = (status ? "完成!" : "失败!") });
        }

        /// <summary>
        /// 文章置顶/取消置顶
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("文章置顶/取消置顶")]
        public JsonResult IsStick(long id, bool state)
        {
            bool status = Articles.UpdateIsStick(id, CurrentUserInfo.Id, state);

            return Json(new JsonResultModel { ResultState = status, Message = (status ? "完成!" : "失败!") });
        }
    }
}


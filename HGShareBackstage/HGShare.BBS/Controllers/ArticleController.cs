using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.BBS.Models;
using HGShare.BBS.Models.Search;
using HGShare.Com.Interface;
using HGShare.Site;
using HGShare.Site.ActionResult;
using HGShare.VWModel;
using HGShare.Web.Interface;
using Webdiyer.WebControls.Mvc;

namespace HGShare.BBS.Controllers
{
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleController : BaseController
    {
        private static readonly IArticles Articles = IocContainer.Service<IArticles>();
        private static readonly IArticlesPublic ArticlesPublic = IocContainer.Service<IArticlesPublic>();
        private static readonly IArticleTypes ArticleTypes = IocContainer.Service<IArticleTypes>();
        private static readonly IUsers Users = IocContainer.Service<IUsers>();
        private static readonly IVerifyCode VerCode = IocContainer.Service<IVerifyCode>();
        private static readonly IUsersPublic UserPublic = IocContainer.Service<IUsersPublic>();
        private static readonly IRoles Roles = IocContainer.Service<IRoles>();
        /// <summary>
        /// 文章列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(ArticleSearchEntity search)
        {
            if (!search.type.HasValue)
                search.type = 0;
            if (!search.bType.HasValue)
                search.bType = 0;
            if (!search.pageIndex.HasValue)
                search.pageIndex = 1;
            if (!search.isJingHua.HasValue)
                search.isJingHua = 0;
             int pageSize = Site.Config.PageConfig.WebArticlePageSize;
            var list = new List<ArticleWebEntity>();

            int dataCount = 0;
            //文章
            var articlesList = Articles.SearchArticlesByTypes(search.type.Value, search.bType.Value, search.pageIndex.Value, pageSize, search.isJingHua.Value, out dataCount);
            if (articlesList != null)
            {
                //用户
                var userlist = Users.GetUsersByIds(articlesList.Select(n => n.UserId).ToArray());
                list.AddRange(from articleVModel in articlesList
                              let user = userlist.FirstOrDefault(n => n.Id == articleVModel.UserId)
                              select new ArticleWebEntity()
                              {
                                  Article = articleVModel,
                                  User = user
                              });
            }
            ViewBag.ArticleTypeName = "活动列表";
            if (search.type > 0)
            {
                var articleType = ArticleTypes.GetArticleTypeVModelById(search.type.Value);
                if (articleType!=null)
                    ViewBag.ArticleTypeName = articleType.Name;
            }
            ViewBag.Search = search;
            ViewData["HeadNavCurrent"] = search.type;

            PagedList<ArticleWebEntity> pageList = list.ToPagedList(search.pageIndex.Value, pageSize);
            pageList.TotalItemCount = (int)dataCount;
            pageList.CurrentPageIndex = search.pageIndex.Value;
            return View(pageList);
        }
        /// <summary>
        /// 详细页
        /// </summary>
        /// <returns></returns>
        public ActionResult Detail(CommentSearch search)
        {
            var model = new ArticleWebEntity
            { 
                //public中是实时数据
                Article =
                    UserIsLogin
                        ? ArticlesPublic.GetArticleInfoById((int) search.AId)
                        : Articles.GetArticleInfoById((int) search.AId)
            };
            
           

            if (model.Article == null || model.Article.IsNull)
                return new RedirectResult(Url.Action("Tip404", "Tips"));

            //非审核通过的帖子，只能自己看
            if ((model.Article.State !=1) && (CurrentUserInfo==null || CurrentUserInfo.Id != model.Article.UserId))
                return new RedirectResult(Url.Action("Tip404", "Tips"));

            model.User = Users.GetUserById(model.Article.UserId);
            ViewData["HeadNavCurrent"] = model.Article.Type;
            model.CommentSearch = search;
           
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize]
        [UserDisableVerification]
        [EmailActivatedVerification]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorize]
        [UserDisableVerification]
        [EmailActivatedVerification]
        [ValidateInput(false)]
        public async Task<ActionResult> Add(VWModel.ArticleVModel model, string vercode)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResultModel { Message = ModelStateHelper.GetAllErrorMessage(ModelState) });
            if (string.IsNullOrEmpty(vercode))
                return Json(new JsonResultModel { Message = "请输入验证码！" });
            if (!VerCode.CheckVerifyCode(vercode))
                return Json(new JsonResultModel { Message = "验证码错误，请重新输入！" });
            
            if(!ArticlesPublic.CheckCanPost(CurrentUserInfo.Id,Site.Config.UserConfig.AddArticleInterval))
                return Json(new JsonResultModel { Message = "操作速度太快了，喝口水再试一下！" });
            model.UserId = CurrentUserInfo.Id;
            model.LastEditUserId = CurrentUserInfo.Id;
            var user = Users.GetUserById(CurrentUserInfo.Id);
            if(user==null || user.IsNull)
                return Json(new JsonResultModel { Message = "用户信息异常！" });
            var role = Roles.GetRole(user.RoleId);
            if(role==null || role.IsNull)
                return Json(new JsonResultModel { Message = "角色信息异常！" });
            //根据角色判断是否需要审核
            model.State = (short)(role.ArticleNeedVerified?0:1);

            long id = await ArticlesPublic.Add(model);
            if (id <= 0)
                return Json(new JsonResultModel {Message = "提交失败！"});
            await UserPublic.UpdateArticleNum(CurrentUserInfo.Id, 1);
            return Json(new JsonResultModel { ResultState = true, Message = "发表成功！", Action = Url.Action("Detail", "Article",new{aid=id}) });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize]
        [UserDisableVerification]
        [EmailActivatedVerification]
        public ActionResult Edit(long id)
        {
            var article = Articles.GetArticleInfoById(id);
            if (article == null || article.IsNull)
                return new RedirectResult(Url.Action("Tip404", "Tips"));
            if(CurrentUserInfo.Id!=article.UserId)
                ShowWarning("您无法修改别人的东西!");

            return View(article);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorize]
        [UserDisableVerification]
        [EmailActivatedVerification]
        [ValidateInput(false)]
        public  ActionResult Edit(VWModel.ArticleVModel model, string vercode)
        {
            if (!ModelState.IsValid)
                return Json(new JsonResultModel { Message = ModelStateHelper.GetAllErrorMessage(ModelState) });
            if (string.IsNullOrEmpty(vercode))
                return Json(new JsonResultModel { Message = "请输入验证码！" });
            if (!VerCode.CheckVerifyCode(vercode))
                return Json(new JsonResultModel { Message = "验证码错误，请重新输入！" });

            //文章作者检测
            var article = Articles.GetArticleInfoById(model.Id);
            if(article==null || article.IsNull || article.UserId!=CurrentUserInfo.Id)
                return Json(new JsonResultModel { Message = "您的文章不存在！" });
            
            model.LastEditUserId = CurrentUserInfo.Id;
            var user = Users.GetUserById(CurrentUserInfo.Id);
            if (user == null || user.IsNull)
                return Json(new JsonResultModel { Message = "用户信息异常！" });
            var role = Roles.GetRole(user.RoleId);
            if (role == null || role.IsNull)
                return Json(new JsonResultModel { Message = "角色信息异常！" });
            //根据角色判断是否需要审核
            model.State = (short)(role.ArticleNeedVerified ? 0 : 1);

            bool status =ArticlesPublic.UpdateArticle(model);
            if (!status)
                return Json(new JsonResultModel { Message = "修改失败！" });
           
            return Json(new JsonResultModel { ResultState = true, Message = "修改成功！", Action = Url.Action("Detail", "Article", new { aid = model.Id }) });
        }

        /// <summary>
        /// 更新文章访问量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UpdataDot(long id)
        {
            await ArticlesPublic.UpdateDot(id);
            return new EmptyResult();
        }
        /// <summary>
        /// 获取文章访问量
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> GetDots(long [] ids)
        {
           var hotfield= await ArticlesPublic.GetArticleHotFieldDots(ids);
            return Json(new JsonResultModel<List<ArticleHotFieldVModel>>() { Body = hotfield.ToList(), ResultState = true });
        }
    }
}
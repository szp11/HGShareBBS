using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.BBS.Models;
using HGShare.BBS.Models.Search;
using HGShare.Site.ActionResult;
using HGShare.VWModel;
using HGShare.Web.Interface;
using Webdiyer.WebControls.Mvc;

namespace HGShare.BBS.Controllers
{
    public class CommentController : BaseController
    {
        private static readonly IUsers Users = IcoReader.Service<IUsers>();
        private static readonly IComments Comments = IcoReader.Service<IComments>();
        private static readonly ICommentsPublic CommentsPublic = IcoReader.Service<ICommentsPublic>();
        private static readonly IDianZanLogsPublic DianZanLogsPublic = IcoReader.Service<IDianZanLogsPublic>();
        private static readonly IRoles Roles = IcoReader.Service<IRoles>();
        private static readonly IArticles Articles = IcoReader.Service<IArticles>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public PartialViewResult Index(CommentSearch search)
        {
            string order = search.Order == 0 ? "ASC" : "DESC";
            if (search.PageIndex <1)
                search.PageIndex = 1;
            int pageSize = Site.Config.PageConfig.WebCommentPageSize;
            int dataCount = 0;
            var list = new List<CommentEntity>();
            var comments = Comments.GetComments(search.PageIndex, pageSize, search.AId, search.AuthorId, order, out dataCount);
            if (comments != null)
            {
                var users = Users.GetUsersByIds(comments.Select(n => n.UserId).ToArray());
                foreach (var comment in comments)
                {
                    var user = new UserVModel();
                    if (users.Any(n => n.Id == comment.UserId))
                        user = users.FirstOrDefault(n => n.Id == comment.UserId);
                    list.Add(new CommentEntity
                    {
                        Comment = comment,
                        User = user
                    });
                }
            }
            PagedList<CommentEntity> pageList = list.ToPagedList(search.PageIndex, pageSize);
            pageList.TotalItemCount = dataCount;
            pageList.CurrentPageIndex = search.PageIndex;
            //如果登录的话取得登录用户对该帖子下所有评论的点赞记录
            if (UserIsLogin)
            {
               List<long> commentIds= DianZanLogsPublic.GetUserAllDianZanCommentId(CurrentUserInfo.Id, search.AId);
               if (commentIds.Count>0)
                   pageList.ForEach(c=>c.IsZan=commentIds.Any(n => n == c.Comment.Id));
            }
            ViewBag.search = search;
            return PartialView(pageList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ActionResult Comment(CommentEntity entity)
        {
            return View(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns></returns>
        [HttpGet]
        [RoleAuthorize]
        public ActionResult CommentById(long commentId)
        {
            var entity=new CommentEntity {User = CurrentUserInfo, Comment = CommentsPublic.GetComment(commentId)};
            return View("Comment", entity);
        }

        /// <summary>
        /// 发布评论
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [RoleAuthorize]
        [UserDisableVerification]
        [EmailActivatedVerification]
        [ValidateInput(false)]
        public ActionResult Add(int id,string content)
        {
            if(id<0)
                return Json(new JsonResultModel { Message = "参数无效！" });
            if (string.IsNullOrEmpty(content) || content.Length <Site.Config.WebSysConfig.CommentMinLength)
                return Json(new JsonResultModel { Message = "评论内容太短了！" });
            if (content.Length > Site.Config.WebSysConfig.CommentMaxLength)
                return Json(new JsonResultModel { Message = "评论内容太长了！" });
            if(!CommentsPublic.CheckCanPost(CurrentUserInfo.Id,Site.Config.UserConfig.AddCommentInterval))
                return Json(new JsonResultModel { Message = "操作速度太快了,喝口水再试一下！" });
            var articles = Articles.GetArticleInfoById(id);
            if(articles.IsCloseComment)
                return Json(new JsonResultModel { Message = string.Format("评论已关闭！原因:{0}", articles.CloseCommentReason) });
            var comment = new CommentVModel()
            {
                UserId = CurrentUserInfo.Id,
                AId = id,
                IP = Common.Fetch.Ip,
                Content = content,
                UserAgent = Common.Fetch.UserAgent
            };
            var user = Users.GetUserById(CurrentUserInfo.Id);
            if (user == null || user.IsNull)
                return Json(new JsonResultModel { Message = "用户信息异常！" });
            var role = Roles.GetRole(user.RoleId);
            if (role == null || role.IsNull)
                return Json(new JsonResultModel { Message = "角色信息异常！" });
            //根据角色判断是否需要审核
            comment.State = (short)(role.CommentNeedVerified ? 0 : 1);


            long cid= CommentsPublic.AddComment(comment);
            if (cid <= 0)
                return Json(new JsonResultModel{Message = "评论失败！"});
            return Json(new JsonResultModel<long> { ResultState = true, Message = "评论成功!",Body = cid});
        }
    }
}
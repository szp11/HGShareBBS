using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HGShare.BBS.Controllers.Base;
using HGShare.BBS.Models;
using HGShare.Web.Interface;

namespace HGShare.BBS.Controllers
{
    /// <summary>
    /// 主结构
    /// </summary>
    public class HomeController : BaseController
    {
        private static readonly IArticles Articles = IocContainer.Service<IArticles>();
        private static readonly IArticleTypes ArticleTypes = IocContainer.Service<IArticleTypes>();
        private static readonly IUsers Users = IocContainer.Service<IUsers>();

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var list = new List<ArticleWebEntity>();

            int pageSize = Site.Config.PageConfig.WebArticlePageSize;
            int dataCount = 0;
            //最新文章
            var articlesList = Articles.SearchArticlesByTypes(0, 0, 1, pageSize,-1, out dataCount);
            if (articlesList != null)
            {
                //用户
                var userlist = Users.GetUsersByIds(articlesList.Select(n => n.UserId).ToArray());
                list.AddRange(from articleVModel in articlesList
                    let user = userlist.FirstOrDefault(n => n.Id == articleVModel.UserId)
                    select new ArticleWebEntity()
                    {
                        Article = articleVModel, User = user
                    });
            }
            return View(list);
        }
        /// <summary>
        /// 导航
        /// </summary>
        /// <returns></returns>
        public ActionResult HeaderNav(int? current)
        {
            var list = ArticleTypes.GetArticleTypesByIsHomeMenu(true);
            ViewBag.HeadNavCurrent =current;
            return PartialView(list);
        }
        /// <summary>
        /// 活跃用户
        /// </summary>
        /// <returns></returns>
        public ActionResult HotUsersTop()
        {
            var list = Users.CommentHotTop(30, 10);
            return View(list);
        }
        /// <summary>
        /// 近期热贴
        /// </summary>
        /// <returns></returns>
        public ActionResult HotDotArticleTop()
        {
            var list = Articles.ArticlesDotHotTop(30, 10);

            return View(list);
        }
        /// <summary>
        /// 近期热仪
        /// </summary>
        /// <returns></returns>
        public ActionResult HotCommentArticleTop()
        {
            var list = Articles.ArticlesCommentNumHotTop(30, 10);

            return View(list);
        }
        /// <summary>
        /// 友链
        /// </summary>
        /// <returns></returns>
        public ActionResult Links()
        {
            return View();
        }
        /// <summary>
        /// 二级分类
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public ActionResult ErJiNav(ArticleSearchEntity search)
        {
            return View(search);
        }
        /// <summary>
        /// 所有图标
        /// </summary>
        /// <returns></returns>
        public ActionResult Icos()
        {
            return View();
        }
        
    }
}
using HGShare.Common;
using Webdiyer.WebControls.Mvc;

namespace HGShare.Site.Config
{
    /// <summary>
    /// 分页配置
    /// </summary>
    public class PageConfig
    {
        #region manager
        /// <summary>
        /// 分页结构、样式配置
        /// </summary>
        public static PagerOptions BackstageOptions
        {
            get
            {
                return new PagerOptions
                {
                    ContainerTagName = "ul",
                    AutoHide = false,
                    PageIndexParameterName = "page",
                    CssClass = "pagination",
                    CurrentPagerItemTemplate =
                        "<li class=\"paginate_button active\"><a href=\"javascript:;\">{0}</a></li>",
                    DisabledPagerItemTemplate =
                        "<li class=\"paginate_button disabled\"><a href=\"javascript:;\">{0}</a></li>",
                    PagerItemTemplate = "<li class=\"paginate_button\">{0}</li>"
                };
            }
        }
        /// <summary>
        /// 页面大小
        /// </summary>
        public static int BackstagePageSize
        {
            get { return 15; }
        }
        #endregion

        #region bbs
        /// <summary>
        /// 文章列表页面显示内容配置
        /// </summary>
        public static int WebArticlePageSize
        {
            get
            {
                int page;
                int.TryParse(Configuration.AppSettings("WebArticlePageSize"), out page);
                return page;
            }
        }
        /// <summary>
        /// 文章列表分页结构、样式配置
        /// </summary>
        public static PagerOptions WebArticlePageOptions
        {
            get
            {
                return new PagerOptions
                {
                    ContainerTagName = "div",
                    PageIndexParameterName = "pageIndex",
                    CssClass = "laypage-main",
                    CurrentPagerItemTemplate =
                        "<span class=\"laypage-curr\" >{0}</span>",
                    DisabledPagerItemTemplate =
                        "<span>{0}</span>",
                    PagerItemTemplate = "{0}",
                    RouteName = "liebiao",
                    PageIndexOutOfRangeErrorMessage = ""
                };
            }
        }
        /// <summary>
        /// 评论列表分页结构、样式配置
        /// </summary>
        public static PagerOptions WebCommentPageOptions
        {
            get
            {
                return new PagerOptions
                {
                    ShowDisabledPagerItems=false,
                    ContainerTagName = "div",
                    PageIndexParameterName = "PageIndex",
                    CssClass = "laypage-main",
                    CurrentPagerItemTemplate =
                        "<span class=\"laypage-curr\" >{0}</span>",
                    DisabledPagerItemTemplate =
                        "<span>{0}</span>",
                    PagerItemTemplate = "{0}",
                    RouteName = "commentpage",
                    ControllerName = "Article",
                    ActionName = "Detail",
                    PageIndexOutOfRangeErrorMessage=""
                };
            }
        }
        /// <summary>
        /// 回复列表页面显示内容配置
        /// </summary>
        public static int WebCommentPageSize
        {
            get
            {
                int page;
                int.TryParse(Configuration.AppSettings("WebCommentPageSize"), out page);
                return page;
            }
        }
        /// <summary>
        /// 用户主页 文章条数
        /// </summary>
        public static int UserHomeArticlePageSize
        {
            get
            {
                int page;
                int.TryParse(Configuration.AppSettings("UserHomeArticlePageSize"), out page);
                return page;
            }
        }
        /// <summary>
        /// 用户主页 评论条数
        /// </summary>
        public static int UserHomeCommentPageSize
        {
            get
            {
                int page;
                int.TryParse(Configuration.AppSettings("UserHomeCommentPageSize"), out page);
                return page;
            }
        }
        /// <summary>
        /// 用户中心 文章条数
        /// </summary>
        public static int UserCenterArticlePageSize
        {
            get
            {
                int page;
                int.TryParse(Configuration.AppSettings("UserCenterArticlePageSize"), out page);
                return page;
            }
        }
        /// <summary>
        /// 用户中心 评论条数
        /// </summary>
        public static int UserCenterCommentPageSize
        {
            get
            {
                int page;
                int.TryParse(Configuration.AppSettings("UserCenterCommentPageSize"), out page);
                return page;
            }
        }

        /// <summary>
        /// 用户主页 文章列表分页结构、样式配置
        /// </summary>
        public static PagerOptions UserHomeCommentPageOptions
        {
            get
            {
                return new PagerOptions
                {
                    ShowDisabledPagerItems = false,
                    ContainerTagName = "div",
                    PageIndexParameterName = "cPageIndex",
                    CssClass = "laypage-main",
                    CurrentPagerItemTemplate =
                        "<span class=\"laypage-curr\" >{0}</span>",
                    DisabledPagerItemTemplate =
                        "<span>{0}</span>",
                    PagerItemTemplate = "{0}",
                    RouteName = "userhomepage",
                    ControllerName = "User",
                    ActionName = "Home",
                    PageIndexOutOfRangeErrorMessage = "",
                    NumericPagerItemCount =3
                };
            }
        }
        /// <summary>
        /// 用户主页 评论列表分页结构、样式配置
        /// </summary>
        public static PagerOptions UserHomeArticlePageOptions
        {
            get
            {
                return new PagerOptions
                {
                    ShowDisabledPagerItems = false,
                    ContainerTagName = "div",
                    PageIndexParameterName = "aPageIndex",
                    CssClass = "laypage-main",
                    CurrentPagerItemTemplate =
                        "<span class=\"laypage-curr\" >{0}</span>",
                    DisabledPagerItemTemplate =
                        "<span>{0}</span>",
                    PagerItemTemplate = "{0}",
                    RouteName = "userhomepage",
                    ControllerName = "User",
                    ActionName = "Home",
                    PageIndexOutOfRangeErrorMessage = "",
                    NumericPagerItemCount =3
                };
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HGShare.BBS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            //首页
            routes.MapRoute(
                name: "shouye",
                url: "",
                defaults: new{controller = "Home",action = "Index"}
            );
            //列表
            routes.MapRoute(
               name: "liebiao",
               url: "articles/{type}-{bType}-{isJingHua}-{pageIndex}",
               defaults: new { controller = "Article", action = "Index", type = 0, bType = 0,isJingHua=0, pageIndex = 1 }
           );
            //发帖
            routes.MapRoute(
               name: "fatie",
               url: "article/add",
               defaults: new { controller = "Article", action = "add" }
           );
            
            //详细页
            routes.MapRoute(
               name: "neirong",
               url: "article/{AId}",
               defaults: new { controller = "Article", action = "Detail", AId = UrlParameter.Optional },
               constraints: new { AId = @"\d+"}
           );
            //详细页-评论
            routes.MapRoute(
               name: "commentpage",
               url: "article/{AId}-{AuthorId}-{Order}-{PageIndex}",
               defaults: new { controller = "Article", action = "Detail", AId = 0, AuthorId = 0, Order = 0, PageIndex = 1 },
               constraints: new { AId = @"\d+", AuthorId = @"\d+", PageIndex = @"\d+", Order = @"\d+" }
           );
            //个人中心
            routes.MapRoute(
               name: "usercenter",
               url: "u",
               defaults: new { controller = "User", action = "UserCenter" }
           );
            //用户主页
            routes.MapRoute(
               name: "userhome",
               url: "u/{id}",
               defaults: new { controller = "User", action = "Home", id = UrlParameter.Optional },
               constraints: new { id = @"\d+" }
           );
            //用户主页
            routes.MapRoute(
               name: "userhomepage",
               url: "u/{id}-{aPageIndex}-{cPageIndex}",
               defaults: new { controller = "User", action = "Home", id = UrlParameter.Optional, aPageIndex = 1, cPageIndex=1 },
               constraints: new { id = @"\d+", cPageIndex = @"\d+", aPageIndex = @"\d+" }
           );
            //个人设置
            routes.MapRoute(
               name: "userset",
               url: "set",
               defaults: new { controller = "User", action = "UserSet"}
           );
            //登录
            routes.MapRoute(
              name: "login",
              url: "login",
              defaults: new { controller = "Vip", action = "Login"}
          );
            //登出
            routes.MapRoute(
              name: "logout",
              url: "logout",
              defaults: new { controller = "Vip", action = "LogOut" }
          );
            //注册
            routes.MapRoute(
              name: "reg",
              url: "reg",
              defaults: new { controller = "Vip", action = "Reg" }
          );
            //默认
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

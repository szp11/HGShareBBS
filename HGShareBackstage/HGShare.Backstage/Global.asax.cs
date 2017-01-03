using System.Web.Mvc;
using System.Web.Routing;
using HGShare.Container;

namespace HGShare.Backstage
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            IocContainer.RegisterServices();
        }
    }
}

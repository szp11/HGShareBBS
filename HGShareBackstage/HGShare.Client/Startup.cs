using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HGShare.Client.Startup))]
namespace HGShare.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

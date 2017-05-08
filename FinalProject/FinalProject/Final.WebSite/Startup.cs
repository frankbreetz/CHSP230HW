using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Final.WebSite.Startup))]
namespace Final.WebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

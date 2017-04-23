using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HW2HomePage.Startup))]
namespace HW2HomePage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

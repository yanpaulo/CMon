using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMon.WebApp.Startup))]
namespace CMon.WebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

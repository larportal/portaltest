using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LarpPortal.Startup))]
namespace LarpPortal
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}

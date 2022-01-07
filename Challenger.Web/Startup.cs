using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Challenger.Web.Startup))]
namespace Challenger.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

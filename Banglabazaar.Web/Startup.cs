using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Banglabazaar.Web.Startup))]
namespace Banglabazaar.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

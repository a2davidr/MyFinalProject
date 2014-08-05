using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DealershipApp.Startup))]
namespace DealershipApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

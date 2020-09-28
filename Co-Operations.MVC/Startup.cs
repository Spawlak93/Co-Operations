using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Co_Operations.MVC.Startup))]
namespace Co_Operations.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

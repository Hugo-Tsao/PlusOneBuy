using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FBPlusOneBuy.Startup))]
namespace FBPlusOneBuy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

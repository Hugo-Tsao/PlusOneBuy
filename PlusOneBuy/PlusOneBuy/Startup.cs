using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PlusOneBuy.Startup))]
namespace PlusOneBuy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

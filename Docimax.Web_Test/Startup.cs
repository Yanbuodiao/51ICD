using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Docimax.Web_Test.Startup))]
namespace Docimax.Web_Test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Docimax.Web_ICD.Startup))]
namespace Docimax.Web_ICD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

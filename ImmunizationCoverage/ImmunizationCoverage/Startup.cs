using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ImmunizationCoverage.Startup))]
namespace ImmunizationCoverage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InvestorsCRM.Startup))]
namespace InvestorsCRM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

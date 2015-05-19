using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortfolioGB.Startup))]
namespace PortfolioGB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

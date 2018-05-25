using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketPlace.Web6.Startup))]
namespace MarketPlace.Web6
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }


    }
}

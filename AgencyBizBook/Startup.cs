using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AgencyBizBook.Startup))]
namespace AgencyBizBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

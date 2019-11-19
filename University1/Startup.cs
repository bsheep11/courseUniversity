using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(University1.Startup))]
namespace University1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

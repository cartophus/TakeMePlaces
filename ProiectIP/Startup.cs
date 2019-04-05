using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProiectIP.Startup))]
namespace ProiectIP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

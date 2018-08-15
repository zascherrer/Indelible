using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Indelible.Startup))]
namespace Indelible
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

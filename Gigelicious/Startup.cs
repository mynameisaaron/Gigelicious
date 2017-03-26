using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gigelicious.Startup))]
namespace Gigelicious
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

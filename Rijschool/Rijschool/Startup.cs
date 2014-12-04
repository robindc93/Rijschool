using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Rijschool.Startup))]
namespace Rijschool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

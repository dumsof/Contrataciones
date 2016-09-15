using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Contrataciones.Startup))]
namespace Contrataciones
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

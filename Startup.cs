using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Commmunications.Startup))]
namespace Commmunications
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}

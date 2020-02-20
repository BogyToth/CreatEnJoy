using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreatEnJoy.Startup))]
namespace CreatEnJoy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

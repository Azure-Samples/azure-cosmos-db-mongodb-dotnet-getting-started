using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyTaskListApp.Startup))]
namespace MyTaskListApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

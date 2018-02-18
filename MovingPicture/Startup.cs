using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MovingPicture.Startup))]
namespace MovingPicture
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

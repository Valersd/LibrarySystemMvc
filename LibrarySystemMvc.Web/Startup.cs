using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LibrarySystemMvc.Web.Startup))]
namespace LibrarySystemMvc.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

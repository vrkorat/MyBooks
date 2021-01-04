using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UniqueBooks.Startup))]
namespace UniqueBooks
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

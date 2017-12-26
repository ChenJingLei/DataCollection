using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataCollection.Startup))]
namespace DataCollection
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GJ_BaseData_API.Startup))]

namespace GJ_BaseData_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseCors(CorsOptions.AllowAll);
            //app.MapSignalR("/GJ_BaseData_API/API/GJ_Driver/Hub", new HubConfiguration
            //{
            //    EnableJavaScriptProxies = true,
            //    EnableDetailedErrors = true
                
            //});
            
        }
    }
}

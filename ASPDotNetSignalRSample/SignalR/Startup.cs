using Microsoft.Owin;
using Owin;
using ASPDotNetSignalRSample;
[assembly: OwinStartup(typeof(ASPDotNetSignalRSample.SignalR.Startup))]
namespace ASPDotNetSignalRSample.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
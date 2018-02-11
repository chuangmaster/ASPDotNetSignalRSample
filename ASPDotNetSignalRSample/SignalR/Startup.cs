using Microsoft.Owin;
using Owin;
using ASPDotNetSignalRSample;
//允許CORS
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

[assembly: OwinStartup(typeof(ASPDotNetSignalRSample.SignalR.Startup))]
namespace ASPDotNetSignalRSample.SignalR
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //不允許跨站
            //app.MapSignalR();
            //允許跨站
            app.Map("/signalr", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguraion = new HubConfiguration
                {
                    EnableDetailedErrors = true
                };
                map.RunSignalR(hubConfiguraion);
            });
            GlobalHost.HubPipeline.AddModule(new TestPipelineModule());
            //GlobalHost.HubPipeline.RequireAuthentication();
        }
    }

    public class TestPipelineModule : HubPipelineModule
    {
        protected override bool OnBeforeIncoming(IHubIncomingInvokerContext context)
        {
            return base.OnBeforeIncoming(context);
        }

        protected override bool OnBeforeOutgoing(IHubOutgoingInvokerContext context)
        {
            return base.OnBeforeOutgoing(context);
        }
    }
}
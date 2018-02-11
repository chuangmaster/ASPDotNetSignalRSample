using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace ASPDotNetSignalRSample.SignalR
{
    public class BroadcastHub : Hub
    {
        List<string> connectionList = new List<string>();
        //允許Client端呼叫的方法
        public void SendMessage(string name, string message)
        {
            //取得自訂的屬性
            var userName = Clients.Caller.UserName;
            connectionList.Add(Context.ConnectionId);
            //Server端呼叫所有客戶端
            //Clients.All.SendMessage(name, message);

            //針對呼叫的人發送
            //Clients.Caller.SendMessage(name, message);

            //使用ClientProxy來做訊息發送
            IClientProxy clientProxy = Clients.All;
            clientProxy.Invoke("SendMessage", name, message);
        }

        public async Task SendAlert(string name)
        {
            IClientProxy clientProxy = Clients.All;
            await clientProxy.Invoke("SendAlert", new { name = name });
            Clients.All.Invoke("SendMessage", "系統", $"{name}上線了");
        }
        /// <summary>
        /// 生命週期 - On Connected
        /// </summary>
        /// <returns></returns>
        public override Task OnConnected()
        {
            return base.OnConnected();
        }
        /// <summary>
        /// 生命週期 - On DisConnected
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }
        /// <summary>
        /// 生命週期 - On Reconnected
        /// </summary>
        /// <returns></returns>
        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }
    }
}
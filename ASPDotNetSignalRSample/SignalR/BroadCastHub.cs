using System;
using System.Collections.Generic;
using System.Linq;
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
            connectionList.Add(Context.ConnectionId);
            //Server端呼叫所有客戶端
            //Clients.All.SendMessage(name, message);

            //針對呼叫的人發送
            //Clients.Caller.SendMessage(name, message);

            //使用ClientProxy來做訊息發送
            IClientProxy clientProxy = Clients.All;
            clientProxy.Invoke("SendMessage", new { name = name, message = message });
        }
    }
}
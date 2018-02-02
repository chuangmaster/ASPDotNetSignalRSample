using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ASPDotNetSignalRSample.SignalR
{
    public class BroadcastHub : Hub
    {
        //允許Client端呼叫的方法
        public void SendMessage(string name, string message)
        {
            //Server端呼叫所有客戶端
            Clients.All.SendMessage(name, message);
        }
    }
}
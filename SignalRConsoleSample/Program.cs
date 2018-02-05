using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            RunProxy();
            Console.Read();
        }
        public static async void RunProxy()
        {
            //建立連線
            var hubConnection = new HubConnection("http://localhost:55500/");
            //建立 Hub Proxy
            IHubProxy proxy = hubConnection.CreateHubProxy("broadcastHub");
            //註冊給伺服器端呼叫的方法
            proxy.On<string, string>("SendMessage", (name, message) => Console.WriteLine($"{name}:{message}"));

            //連線到 SignalR Server
            await hubConnection.Start();

            //執行Signal Server 方法
            await proxy.Invoke("SendMessage", "console", "Hi");
        }
    }
}

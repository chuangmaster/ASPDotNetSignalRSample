using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Transports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalRFormSample
{
    public partial class Form1 : Form
    {
        //建立連線
        HubConnection hubConnection;
        IHubProxy proxy;
        public Form1()
        {
            InitializeComponent();
        }

        delegate void InvokeMethod(string name, string message);

        private void Form1_Load(object sender, EventArgs e)
        {
            var queryString = new Dictionary<string, string>();
            queryString.Add("QS1", "參數1");
            queryString.Add("QS2", "參數2");
            hubConnection = new HubConnection("http://localhost:55500/", queryString);
            // 建立 Hub Proxy
            proxy = hubConnection.CreateHubProxy("broadcastHub");
            // 建立 Invoke內容
            InvokeMethod invoker = (name, message) => content.AppendText($"{name}:{message}\n");
            //註冊給伺服端呼叫的方法
            proxy.On<string, string>("SendMessage", (name, message) =>
            content.Invoke(invoker, name, message)
            );
            //增加Header
            hubConnection.Headers.Add("headerParameter", "parameter");
            //連線 SignalR Server；同時更換連線方式
            hubConnection.Start(new LongPollingTransport()).Wait();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //執行 SinalR Server Method
            await proxy.Invoke("SendMessage", UserName.Text, Message.Text);
        }


    }
}

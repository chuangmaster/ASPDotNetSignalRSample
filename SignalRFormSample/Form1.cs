using Microsoft.AspNet.SignalR.Client;
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
        HubConnection hubConnection = new HubConnection("http://localhost:55500/");
        IHubProxy proxy;
        public Form1()
        {
            InitializeComponent();
        }

        delegate void InvokeMethod(string name, string message);

        private void Form1_Load(object sender, EventArgs e)
        {
            // 建立 Hub Proxy
            proxy = hubConnection.CreateHubProxy("broadcastHub");
            // 建立 Invoke內容
            InvokeMethod invoker = (name, message) => content.AppendText($"{name}:{message}\n");
            //註冊給伺服端呼叫的方法
            proxy.On<string, string>("SendMessage", (name, message) =>
            content.Invoke(invoker, name, message)
            );
            //連線 SignalR Server
            hubConnection.Start().Wait();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            //執行 SinalR Server Method
            await proxy.Invoke("SendMessage", UserName.Text, Message.Text);
        }


    }
}

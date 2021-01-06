using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatsonWebsocket;

namespace RTD_Wormhole
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        readonly RtdClient RTDclient;
        private WatsonWsServer LinkServer;
        private WatsonWsClient LinkClient;

        public Form1()
        {
            InitializeComponent();
            toolStrip.ImageList = imageList;
            synchronizationContext = SynchronizationContext.Current;
            tb_srv_ip.Text = GetLocalIp();
            RTDclient = new RtdClient();
            RTDclient.EDisconnect += new EventHandler(Client_OnDisconnect);
            RTDclient.HeartBeatLost += new EventHandler(Client_OnHeartBeatLost);
            RTDclient.Data += new EventHandler<DataEventArgs>(Client_OnData);

        }

        /// <summary>
        /// safely access UI from other threads
        /// </summary>
        /// <param name="action"></param>
        public void PostUI(Action action)
        {
            synchronizationContext.Post(new SendOrPostCallback(o => action()), null);
        }

        private string GetLocalIp()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable()) return "0.0.0.0";
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                return endPoint.Address.ToString();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // shutdown server
            Application.Exit();
        }

        private void btn_server_Click(object sender, EventArgs e)
        {
            // Connect RTD
            if (!RTDclient.Connected)
            {
                srv_status.Text = "Connecting to RTD server...";
                AppendLog("Connecting to RTD server...");
                server_rtd_status.Image = imageList.Images[1];
                ConnectRTD();
            }
            else
            {
                AppendLog("Disconnecting from RTD server...");
                RTDclient.Disconnect();
                server_rtd_status.Image = imageList.Images[0];
                srv_status.Text = "RTD server disconnected.";
                AppendLog("RTD server disconnected.");
            }
            // Start Server
            AppendLog("Starting Wormhole server...");
            // DEBUG: TODO Change from Localhost
            LinkServer = new WatsonWsServer(tb_client_ip.Text, Decimal.ToInt32(ud_srv_port.Value), false);
            LinkServer.ClientConnected += WSClientConnected;
            //server.ClientDisconnected += ClientDisconnected;
            LinkServer.MessageReceived += MessageReceived;
            LinkServer.Start();
            server_ws_status.Image = imageList.Images[2];
            server_link_status.Image = imageList.Images[1];
            srv_status.Text = "Wormhole server running. Awaiting connection...";
            AppendLog("Wormhole server running. Awaiting connection...");

        }

        void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Object data = ByteArrayToObject(args.Data);
            if (data is string)
                AppendLog("Text message received from " + args.IpPort + ": " + data, false);
            else if (data is object[])
            {
                AppendLog("Array object", false);
                // this is where the RTD-message should be handled
            }
            else
                AppendLog("Unknown object");
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            LinkClient = new WatsonWsClient(tb_client_ip.Text, Decimal.ToInt32(ud_client_port.Value), false);
            LinkClient.ServerConnected += ServerConnected;
            LinkClient.ServerDisconnected += ServerDisconnected;
            //client.MessageReceived += MessageReceived;
            LinkClient.Start();

            client_ws_status.Image = imageList.Images[2];
        }

        void ServerConnected(object sender, EventArgs args)
        {
            AppendLog("Server connected");
        }

        void ServerDisconnected(object sender, EventArgs args)
        {
            AppendLog("Server disconnected");
        }

        void WSClientConnected(object sender, ClientConnectedEventArgs args)
        {
            PostUI(() =>
            {
                server_link_status.Image = imageList.Images[2];
            });
        }

        void Client_OnHeartBeatLost(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[1];
                srv_status.Text = "RTD Heartbeat lost. Reconnecting...";
                AppendLog("RTD Heartbeat lost. Reconnecting...");
                System.Threading.Tasks.Task.Delay(10 * 1000).ContinueWith((_) => PostUI(() =>
            { ConnectRTD(); }));
            });
        }

        private void ConnectRTD()
        {
            bool ret = RTDclient.Connect("xrtd.xrtd", -1);

                if (ret)
                {
                    srv_status.Text = "RTD Connected.";
                AppendLog("RTD Connected.");
                server_rtd_status.Image = imageList.Images[2];
                }
                else
                {
                    srv_status.Text = "RTD Connection failed.";
                    AppendLog("RTD Connection failed.");
                    server_rtd_status.Image = imageList.Images[1];
                }

        }

        void Client_OnDisconnect(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[0];
                srv_status.Text = "RTD disconnected.";
                AppendLog("RTD disconnected.");
            });
        }

        void Client_OnData(object sender, DataEventArgs e)
        {
            // push through warmhole until success 
            // nothing bad can happen if the same message is repeated
            LinkClient.SendAsync(e.Data);
            AppendLog("Client recieved data event");
        }

        /// <summary>
        /// tests the connection between client and server via the Watson-link
        /// </summary>
        private async Task<bool> TestConnectionAsync()
        {
            if (LinkClient != null)
            {
                return await LinkClient.SendAsync(ObjectToByteArray("Test message from client"));
            }
            else
            {
                return false;
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            AppendLog("Sending test message to server " + tb_client_ip.Text + ":" + Decimal.ToInt32(ud_srv_port.Value));
            btn_testConnection.Image = global::RTD_Wormhole.Properties.Resources.disconnected;
            Task<bool> testCall = TestConnectionAsync();
            bool testSuccess = await testCall;
            if (testSuccess)
            {
                AppendLog("Test message was successfully received by server");
                btn_testConnection.Image = global::RTD_Wormhole.Properties.Resources.connected;
            } else
            {
                AppendLog("Test message could not be sent to server");
            }
        }

        private static string LogStatement(string logText)
        {
            return "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] " + logText + Environment.NewLine;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] byteData = ObjectToByteArray("test byte message");
            DataEventArgs deArgs = new DataEventArgs(1, byteData);
            Client_OnData(sender, deArgs);
        }

        // Convert a byte array to an Object
        public static Object ByteArrayToObject(byte[] arrBytes)
        {
            using (var memStream = new MemoryStream())
            {
                var binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                var obj = binForm.Deserialize(memStream);
                return obj;
            }
        }

        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public void AppendLog(string logText, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { AppendLog(logText); }));
            }
            else
            {
                DateTime timestamp = DateTime.Now;
                textBox1.AppendText(LogStatement(logText));
            }
        }
    }
}

using System;
using System.Net;
using System.Net.Sockets;
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
                server_rtd_status.Image = imageList.Images[1];
                ConnectRTD();
            }
            else
            {
                srv_status.Text = "Disconnecting RTD server...";
                RTDclient.Disconnect();
                server_rtd_status.Image = imageList.Images[0];
                srv_status.Text = "RTD server disconnected.";
            }
            // Start Server
            srv_status.Text = "Starting Wormhole server...";
            // DEBUG: TODO Change from Localhost
            WatsonWsServer server = new WatsonWsServer(tb_client_ip.Text, Decimal.ToInt32(ud_srv_port.Value), false);
            server.ClientConnected += WSClientConnected;
            //server.ClientDisconnected += ClientDisconnected;
            server.MessageReceived += MessageReceived;
            server.Start();
            server_ws_status.Image = imageList.Images[2];
            server_link_status.Image = imageList.Images[1];
            srv_status.Text = "Wormhole server running. Awaiting connection...";

        }

        static void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            Console.WriteLine("Message received from " + args.IpPort + ": " + Encoding.UTF8.GetString(args.Data));
        }

        private void btn_client_Click(object sender, EventArgs e)
        {
            LinkClient = new WatsonWsClient(tb_client_ip.Text, Decimal.ToInt32(ud_client_port.Value), false);
            //client.ServerConnected += ServerConnected;
            //client.ServerDisconnected += ServerDisconnected;
            //client.MessageReceived += MessageReceived;
            LinkClient.Start();

            client_ws_status.Image = imageList.Images[2];
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
                    server_rtd_status.Image = imageList.Images[2];
                }
                else
                {
                    srv_status.Text = "RTD Connection failed.";
                    server_rtd_status.Image = imageList.Images[1];
                }

        }

        void Client_OnDisconnect(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[0];
                srv_status.Text = "RTD disconnected.";
            });
        }

        void Client_OnData(object sender, DataEventArgs e)
        {
            // push through warmhole until success 
            // nothing bad can happen if the same message is repeated
        }

        /// <summary>
        /// tests the connection between client and server via the Watson-link
        /// </summary>
        private async Task<bool> TestConnectionAsync()
        {
            if (LinkClient != null)
            {
                return await LinkClient.SendAsync("Test message from client");
            }
            else
            {
                return false;
            }
        }

        private async void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.AppendText(LogStatement("Sending test message to server"));
            btn_testConnection.Image = global::RTD_Wormhole.Properties.Resources.disconnected;
            Task<bool> testCall = TestConnectionAsync();
            bool testSuccess = await testCall;
            if (testSuccess)
            {
                textBox1.AppendText(LogStatement("Test message was successfully received by server"));
                btn_testConnection.Image = global::RTD_Wormhole.Properties.Resources.connected;
            } else
            {
                textBox1.AppendText(LogStatement("Test message could not be sent to server"));
            }
        }

        private string LogStatement(string logText)
        {
            return "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] " + logText + Environment.NewLine;
        }
    }
}

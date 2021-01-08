using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fleck; // ws server
using WatsonWebsocket; // wsclient


namespace RTD_Wormhole
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        private WebSocketServer LinkServer;
        private WatsonWsClient LinkClient;
        private readonly List<IWebSocketConnection> Connections;
        bool clientconnecting = false;
        //TODO private readonly List<RtdClient> RTDclients;

        public Form1()
        {
            InitializeComponent();
            toolStrip.ImageList = imageList;
            synchronizationContext = SynchronizationContext.Current;
            tb_srv_ip.Text = GetLocalIp();
            tb_client_ip.Text = GetLocalIp();
            Connections = new List<IWebSocketConnection>();
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

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: shutdown server, client and all active connections
            Application.Exit();
        }

        // LinkServer
        private void Btn_server_Click(object sender, EventArgs e)
        {
            // start/stop websocket server
            if (LinkServer != null)
            {
                AppendLog("Stopping Wormhole server...");
                foreach (IWebSocketConnection connection in Connections.ToArray()) connection.Close();
                server_link_status.Image = imageList.Images[0];
                LinkServer.Dispose();
                LinkServer = null;
                btn_server.Image = imageList.Images[0];
                server_ws_status.Image = imageList.Images[0];             
                AppendLog("Wormhole server down.");
            } else {
                AppendLog("Starting Wormhole server...");
                LinkServer = StartWebSocketServer(tb_srv_ip.Text, Decimal.ToInt32(ud_srv_port.Value));
                btn_server.Image = imageList.Images[2];
                server_ws_status.Image = imageList.Images[2];
                server_link_status.Image = imageList.Images[1];
                AppendLog("Wormhole server running. Awaiting connections...");
            }
        }

        private WebSocketServer StartWebSocketServer(string ip, int port)
        {
            WebSocketServer server = new WebSocketServer("ws://" + ip + ":" + port, true);
            server.Start(socket =>
                {
                    socket.OnOpen = () => ServerOnOpen(socket);
                    socket.OnClose = () => ServerOnClose(socket);
                    socket.OnMessage = message => ServerOnMessage(socket, message);
                    socket.OnBinary = data => ServerOnBinary(socket, data);
                });
            return server;
        }

        private void ServerOnOpen(IWebSocketConnection socket)
        {
            Connections.Add(socket);
            ChangeWebSocketStatus("server_link_status", 2);
            UpdateConnections(Connections.Count);
            AppendLog("Client connected: " + socket.ConnectionInfo.ToString());
            AppendLog("Starting RTD for client: " + socket.ConnectionInfo.ToString());
            
            // TODO client needs link to socket
            RtdClient client = new RtdClient();
            client.EDisconnect += new EventHandler(Client_OnDisconnect);
            client.HeartBeatLost += new EventHandler(Client_OnHeartBeatLost);
            client.Data += new EventHandler<DataEventArgs>(Client_OnData);
            // TODO connect client, put in list
        }

        private void ServerOnClose(IWebSocketConnection socket)
        {
            Connections.Remove(socket);
            UpdateConnections(Connections.Count);
            if (Connections.Count == 0) ChangeWebSocketStatus("server_link_status", 1);
            AppendLog("Client disconnected: " + socket.ConnectionInfo.ToString());
            // TODO disconnect client
        }

        private void ServerOnMessage(IWebSocketConnection socket, string message)
        {
            AppendLog("Websocket server received text message. Echo...");
            socket.Send(message);
        }

        private void ServerOnBinary(IWebSocketConnection socket, byte[] data)
        {
            AppendLog("Websocket server received binary message. Echo...");
            socket.Send(data);
        }

        // LinkClient
        private void Btn_client_Click(object sender, EventArgs e)
        {
            // allow only one client
            if (clientconnecting) return;
            if (LinkClient != null)
            {
                AppendLog("Stopping Wormhole client...");
                if (LinkClient.Connected)
                {
                    LinkClient.Stop();
                }
                else
                {
                    LinkClient.Dispose();
                    LinkClient = null;
                }
                ChangeWebSocketStatus("client_link_status", 0);
            } else
            {
                AppendLog("Starting Wormhole client...");
                clientconnecting = true;
                LinkClient = new WatsonWsClient(tb_client_ip.Text, Decimal.ToInt32(ud_client_port.Value), false);
                LinkClient.ServerConnected += LinkClientConnected;
                LinkClient.ServerDisconnected += LinkClientDisconnected;
                LinkClient.MessageReceived += LinkClientMessageReceived;
                ChangeWebSocketStatus("client_link_status", 1);
                LinkClient.Start();
            }
        }
        void LinkClientConnected(object sender, EventArgs args)
        {
            ChangeWebSocketStatus("client_link_status", 2);
            AppendLog("Client connected to server.");
            clientconnecting = false;
        }

        void LinkClientDisconnected(object sender, EventArgs args)
        {
            LinkClient.Dispose();
            LinkClient = null;
            ChangeWebSocketStatus("client_link_status", 0);
            AppendLog("Client not connected.");
            clientconnecting = false;
        }
        void LinkClientMessageReceived(object sender, MessageReceivedEventArgs args)
        {
           
            if (args.MessageType == System.Net.WebSockets.WebSocketMessageType.Text)
                AppendLog("Client received text message from " + args.IpPort + ": " + Encoding.UTF8.GetString(args.Data), false);
            else if (args.MessageType == System.Net.WebSockets.WebSocketMessageType.Binary)
            {
                Object data = ByteArrayToObject(args.Data); //TODO evt try falls es doch kein object[] ist
                AppendLog("Client received object[] from " + args.IpPort + ": " + data, false);
                // this is where the RTD-message should be handled
            }
            else
                AppendLog("Client received unknown message type " + args.IpPort + ": " + args.MessageType.ToString());
        }

        // RTD Client

        void Client_OnHeartBeatLost(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[1];
                AppendLog("RTD Heartbeat lost. Reconnecting...");
                System.Threading.Tasks.Task.Delay(10 * 1000).ContinueWith((_) => PostUI(() =>
            { /*ConnectRTD();*/ }));
            });
        }
        /*
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

        }*/

        void Client_OnDisconnect(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[0];
                AppendLog("RTD disconnected.");
            });
        }

        void Client_OnData(object sender, DataEventArgs e)
        {
            // push through warmhole until success 
            // nothing bad can happen if the same message is repeated
            LinkClient.SendAsync(e.Data);
            AppendLog("RTDClient recieved data event. Forwarding to Websocket server.");
        }

        /// <summary>
        /// tests the connection between client and server via the Watson-link
        /// </summary>
        private async Task<bool> TestConnectionAsync()
        {
            if (LinkClient != null && LinkClient.Connected)
            {
                return await LinkClient.SendAsync(ObjectToByteArray("Client says: Hi Server!"));
            }
            else
            {
                return false;
            }
        }

        private async void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AppendLog("Sending test message to server " + tb_client_ip.Text + ":" + Decimal.ToInt32(ud_client_port.Value));
            btn_testConnection.Image = global::RTD_Wormhole.Properties.Resources.disconnected;
            Task<bool> testCall = TestConnectionAsync();
            bool testSuccess = await testCall;
            if (testSuccess)
            {
                AppendLog("Test message was successfully send to server");
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

        public void ChangeWebSocketStatus(string target, int status, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                     delegate () { ChangeWebSocketStatus(target, status, debug); }));
            }
            else
            {
                switch (target)
                {
                    case "server_link_status":
                        server_link_status.Image = imageList.Images[status];
                        break;
                    case "client_link_status":
                        client_ws_status.Image = imageList.Images[status];
                        btn_client.Image = imageList.Images[status];
                        break;
                }
            }
        }

        public void UpdateConnections(int count)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { UpdateConnections(count); }));
            }
            else
            {
                lbl_conn.Text = "Link (" + count.ToString() + " Connections)";
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

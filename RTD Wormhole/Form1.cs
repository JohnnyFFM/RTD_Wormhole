using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using Fleck; // ws server

namespace RTD_Wormhole
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        private WebSocketServer LinkServer;   
        private readonly Dictionary<IWebSocketConnection, RtdClient> Connections = new Dictionary<IWebSocketConnection, RtdClient>();
        private readonly Dictionary<RtdClient, IWebSocketConnection> ReverseConnections = new Dictionary<RtdClient, IWebSocketConnection>();

        public Form1()
        {
            InitializeComponent();
            synchronizationContext = SynchronizationContext.Current;
            toolStrip.ImageList = imageList;
            tb_srv_ip.Text = Helper.GetLocalIp();
        }

        // LinkServer
        private void Btn_server_Click(object sender, EventArgs e)
        {
            // start/stop websocket server
            if (LinkServer != null)
            {
                AppendLog("Stopping Wormhole server...");
                foreach (IWebSocketConnection connection in Connections.Keys) connection.Close();
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
            server.RestartAfterListenError = true;
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

            AppendLog("Client connected: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString());
            AppendLog("Starting RTD for client: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString()); 
            ChangeConnectionStatus("server_link_status", 2);

            // create RTD client for connection
            RtdClient client = new RtdClient();
            client.EConnect += new EventHandler(Client_OnConnect);
            client.EDisconnect += new EventHandler(Client_OnDisconnect);
            client.HeartBeatLost += new EventHandler(Client_OnHeartBeatLost);
            client.Data += new EventHandler<DataEventArgs>(Client_OnData);
            Connections.Add(socket, client);
            ReverseConnections.Add(client, socket);
            UpdateConnections(Connections.Count);         
            ConnectRTD(client);          
        }

        private void ServerOnClose(IWebSocketConnection socket)
        {
            Connections[socket].Disconnect();
            ReverseConnections.Remove(Connections[socket]);
            Connections.Remove(socket);
            UpdateConnections(Connections.Count);
            UpdateConnections2(Connections.Count);
            if (Connections.Count == 0)
            {
                ChangeConnectionStatus("server_link_status", 1);
            }
            AppendLog("Client disconnected: " + socket.ConnectionInfo.ToString());
        }

        private void ServerOnMessage(IWebSocketConnection socket, string message)
        {
            AppendLog("Websocket server received text message. Echo...");
            socket.Send(message);
        }

        private void ServerOnBinary(IWebSocketConnection socket, byte[] data)
        {
            AppendLog("Websocket server received binary message.");

            // deserialise
            object incoming = Helper.ByteArrayToObject(data);
            RtdClient client = Connections[socket];
            switch (incoming){
                case SubscriptionRequest sr:
                    AppendLog("Message is a subscription request. Forwarding to RTD...");   
                    if (client.Connected)
                    {
                        client.Subscribe(sr.topicID, sr.topicParams);
                    } else
                    {
                        AppendLog("Error: RTD server not available!");
                        socket.Send("Error: RTD server not available!");
                    }
                    
                    break;
                case CancelRequest cr:
                    AppendLog("Message is a subscription cancellation request. Forwarding to RTD...");
                    if (client.Connected)
                    {
                        client.Unsubscribe(cr.topicID);
                    }
                    else
                    {
                        AppendLog("Error: RTD server not available!");
                        socket.Send("Error: RTD server not available!");
                    }
                    break;
                default:
                    AppendLog("Message type unknown.");
                    break;
            }          
        }

        // RTD Client

        void Client_OnHeartBeatLost(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[1];
                AppendLog("RTD Heartbeat lost. Reconnecting...");
                ReverseConnections[(RtdClient)sender].Send("RTD connection lost. Reconnecting...");
                System.Threading.Tasks.Task.Delay(10 * 1000).ContinueWith((_) => PostUI(() =>
            { ConnectRTD((RtdClient)sender); }));
            });
        }

        private void ConnectRTD(RtdClient RTDclient)
        {
            AppendLog("RTD connecting...");
            ChangeConnectionStatus("rtd_link_status", 1);
            bool ret = RTDclient.Connect("xrtd.xrtd", -1);

            if (!ret)
            {
                AppendLog("RTD Connection failed.");
                ChangeConnectionStatus("rtd_link_status", 0);
            }
        }
        void Client_OnConnect(object sender, EventArgs e)
        {
                AppendLog("RTD connected.");
                ReverseConnections[(RtdClient)sender].Send("RTD connected.");
                ChangeConnectionStatus("rtd_link_status", 2);
                UpdateConnections2(Connections.Count);
        }

        void Client_OnDisconnect(object sender, EventArgs e)
        {
            AppendLog("RTD disconnected.");
            if (Connections.Count == 1) ChangeConnectionStatus("rtd_link_status", 0);
        }

        void Client_OnData(object sender, DataEventArgs e)
        {
            AppendLog("RTDClient recieved data event. Forwarding to Websocket client.");
            RTDdata data = new RTDdata(e.Count, e.Data);
            byte[] datab = Helper.ObjectToByteArray(data);
            ReverseConnections[(RtdClient)sender].Send(datab); 
        }

        // UI

        /// <summary>
        /// safely access UI from other threads
        /// </summary>
        /// <param name="action"></param>
        public void PostUI(Action action)
        {
            synchronizationContext.Post(new SendOrPostCallback(o => action()), null);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: shutdown server, client and all active connections
            Application.Exit();
        }

        private static string LogStatement(string logText)
        {
            return "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] " + logText + Environment.NewLine;
        }

        public void ChangeConnectionStatus(string target, int status, bool debug = false)
        {
            if (debug)
                return;
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                     delegate () { ChangeConnectionStatus(target, status, debug); }));
            }
            else
            {
                switch (target)
                {
                    case "server_link_status":
                        server_link_status.Image = imageList.Images[status];
                        break;
                    case "rtd_link_status":
                        server_rtd_status.Image = imageList.Images[status];
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
                lbl_conn.Text = "Link (" + count.ToString() + " connections)";
            }
        }

        public void UpdateConnections2(int count)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(
                    new MethodInvoker(
                    delegate () { UpdateConnections2(count); }));
            }
            else
            {
                lbl_rtd.Text = "RTDclient (" + count.ToString() + " connections) <-->";
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
                tb_log.AppendText(LogStatement(logText));
            }
        }
    }

    [Serializable]
    struct SubscriptionRequest
    {
        public readonly int topicID;
        public readonly object[] topicParams;

        public SubscriptionRequest(int x, object[] y)
        {
            this.topicID = x;
            this.topicParams = y;
        }
    }

    [Serializable]
    struct RTDdata
    {
        public readonly int count;
        public readonly object[,] data;

        public RTDdata(int x, object[,] y)
        {
            this.count = x;
            this.data = y;
        }
    }

    [Serializable]
    struct CancelRequest
    {
        public readonly int topicID;

        public CancelRequest(int x)
        {
            this.topicID = x;
        }
    }
}

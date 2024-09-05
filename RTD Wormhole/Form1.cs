using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Fleck; // ws server
using NLog;

namespace RTD_Wormhole
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        private WebSocketServer LinkServer;
        private readonly Dictionary<IWebSocketConnection, ConnectionData> Connections = new Dictionary<IWebSocketConnection, ConnectionData>();
        private readonly Dictionary<RtdClient, IWebSocketConnection> ReverseConnections = new Dictionary<RtdClient, IWebSocketConnection>();
        private readonly object connectionLock = new object();
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
            var richTextBoxTarget = LogManager.Configuration.FindTargetByName<RichTextBoxTarget>("richtextbox");
            if (richTextBoxTarget != null)
            {
                richTextBoxTarget.RichTextBox = rtb_log; // Das RichTextBox-Steuerelement zuweisen
            }
            logger.Info("RTD Wormhole Server Application started");
            synchronizationContext = SynchronizationContext.Current;
            toolStrip.ImageList = imageList;
            tb_srv_ip.Text = Helper.GetLocalIp();
            // auto-start
            Btn_server_Click(null, null);
        }

        // LinkServer
        private void Btn_server_Click(object sender, EventArgs e)
        {
            // start/stop websocket server
            if (LinkServer != null)
            {
                logger.Info("Stopping Wormhole server...");
                foreach (IWebSocketConnection connection in Connections.Keys) connection.Close();
                server_link_status.Image = imageList.Images[0];
                LinkServer.Dispose();
                LinkServer = null;
                btn_server.Image = imageList.Images[0];
                server_ws_status.Image = imageList.Images[0];
                logger.Info("Wormhole server down.");
            }
            else
            {
                logger.Info("Starting Wormhole server...");
                LinkServer = StartWebSocketServer(tb_srv_ip.Text, Decimal.ToInt32(ud_srv_port.Value));
                btn_server.Image = imageList.Images[2];
                server_ws_status.Image = imageList.Images[2];
                server_link_status.Image = imageList.Images[1];
                logger.Info("Wormhole server running. Awaiting connections...");
            }
        }

        private WebSocketServer StartWebSocketServer(string ip, int port)
        {
            WebSocketServer server = new WebSocketServer("ws://" + ip + ":" + port, true)
            {
                RestartAfterListenError = true
            };
            server.Start(socket =>
                {
                    socket.OnOpen = () => ServerOnOpen(socket);
                    socket.OnClose = () => ServerOnClose(socket);
                    socket.OnMessage = message => ServerOnMessage(socket, message);
                    socket.OnBinary = data => ServerOnBinary(socket, data);
                    socket.OnError = (ex) => ServerOnError(socket, ex); // Handle errors
                });
            return server;
        }

        private void ServerOnError(IWebSocketConnection socket, Exception ex)
        {
            logger.Error("WebSocket error: " + ex.Message);
            socket.Close(); // Close the socket in case of error
            lock (connectionLock)
            {
                if (Connections.ContainsKey(socket))
                {
                    Connections[socket].Client.Disconnect();
                    string client = $"{socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}";
                    logger.Info($"RTD disconnected for client {client}");
                    ReverseConnections.Remove(Connections[socket].Client);
                    Connections.Remove(socket);
                    UpdateConnections(Connections.Count);
                    UpdateRTDConnections(Connections.Count);

                    if (Connections.Count == 0)
                    {
                        ChangeConnectionStatus("server_link_status", 1);
                    }
                    logger.Info("Client disconnected: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString());
                }
            }
        }

        private void ServerOnOpen(IWebSocketConnection socket)
        {
            lock (connectionLock)
            {
                logger.Info("Client connected: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString());
                logger.Info("Starting RTD for client: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString());
                ChangeConnectionStatus("server_link_status", 2);

                // create RTD client for connection
                RtdClient client = new RtdClient();
                client.EConnect += new EventHandler(Client_OnConnect);
                client.EDisconnect += new EventHandler(Client_OnDisconnect);
                client.HeartBeatLost += new EventHandler(Client_OnHeartBeatLost);
                client.Data += new EventHandler<DataEventArgs>(Client_OnData);

                // Add to Connections and ReverseConnections inside the lock
                Connections.Add(socket, new ConnectionData { Client = client, Delay = TimeSpan.Zero });
                ReverseConnections.Add(client, socket);

                UpdateConnections(Connections.Count);
                ConnectRTD(client, $"{socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}");
            }
        }

        private void ServerOnClose(IWebSocketConnection socket)
        {
            lock (connectionLock)
            {
                if (Connections.ContainsKey(socket))
                {
                    Connections[socket].Client.Disconnect();
                    string client = $"{socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}";
                    logger.Info($"RTD disconnected for client {client}");
                    ReverseConnections.Remove(Connections[socket].Client);
                    Connections.Remove(socket);
                    UpdateConnections(Connections.Count);
                    UpdateRTDConnections(Connections.Count);

                    if (Connections.Count == 0)
                    {
                        ChangeConnectionStatus("server_link_status", 1);
                    }
                    logger.Info("Client disconnected: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString());
                }
            }
        }

        private void ServerOnMessage(IWebSocketConnection socket, string message)
        {
            try
            {
                logger.Info("Websocket server received text message. Echo...");
                socket.Send(message);
            }
            catch (Exception ex)
            {
                logger.Error("Error processing message: " + ex.Message);
                socket.Close();
            }
        }

        private void ServerOnBinary(IWebSocketConnection socket, byte[] data)
        {
            // deserialise
            object incoming = Helper.ByteArrayToObject(data);
            RtdClient client = Connections[socket].Client;
            switch (incoming)
            {
                case SubscriptionRequest sr:
                    if (client.Connected)
                    {
                        logger.Debug($"↓[SUB] Client: {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}, ID: {sr.topicID}, Data: {string.Join(";", sr.topicParams)})");
                        ToServerTime(socket, sr.topicParams);

                        client.Subscribe(sr.topicID, sr.topicParams);
                    }
                    else
                    {
                        logger.Error("Error: RTD server not available, subscribe failed!");
                        socket.Send("Error: RTD server not available, subscribe failed!");
                    }

                    break;
                case CancelRequest cr:
                    if (client.Connected)
                    {
                        logger.Debug($"↓[CXL] Client: {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}, ID: {cr.topicID})");
                        client.Unsubscribe(cr.topicID);
                    }
                    else
                    {
                        logger.Error("Error: RTD server not available, unsubscribe failed!");
                        socket.Send("Error: RTD server not available, unsubscribe failed!");
                    }
                    break;
                default:
                    logger.Error("Error: Binary Message type unknown.");
                    break;
            }
        }

        void ToServerTime(IWebSocketConnection socket, object[] sr)
        {
            for (int i = 0; i < sr.Length; i++)
            {
                if (sr[i].ToString().StartsWith("REQUESTTIME"))
                {
                    DateTime servertime = DateTime.Now;
                    DateTime clienttime = DateTime.FromOADate(Double.Parse(sr[i].ToString().Split('=')[1]));
                    Connections[socket].Delay = clienttime - servertime;
                    sr[i] = "REQUESTTIME=" + servertime.ToOADate().ToString();
                }
            }
        }

        void ToClientTime(IWebSocketConnection socket, object[,] sr)
        {
            for (int i = 0; i < sr.GetLength(1); i++)
            {
                if (sr[1, i] is DateTime)
                {

                    sr[1, i] = ((DateTime)sr[1, i]).Add(Connections[socket].Delay);
                }
            }
        }
        // RTD Client

        void Client_OnHeartBeatLost(object sender, EventArgs e)
        {
            lock (connectionLock)
            {
                PostUI(() =>
                {
                    server_rtd_status.Image = imageList.Images[1];
                    logger.Warn("RTD Heartbeat lost. Reconnecting...");
                    string client = string.Empty;
                    if (ReverseConnections.TryGetValue((RtdClient)sender, out var socket))
                    {
                        socket.Send("RTD connection lost. Reconnecting...");
                        client = $"s{socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}";
                        System.Threading.Tasks.Task.Delay(10 * 1000).ContinueWith((_) => PostUI(() =>
                        {
                            ConnectRTD((RtdClient)sender, client);
                        }));
                    } else
                    {
                        logger.Error("Heartbeat loss on unknown RTD.");
                    }
                });
            }
        }

        private void ConnectRTD(RtdClient RTDclient, string client)
        {
            ChangeConnectionStatus("rtd_link_status", 1);
            bool ret = RTDclient.Connect("xrtd.xrtd", -1);

            if (!ret)
            {
                logger.Error($"RTD Connection failed for client {client}");
                ChangeConnectionStatus("rtd_link_status", 0);
            }
        }
        void Client_OnConnect(object sender, EventArgs e)
        {
            lock (connectionLock)
            {
                string client = string.Empty;
                if (ReverseConnections.TryGetValue((RtdClient)sender, out var socket))
                {
                    socket.Send("RTD connected.");
                    client = $"{socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}";
                } else
                {
                    logger.Error("Connected unknown RTD.");
                }
                logger.Info($"RTD connected for {client}");
                ChangeConnectionStatus("rtd_link_status", 2);
                UpdateRTDConnections(Connections.Count);
            }
        }

        void Client_OnDisconnect(object sender, EventArgs e)
        {
            lock (connectionLock)
            {
                if (Connections.Count == 1)
                {
                    ChangeConnectionStatus("rtd_link_status", 0);
                }
            }
        }

        void Client_OnData(object sender, DataEventArgs e)
        {
            lock (connectionLock)
            {                
                if (ReverseConnections.TryGetValue((RtdClient)sender, out var socket))
                {
                    ToClientTime(socket, e.Data);
                    logger.Debug($"↑[DATA] Target: {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}, Count: {e.Count}, Data: [{string.Join(", ", Enumerable.Range(0, e.Data.GetLength(1)).Select(j => $"{e.Data[0, j]}:{e.Data[1, j]}"))}]");
                    RTDdata data = new RTDdata(e.Count, e.Data);
                    byte[] datab = Helper.ObjectToByteArray(data);
                    socket.Send(datab);
                } else
                {
                    logger.Warn($"[WARN] RTDClient received data event, but client down: {socket.ConnectionInfo.ClientIpAddress}:{socket.ConnectionInfo.ClientPort}");
                }
            }
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
            Btn_server_Click(null, null); // Server stoppen
            Application.Exit();
        }

        private static string LogStatement(string logText)
        {
            return "[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "] " + logText + Environment.NewLine;
        }

        public void ChangeConnectionStatus(string target, int status)
        {
            PostUI(() =>
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
            });
        }

        public void UpdateConnections(int count)
        {
            PostUI(() => lbl_conn.Text = "Link (" + count.ToString() + " connections)");
        }


        public void UpdateRTDConnections(int count)
        {
            PostUI(() => lbl_rtd.Text = "RTDclient (" + count.ToString() + " connections) <-->");
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

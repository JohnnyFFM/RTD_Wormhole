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
       // private readonly List<IWebSocketConnection> Connections = new List<IWebSocketConnection>();     
        private readonly Dictionary<IWebSocketConnection, RtdClient> Connections = new Dictionary<IWebSocketConnection, RtdClient>();     
    
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
            ChangeWebSocketStatus("server_link_status", 2);
            UpdateConnections(Connections.Count);
            AppendLog("Client connected: " + socket.ConnectionInfo.ClientIpAddress.ToString() +":"+  socket.ConnectionInfo.ClientPort.ToString());
            AppendLog("Starting RTD for client: " + socket.ConnectionInfo.ClientIpAddress.ToString() + ":" + socket.ConnectionInfo.ClientPort.ToString());
            
            // TODO client needs link to socket
            RtdClient client = new RtdClient();
            client.EDisconnect += new EventHandler(Client_OnDisconnect);
            client.HeartBeatLost += new EventHandler(Client_OnHeartBeatLost);
            client.Data += new EventHandler<DataEventArgs>(Client_OnData);
            // TODO connect client, put in list
            Connections.Add(socket, client);
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
            //LinkClient.SendAsync(e.Data);
            AppendLog("RTDClient recieved data event. Forwarding to Websocket server.");
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
                tb_log.AppendText(LogStatement(logText));
            }
        }
    }
}

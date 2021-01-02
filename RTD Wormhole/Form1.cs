using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTD_Wormhole
{
    public partial class Form1 : Form
    {
        private readonly SynchronizationContext synchronizationContext;
        readonly RtdClient RTDclient;

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
        }

        void Client_OnHeartBeatLost(object sender, EventArgs e)
        {
            PostUI(() =>
            {
                server_rtd_status.Image = imageList.Images[1];
                srv_status.Text = "RTD Heartbeat lost. Reconnecting...";
                System.Threading.Tasks.Task.Delay(10 * 1000).ContinueWith((_) => ConnectRTD());
            });
        }

        private void ConnectRTD()
        {
            bool ret = RTDclient.Connect("xrtd.xrtd", -1);
            PostUI(() =>
            {
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
            });
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
    }
}

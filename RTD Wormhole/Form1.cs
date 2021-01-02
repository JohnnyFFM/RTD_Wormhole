using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTD_Wormhole
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            tb_srv_ip.Text = GetLocalIp();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
    }
}

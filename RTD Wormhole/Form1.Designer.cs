namespace RTD_Wormhole
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.btn_server = new System.Windows.Forms.ToolStripButton();
            this.btn_client = new System.Windows.Forms.ToolStripButton();
            this.btn_testConnection = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_srv_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_srv_progid = new System.Windows.Forms.TextBox();
            this.ud_srv_port = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_client_progid = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_client_ip = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ud_client_port = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.client_ws_status = new System.Windows.Forms.PictureBox();
            this.label11 = new System.Windows.Forms.Label();
            this.server_link_status = new System.Windows.Forms.PictureBox();
            this.label10 = new System.Windows.Forms.Label();
            this.server_ws_status = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.server_rtd_status = new System.Windows.Forms.PictureBox();
            this.client_status = new System.Windows.Forms.Label();
            this.srv_status = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ud_srv_port)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ud_client_port)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.client_ws_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_link_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_ws_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_rtd_status)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(766, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(93, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 489);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(766, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_server,
            this.btn_client,
            this.btn_testConnection});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(766, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // btn_server
            // 
            this.btn_server.Image = ((System.Drawing.Image)(resources.GetObject("btn_server.Image")));
            this.btn_server.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_server.Name = "btn_server";
            this.btn_server.Size = new System.Drawing.Size(59, 22);
            this.btn_server.Text = "Server";
            this.btn_server.Click += new System.EventHandler(this.btn_server_Click);
            // 
            // btn_client
            // 
            this.btn_client.Image = ((System.Drawing.Image)(resources.GetObject("btn_client.Image")));
            this.btn_client.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_client.Name = "btn_client";
            this.btn_client.Size = new System.Drawing.Size(58, 22);
            this.btn_client.Text = "Client";
            this.btn_client.Click += new System.EventHandler(this.btn_client_Click);
            // 
            // btn_testConnection
            // 
            this.btn_testConnection.Image = ((System.Drawing.Image)(resources.GetObject("btn_testConnection.Image")));
            this.btn_testConnection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_testConnection.Name = "btn_testConnection";
            this.btn_testConnection.Size = new System.Drawing.Size(112, 22);
            this.btn_testConnection.Text = "Test Connection";
            this.btn_testConnection.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_srv_ip);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_srv_progid);
            this.groupBox1.Controls.Add(this.ud_srv_port);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 82);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting Server";
            // 
            // tb_srv_ip
            // 
            this.tb_srv_ip.Location = new System.Drawing.Point(78, 53);
            this.tb_srv_ip.Name = "tb_srv_ip";
            this.tb_srv_ip.ReadOnly = true;
            this.tb_srv_ip.Size = new System.Drawing.Size(100, 20);
            this.tb_srv_ip.TabIndex = 3;
            this.tb_srv_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address";
            // 
            // tb_srv_progid
            // 
            this.tb_srv_progid.Location = new System.Drawing.Point(78, 24);
            this.tb_srv_progid.Name = "tb_srv_progid";
            this.tb_srv_progid.Size = new System.Drawing.Size(100, 20);
            this.tb_srv_progid.TabIndex = 1;
            this.tb_srv_progid.Text = "xrtd.xrtd";
            this.tb_srv_progid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ud_srv_port
            // 
            this.ud_srv_port.Location = new System.Drawing.Point(216, 54);
            this.ud_srv_port.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.ud_srv_port.Name = "ud_srv_port";
            this.ud_srv_port.Size = new System.Drawing.Size(65, 20);
            this.ud_srv_port.TabIndex = 5;
            this.ud_srv_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ud_srv_port.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RTD ProgID";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_client_progid);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tb_client_ip);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.ud_client_port);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(386, 52);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(368, 82);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Setting Client";
            // 
            // tb_client_progid
            // 
            this.tb_client_progid.Location = new System.Drawing.Point(78, 53);
            this.tb_client_progid.Name = "tb_client_progid";
            this.tb_client_progid.Size = new System.Drawing.Size(100, 20);
            this.tb_client_progid.TabIndex = 5;
            this.tb_client_progid.Text = "yrtd.xrtd";
            this.tb_client_progid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "RTD ProgID";
            // 
            // tb_client_ip
            // 
            this.tb_client_ip.Location = new System.Drawing.Point(78, 24);
            this.tb_client_ip.Name = "tb_client_ip";
            this.tb_client_ip.Size = new System.Drawing.Size(100, 20);
            this.tb_client_ip.TabIndex = 1;
            this.tb_client_ip.Text = "127.0.0.1";
            this.tb_client_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Address";
            // 
            // ud_client_port
            // 
            this.ud_client_port.Location = new System.Drawing.Point(216, 24);
            this.ud_client_port.Maximum = new decimal(new int[] {
            65000,
            0,
            0,
            0});
            this.ud_client_port.Name = "ud_client_port";
            this.ud_client_port.Size = new System.Drawing.Size(65, 20);
            this.ud_client_port.TabIndex = 3;
            this.ud_client_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ud_client_port.Value = new decimal(new int[] {
            7777,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(184, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Port";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.pictureBox5);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.pictureBox4);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.client_ws_status);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.server_link_status);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.server_ws_status);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.server_rtd_status);
            this.groupBox3.Controls.Add(this.client_status);
            this.groupBox3.Controls.Add(this.srv_status);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(12, 140);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(742, 200);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(337, 147);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 16);
            this.label14.TabIndex = 15;
            this.label14.Text = "Link";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(315, 147);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(16, 16);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox5.TabIndex = 14;
            this.pictureBox5.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(203, 147);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(106, 16);
            this.label13.TabIndex = 13;
            this.label13.Text = "RTDserver -->";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(181, 147);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(16, 16);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox4.TabIndex = 12;
            this.pictureBox4.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(32, 147);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(143, 16);
            this.label12.TabIndex = 11;
            this.label12.Text = "Wormhole Client -->";
            // 
            // client_ws_status
            // 
            this.client_ws_status.Image = ((System.Drawing.Image)(resources.GetObject("client_ws_status.Image")));
            this.client_ws_status.Location = new System.Drawing.Point(10, 147);
            this.client_ws_status.Name = "client_ws_status";
            this.client_ws_status.Size = new System.Drawing.Size(16, 16);
            this.client_ws_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.client_ws_status.TabIndex = 10;
            this.client_ws_status.TabStop = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(343, 65);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "Link";
            // 
            // server_link_status
            // 
            this.server_link_status.Image = ((System.Drawing.Image)(resources.GetObject("server_link_status.Image")));
            this.server_link_status.Location = new System.Drawing.Point(321, 65);
            this.server_link_status.Name = "server_link_status";
            this.server_link_status.Size = new System.Drawing.Size(16, 16);
            this.server_link_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.server_link_status.TabIndex = 8;
            this.server_link_status.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(165, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 16);
            this.label10.TabIndex = 7;
            this.label10.Text = "Wormhole Server -->";
            // 
            // server_ws_status
            // 
            this.server_ws_status.Image = ((System.Drawing.Image)(resources.GetObject("server_ws_status.Image")));
            this.server_ws_status.Location = new System.Drawing.Point(143, 65);
            this.server_ws_status.Name = "server_ws_status";
            this.server_ws_status.Size = new System.Drawing.Size(16, 16);
            this.server_ws_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.server_ws_status.TabIndex = 6;
            this.server_ws_status.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(38, 65);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(99, 16);
            this.label9.TabIndex = 5;
            this.label9.Text = "RTDclient -->";
            // 
            // server_rtd_status
            // 
            this.server_rtd_status.Image = ((System.Drawing.Image)(resources.GetObject("server_rtd_status.Image")));
            this.server_rtd_status.Location = new System.Drawing.Point(10, 65);
            this.server_rtd_status.Name = "server_rtd_status";
            this.server_rtd_status.Size = new System.Drawing.Size(16, 16);
            this.server_rtd_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.server_rtd_status.TabIndex = 4;
            this.server_rtd_status.TabStop = false;
            // 
            // client_status
            // 
            this.client_status.AutoSize = true;
            this.client_status.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.client_status.Location = new System.Drawing.Point(84, 105);
            this.client_status.Name = "client_status";
            this.client_status.Size = new System.Drawing.Size(117, 19);
            this.client_status.TabIndex = 3;
            this.client_status.Text = "Disconnected";
            // 
            // srv_status
            // 
            this.srv_status.AutoSize = true;
            this.srv_status.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.srv_status.Location = new System.Drawing.Point(84, 29);
            this.srv_status.Name = "srv_status";
            this.srv_status.Size = new System.Drawing.Size(117, 19);
            this.srv_status.TabIndex = 2;
            this.srv_status.Text = "Disconnected";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 19);
            this.label8.TabIndex = 1;
            this.label8.Text = "Client:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 19);
            this.label7.TabIndex = 0;
            this.label7.Text = "Server:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Location = new System.Drawing.Point(12, 346);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(742, 132);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Logs";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 19);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(727, 107);
            this.textBox1.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "offline.ico");
            this.imageList.Images.SetKeyName(1, "connecting.ico");
            this.imageList.Images.SetKeyName(2, "online.ico");
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 511);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(782, 550);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(782, 550);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RTD Wormhole";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ud_srv_port)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ud_client_port)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.client_ws_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_link_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_ws_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_rtd_status)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btn_server;
        private System.Windows.Forms.ToolStripButton btn_client;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_srv_progid;
        private System.Windows.Forms.TextBox tb_client_progid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_client_ip;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown ud_client_port;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_srv_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ud_srv_port;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label client_status;
        private System.Windows.Forms.Label srv_status;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox client_ws_status;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.PictureBox server_link_status;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PictureBox server_ws_status;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox server_rtd_status;
        private System.Windows.Forms.ToolStripButton btn_testConnection;
    }
}


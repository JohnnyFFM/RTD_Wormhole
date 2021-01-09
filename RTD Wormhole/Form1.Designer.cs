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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_srv_ip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_srv_progid = new System.Windows.Forms.TextBox();
            this.ud_srv_port = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tb_log = new System.Windows.Forms.TextBox();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_conn = new System.Windows.Forms.Label();
            this.server_rtd_status = new System.Windows.Forms.PictureBox();
            this.server_link_status = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.server_ws_status = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ud_srv_port)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.server_rtd_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_link_status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_ws_status)).BeginInit();
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
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
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
            this.btn_server});
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
            this.btn_server.Click += new System.EventHandler(this.Btn_server_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_srv_ip);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_srv_progid);
            this.groupBox1.Controls.Add(this.ud_srv_port);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 82);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting Server";
            // 
            // tb_srv_ip
            // 
            this.tb_srv_ip.Location = new System.Drawing.Point(78, 24);
            this.tb_srv_ip.Name = "tb_srv_ip";
            this.tb_srv_ip.Size = new System.Drawing.Size(100, 20);
            this.tb_srv_ip.TabIndex = 3;
            this.tb_srv_ip.Text = "0.0.0.0";
            this.tb_srv_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address";
            // 
            // tb_srv_progid
            // 
            this.tb_srv_progid.Location = new System.Drawing.Point(78, 53);
            this.tb_srv_progid.Name = "tb_srv_progid";
            this.tb_srv_progid.Size = new System.Drawing.Size(100, 20);
            this.tb_srv_progid.TabIndex = 1;
            this.tb_srv_progid.Text = "xrtd.xrtd";
            this.tb_srv_progid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ud_srv_port
            // 
            this.ud_srv_port.Location = new System.Drawing.Point(216, 25);
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(184, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RTD ProgID";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tb_log);
            this.groupBox4.Location = new System.Drawing.Point(12, 260);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(742, 218);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Logs";
            // 
            // tb_log
            // 
            this.tb_log.Location = new System.Drawing.Point(9, 19);
            this.tb_log.Multiline = true;
            this.tb_log.Name = "tb_log";
            this.tb_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_log.Size = new System.Drawing.Size(727, 193);
            this.tb_log.TabIndex = 0;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "offline.ico");
            this.imageList.Images.SetKeyName(1, "connecting.ico");
            this.imageList.Images.SetKeyName(2, "online.ico");
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.lbl_conn);
            this.groupBox3.Controls.Add(this.server_rtd_status);
            this.groupBox3.Controls.Add(this.server_link_status);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.server_ws_status);
            this.groupBox3.Location = new System.Drawing.Point(12, 140);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(742, 114);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Status";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(342, 49);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(158, 16);
            this.label10.TabIndex = 13;
            this.label10.Text = "Wormhole Server <-->";
            // 
            // lbl_conn
            // 
            this.lbl_conn.AutoSize = true;
            this.lbl_conn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_conn.Location = new System.Drawing.Point(528, 49);
            this.lbl_conn.Name = "lbl_conn";
            this.lbl_conn.Size = new System.Drawing.Size(145, 16);
            this.lbl_conn.TabIndex = 15;
            this.lbl_conn.Text = "Link (0 connections)";
            // 
            // server_rtd_status
            // 
            this.server_rtd_status.Image = ((System.Drawing.Image)(resources.GetObject("server_rtd_status.Image")));
            this.server_rtd_status.Location = new System.Drawing.Point(70, 49);
            this.server_rtd_status.Name = "server_rtd_status";
            this.server_rtd_status.Size = new System.Drawing.Size(16, 16);
            this.server_rtd_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.server_rtd_status.TabIndex = 10;
            this.server_rtd_status.TabStop = false;
            // 
            // server_link_status
            // 
            this.server_link_status.Image = ((System.Drawing.Image)(resources.GetObject("server_link_status.Image")));
            this.server_link_status.Location = new System.Drawing.Point(506, 49);
            this.server_link_status.Name = "server_link_status";
            this.server_link_status.Size = new System.Drawing.Size(16, 16);
            this.server_link_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.server_link_status.TabIndex = 14;
            this.server_link_status.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(98, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(216, 16);
            this.label9.TabIndex = 11;
            this.label9.Text = "RTDclient (0 connections) <-->";
            // 
            // server_ws_status
            // 
            this.server_ws_status.Image = ((System.Drawing.Image)(resources.GetObject("server_ws_status.Image")));
            this.server_ws_status.Location = new System.Drawing.Point(320, 49);
            this.server_ws_status.Name = "server_ws_status";
            this.server_ws_status.Size = new System.Drawing.Size(16, 16);
            this.server_ws_status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.server_ws_status.TabIndex = 12;
            this.server_ws_status.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 511);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
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
            this.Text = "RTD Wormhole Server";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ud_srv_port)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.server_rtd_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_link_status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.server_ws_status)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton btn_server;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_srv_progid;
        private System.Windows.Forms.TextBox tb_srv_ip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ud_srv_port;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tb_log;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_conn;
        private System.Windows.Forms.PictureBox server_rtd_status;
        private System.Windows.Forms.PictureBox server_link_status;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox server_ws_status;
    }
}


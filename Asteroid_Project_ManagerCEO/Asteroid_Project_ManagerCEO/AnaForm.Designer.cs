namespace Asteroid_Project_ManagerCEO
{
    partial class AnaForm
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaForm));
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.panelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.çalışanOnayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.duyurular = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.sirket_aciklamasi = new System.Windows.Forms.TextBox();
            this.sirket_resmi = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sirket_ismi = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sirket_resmi)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.White;
            this.menuStrip.Font = new System.Drawing.Font("Bahnschrift SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.panelToolStripMenuItem,
            this.çalışanOnayToolStripMenuItem,
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1254, 33);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // panelToolStripMenuItem
            // 
            this.panelToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.panelToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.panelToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("panelToolStripMenuItem.Image")));
            this.panelToolStripMenuItem.Name = "panelToolStripMenuItem";
            this.panelToolStripMenuItem.Size = new System.Drawing.Size(92, 29);
            this.panelToolStripMenuItem.Text = "Panel";
            this.panelToolStripMenuItem.Click += new System.EventHandler(this.panelToolStripMenuItem_Click);
            // 
            // çalışanOnayToolStripMenuItem
            // 
            this.çalışanOnayToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.çalışanOnayToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.çalışanOnayToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("çalışanOnayToolStripMenuItem.Image")));
            this.çalışanOnayToolStripMenuItem.Name = "çalışanOnayToolStripMenuItem";
            this.çalışanOnayToolStripMenuItem.Size = new System.Drawing.Size(135, 29);
            this.çalışanOnayToolStripMenuItem.Text = "Çalışanlar";
            this.çalışanOnayToolStripMenuItem.Click += new System.EventHandler(this.çalışanOnayToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.toolStripMenuItem5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem5.Image")));
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(170, 29);
            this.toolStripMenuItem5.Text = "Departmanlar";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.departmanlarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.toolStripMenuItem4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem4.Image")));
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(96, 29);
            this.toolStripMenuItem4.Text = "Şirket";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.şirketToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.toolStripMenuItem3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem3.Image")));
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(115, 29);
            this.toolStripMenuItem3.Text = "Projeler";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.projelerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.toolStripMenuItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem2.Image")));
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(148, 29);
            this.toolStripMenuItem2.Text = "Pozisyonlar";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.pozisyonlarToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(195, 29);
            this.toolStripMenuItem1.Text = "Aktivite Dökümü";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.aktiviteDökümüToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 33);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(292, 716);
            this.panel1.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.duyurular);
            this.groupBox2.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.groupBox2.Location = new System.Drawing.Point(12, 424);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(263, 212);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Duyurular";
            // 
            // duyurular
            // 
            this.duyurular.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duyurular.HideSelection = false;
            this.duyurular.Location = new System.Drawing.Point(3, 22);
            this.duyurular.Name = "duyurular";
            this.duyurular.Size = new System.Drawing.Size(257, 187);
            this.duyurular.TabIndex = 5;
            this.duyurular.UseCompatibleStateImageBehavior = false;
            this.duyurular.View = System.Windows.Forms.View.SmallIcon;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.sirket_aciklamasi);
            this.groupBox1.Controls.Add(this.sirket_resmi);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.sirket_ismi);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 415);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(17, 106);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 21);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // sirket_aciklamasi
            // 
            this.sirket_aciklamasi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sirket_aciklamasi.Enabled = false;
            this.sirket_aciklamasi.Font = new System.Drawing.Font("Bahnschrift Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sirket_aciklamasi.HideSelection = false;
            this.sirket_aciklamasi.Location = new System.Drawing.Point(9, 130);
            this.sirket_aciklamasi.Multiline = true;
            this.sirket_aciklamasi.Name = "sirket_aciklamasi";
            this.sirket_aciklamasi.ReadOnly = true;
            this.sirket_aciklamasi.ShortcutsEnabled = false;
            this.sirket_aciklamasi.Size = new System.Drawing.Size(263, 270);
            this.sirket_aciklamasi.TabIndex = 3;
            // 
            // sirket_resmi
            // 
            this.sirket_resmi.BackColor = System.Drawing.Color.White;
            this.sirket_resmi.Image = ((System.Drawing.Image)(resources.GetObject("sirket_resmi.Image")));
            this.sirket_resmi.Location = new System.Drawing.Point(17, 20);
            this.sirket_resmi.Name = "sirket_resmi";
            this.sirket_resmi.Size = new System.Drawing.Size(80, 80);
            this.sirket_resmi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sirket_resmi.TabIndex = 1;
            this.sirket_resmi.TabStop = false;
            this.sirket_resmi.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.label3.Location = new System.Drawing.Point(55, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 14);
            this.label3.TabIndex = 0;
            this.label3.Text = "Şirket Açıklaması";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.label1.Location = new System.Drawing.Point(106, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "Şirket İsmi";
            // 
            // sirket_ismi
            // 
            this.sirket_ismi.AutoSize = true;
            this.sirket_ismi.Font = new System.Drawing.Font("Bahnschrift", 13F, System.Drawing.FontStyle.Bold);
            this.sirket_ismi.Location = new System.Drawing.Point(103, 42);
            this.sirket_ismi.Name = "sirket_ismi";
            this.sirket_ismi.Size = new System.Drawing.Size(141, 22);
            this.sirket_ismi.TabIndex = 2;
            this.sirket_ismi.Text = "Asteroid Games";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AllowMerge = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Black;
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.Font = new System.Drawing.Font("Bahnschrift Condensed", 10F);
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(3);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.toolStrip1.Location = new System.Drawing.Point(0, 693);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(292, 23);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.AutoToolTip = true;
            this.toolStripLabel1.BackColor = System.Drawing.SystemColors.InfoText;
            this.toolStripLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripLabel1.ForeColor = System.Drawing.Color.White;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.ReadOnly = true;
            this.toolStripLabel1.ShortcutsEnabled = false;
            this.toolStripLabel1.Size = new System.Drawing.Size(288, 23);
            this.toolStripLabel1.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AnaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 749);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "AnaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asteroid Project Manager - Yönetim Sistemi";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AnaForm_FormClosed);
            this.Load += new System.EventHandler(this.AnaForm_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sirket_resmi)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem çalışanOnayToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ToolStrip toolStrip1;
        public System.Windows.Forms.PictureBox sirket_resmi;
        public System.Windows.Forms.Label sirket_ismi;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox sirket_aciklamasi;
        private System.Windows.Forms.ToolStripMenuItem panelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ToolStripTextBox toolStripLabel1;
        private System.Windows.Forms.ListView duyurular;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}




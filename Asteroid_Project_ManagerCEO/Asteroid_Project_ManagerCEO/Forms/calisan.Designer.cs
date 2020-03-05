namespace Asteroid_Project_ManagerCEO.Forms
{
    partial class calisan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(calisan));
            this.calisan_datagrid = new System.Windows.Forms.DataGridView();
            this.onay_button = new System.Windows.Forms.Button();
            this.calisan_ekle = new System.Windows.Forms.Button();
            this.calisan_bilgileri_guncelle = new System.Windows.Forms.Button();
            this.calisan_sil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.calisan_id = new System.Windows.Forms.Label();
            this.calisan_resmi = new System.Windows.Forms.PictureBox();
            this.calisan_isim = new System.Windows.Forms.Label();
            this.calisan_email = new System.Windows.Forms.Label();
            this.onay_durum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.onay_resim = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pozisyon_listview = new System.Windows.Forms.ListBox();
            this.calisan_cinsiyet = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.calisan_datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calisan_resmi)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.onay_resim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // calisan_datagrid
            // 
            this.calisan_datagrid.AllowUserToAddRows = false;
            this.calisan_datagrid.AllowUserToDeleteRows = false;
            this.calisan_datagrid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.calisan_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.calisan_datagrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.calisan_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.calisan_datagrid.Location = new System.Drawing.Point(24, 67);
            this.calisan_datagrid.MultiSelect = false;
            this.calisan_datagrid.Name = "calisan_datagrid";
            this.calisan_datagrid.ReadOnly = true;
            this.calisan_datagrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.calisan_datagrid.RowHeadersVisible = false;
            this.calisan_datagrid.RowHeadersWidth = 200;
            this.calisan_datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.calisan_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.calisan_datagrid.Size = new System.Drawing.Size(476, 532);
            this.calisan_datagrid.TabIndex = 0;
            this.calisan_datagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.row_selected);
            this.calisan_datagrid.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // onay_button
            // 
            this.onay_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.onay_button.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.onay_button.ForeColor = System.Drawing.Color.ForestGreen;
            this.onay_button.Location = new System.Drawing.Point(517, 371);
            this.onay_button.Name = "onay_button";
            this.onay_button.Size = new System.Drawing.Size(152, 102);
            this.onay_button.TabIndex = 0;
            this.onay_button.Text = "Onayla";
            this.onay_button.UseVisualStyleBackColor = true;
            this.onay_button.Click += new System.EventHandler(this.button1_Click);
            // 
            // calisan_ekle
            // 
            this.calisan_ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calisan_ekle.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calisan_ekle.ForeColor = System.Drawing.Color.DarkCyan;
            this.calisan_ekle.Location = new System.Drawing.Point(517, 479);
            this.calisan_ekle.Name = "calisan_ekle";
            this.calisan_ekle.Size = new System.Drawing.Size(152, 102);
            this.calisan_ekle.TabIndex = 2;
            this.calisan_ekle.Text = "Çalışan Ekle";
            this.calisan_ekle.UseVisualStyleBackColor = true;
            this.calisan_ekle.Click += new System.EventHandler(this.button2_Click);
            // 
            // calisan_bilgileri_guncelle
            // 
            this.calisan_bilgileri_guncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calisan_bilgileri_guncelle.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calisan_bilgileri_guncelle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.calisan_bilgileri_guncelle.Location = new System.Drawing.Point(675, 371);
            this.calisan_bilgileri_guncelle.Name = "calisan_bilgileri_guncelle";
            this.calisan_bilgileri_guncelle.Size = new System.Drawing.Size(231, 102);
            this.calisan_bilgileri_guncelle.TabIndex = 1;
            this.calisan_bilgileri_guncelle.Text = "Çalışan Bilgilerini Güncelle";
            this.calisan_bilgileri_guncelle.UseVisualStyleBackColor = true;
            this.calisan_bilgileri_guncelle.Click += new System.EventHandler(this.button3_Click);
            // 
            // calisan_sil
            // 
            this.calisan_sil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calisan_sil.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calisan_sil.ForeColor = System.Drawing.Color.Red;
            this.calisan_sil.Location = new System.Drawing.Point(675, 479);
            this.calisan_sil.Name = "calisan_sil";
            this.calisan_sil.Size = new System.Drawing.Size(231, 102);
            this.calisan_sil.TabIndex = 3;
            this.calisan_sil.Text = "Çalışan Bilgilerini Sil";
            this.calisan_sil.UseVisualStyleBackColor = true;
            this.calisan_sil.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.label1.Location = new System.Drawing.Point(86, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Çalışanlar";
            // 
            // calisan_id
            // 
            this.calisan_id.AutoSize = true;
            this.calisan_id.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calisan_id.Location = new System.Drawing.Point(44, 16);
            this.calisan_id.Name = "calisan_id";
            this.calisan_id.Size = new System.Drawing.Size(141, 18);
            this.calisan_id.TabIndex = 3;
            this.calisan_id.Text = "Çalışan Bilgileri (ID)";
            // 
            // calisan_resmi
            // 
            this.calisan_resmi.Location = new System.Drawing.Point(25, 40);
            this.calisan_resmi.Name = "calisan_resmi";
            this.calisan_resmi.Size = new System.Drawing.Size(123, 123);
            this.calisan_resmi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.calisan_resmi.TabIndex = 4;
            this.calisan_resmi.TabStop = false;
            // 
            // calisan_isim
            // 
            this.calisan_isim.AutoSize = true;
            this.calisan_isim.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calisan_isim.Location = new System.Drawing.Point(154, 40);
            this.calisan_isim.Name = "calisan_isim";
            this.calisan_isim.Size = new System.Drawing.Size(115, 23);
            this.calisan_isim.TabIndex = 3;
            this.calisan_isim.Text = "Çalışan İsim";
            // 
            // calisan_email
            // 
            this.calisan_email.AutoSize = true;
            this.calisan_email.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.calisan_email.Location = new System.Drawing.Point(184, 82);
            this.calisan_email.Name = "calisan_email";
            this.calisan_email.Size = new System.Drawing.Size(85, 14);
            this.calisan_email.TabIndex = 3;
            this.calisan_email.Text = "Çalışan Eposta";
            // 
            // onay_durum
            // 
            this.onay_durum.AutoSize = true;
            this.onay_durum.Font = new System.Drawing.Font("Bahnschrift", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.onay_durum.ForeColor = System.Drawing.Color.Red;
            this.onay_durum.Location = new System.Drawing.Point(48, 254);
            this.onay_durum.Name = "onay_durum";
            this.onay_durum.Size = new System.Drawing.Size(179, 33);
            this.onay_durum.TabIndex = 3;
            this.onay_durum.Text = "Onay Durumu";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox5);
            this.groupBox1.Controls.Add(this.onay_resim);
            this.groupBox1.Controls.Add(this.pictureBox6);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.pozisyon_listview);
            this.groupBox1.Controls.Add(this.calisan_id);
            this.groupBox1.Controls.Add(this.calisan_resmi);
            this.groupBox1.Controls.Add(this.calisan_isim);
            this.groupBox1.Controls.Add(this.onay_durum);
            this.groupBox1.Controls.Add(this.calisan_cinsiyet);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.calisan_email);
            this.groupBox1.Location = new System.Drawing.Point(517, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(389, 290);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(158, 106);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(20, 18);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 6;
            this.pictureBox5.TabStop = false;
            // 
            // onay_resim
            // 
            this.onay_resim.Image = ((System.Drawing.Image)(resources.GetObject("onay_resim.Image")));
            this.onay_resim.Location = new System.Drawing.Point(6, 248);
            this.onay_resim.Name = "onay_resim";
            this.onay_resim.Size = new System.Drawing.Size(43, 36);
            this.onay_resim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.onay_resim.TabIndex = 6;
            this.onay_resim.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(158, 78);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(20, 18);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 6;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(25, 171);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(20, 18);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 6;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(25, 16);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(20, 18);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // pozisyon_listview
            // 
            this.pozisyon_listview.Cursor = System.Windows.Forms.Cursors.Default;
            this.pozisyon_listview.Enabled = false;
            this.pozisyon_listview.FormattingEnabled = true;
            this.pozisyon_listview.Location = new System.Drawing.Point(158, 130);
            this.pozisyon_listview.Name = "pozisyon_listview";
            this.pozisyon_listview.Size = new System.Drawing.Size(225, 121);
            this.pozisyon_listview.TabIndex = 5;
            // 
            // calisan_cinsiyet
            // 
            this.calisan_cinsiyet.AutoSize = true;
            this.calisan_cinsiyet.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.calisan_cinsiyet.Location = new System.Drawing.Point(51, 175);
            this.calisan_cinsiyet.Name = "calisan_cinsiyet";
            this.calisan_cinsiyet.Size = new System.Drawing.Size(90, 14);
            this.calisan_cinsiyet.TabIndex = 3;
            this.calisan_cinsiyet.Text = "Çalışan Cinsiyet";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift Light", 9F);
            this.label4.Location = new System.Drawing.Point(184, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 14);
            this.label4.TabIndex = 3;
            this.label4.Text = "Çalışan Pozisyonları";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(24, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(56, 52);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // calisan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 611);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.calisan_sil);
            this.Controls.Add(this.calisan_bilgileri_guncelle);
            this.Controls.Add(this.calisan_ekle);
            this.Controls.Add(this.onay_button);
            this.Controls.Add(this.calisan_datagrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "calisan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "calisan";
            this.Load += new System.EventHandler(this.calisan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calisan_datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calisan_resmi)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.onay_resim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView calisan_datagrid;
        private System.Windows.Forms.Button onay_button;
        private System.Windows.Forms.Button calisan_ekle;
        private System.Windows.Forms.Button calisan_bilgileri_guncelle;
        private System.Windows.Forms.Button calisan_sil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label calisan_id;
        private System.Windows.Forms.PictureBox calisan_resmi;
        private System.Windows.Forms.Label calisan_isim;
        private System.Windows.Forms.Label calisan_email;
        private System.Windows.Forms.Label onay_durum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label calisan_cinsiyet;
        private System.Windows.Forms.ListBox pozisyon_listview;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox onay_resim;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
namespace Asteroid_Project_ManagerCEO
{
    partial class kayit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(kayit));
            this.kullanici_adi = new System.Windows.Forms.TextBox();
            this.sifre = new System.Windows.Forms.TextBox();
            this.sifre_tekrar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.kayit_ol = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.sirket_resmi_ekle = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.sirket_ismi = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.sirket_detay = new System.Windows.Forms.TextBox();
            this.hata_label = new System.Windows.Forms.Label();
            this.sirket_resmi = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.sirket_resmi_kaldir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.sirket_resmi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.SuspendLayout();
            // 
            // kullanici_adi
            // 
            this.kullanici_adi.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kullanici_adi.Location = new System.Drawing.Point(627, 101);
            this.kullanici_adi.Name = "kullanici_adi";
            this.kullanici_adi.Size = new System.Drawing.Size(216, 30);
            this.kullanici_adi.TabIndex = 1;
            // 
            // sifre
            // 
            this.sifre.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sifre.Location = new System.Drawing.Point(627, 157);
            this.sifre.Name = "sifre";
            this.sifre.Size = new System.Drawing.Size(216, 30);
            this.sifre.TabIndex = 2;
            this.sifre.TextChanged += new System.EventHandler(this.sifre_enter);
            // 
            // sifre_tekrar
            // 
            this.sifre_tekrar.Enabled = false;
            this.sifre_tekrar.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sifre_tekrar.Location = new System.Drawing.Point(627, 211);
            this.sifre_tekrar.Name = "sifre_tekrar";
            this.sifre_tekrar.Size = new System.Drawing.Size(216, 30);
            this.sifre_tekrar.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(658, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 18);
            this.label1.TabIndex = 3;
            this.label1.Text = "Kullanıcı Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(658, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Şifre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(624, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "Şifre Tekrar";
            // 
            // kayit_ol
            // 
            this.kayit_ol.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.kayit_ol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.kayit_ol.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.kayit_ol.ForeColor = System.Drawing.SystemColors.Highlight;
            this.kayit_ol.Location = new System.Drawing.Point(679, 521);
            this.kayit_ol.Name = "kayit_ol";
            this.kayit_ol.Size = new System.Drawing.Size(259, 117);
            this.kayit_ol.TabIndex = 7;
            this.kayit_ol.Text = "KAYIT OL";
            this.kayit_ol.UseVisualStyleBackColor = true;
            this.kayit_ol.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // sirket_resmi_ekle
            // 
            this.sirket_resmi_ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sirket_resmi_ekle.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sirket_resmi_ekle.Location = new System.Drawing.Point(235, 219);
            this.sirket_resmi_ekle.Name = "sirket_resmi_ekle";
            this.sirket_resmi_ekle.Size = new System.Drawing.Size(73, 35);
            this.sirket_resmi_ekle.TabIndex = 4;
            this.sirket_resmi_ekle.Text = "Gözat";
            this.sirket_resmi_ekle.UseVisualStyleBackColor = true;
            this.sirket_resmi_ekle.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(35, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 18);
            this.label4.TabIndex = 9;
            this.label4.Text = "Şirket Logosu";
            // 
            // sirket_ismi
            // 
            this.sirket_ismi.Font = new System.Drawing.Font("Bahnschrift", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sirket_ismi.Location = new System.Drawing.Point(326, 98);
            this.sirket_ismi.Name = "sirket_ismi";
            this.sirket_ismi.Size = new System.Drawing.Size(267, 33);
            this.sirket_ismi.TabIndex = 0;
            this.sirket_ismi.Text = "COMPANY";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(323, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 18);
            this.label5.TabIndex = 11;
            this.label5.Text = "Şirket İsmi";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(69, 272);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(126, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "Şirket Açıklaması";
            // 
            // sirket_detay
            // 
            this.sirket_detay.Font = new System.Drawing.Font("Bahnschrift SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sirket_detay.Location = new System.Drawing.Point(38, 296);
            this.sirket_detay.Multiline = true;
            this.sirket_detay.Name = "sirket_detay";
            this.sirket_detay.Size = new System.Drawing.Size(890, 186);
            this.sirket_detay.TabIndex = 6;
            this.sirket_detay.Text = "Company Details";
            // 
            // hata_label
            // 
            this.hata_label.AutoSize = true;
            this.hata_label.Font = new System.Drawing.Font("Bahnschrift Light Condensed", 14F);
            this.hata_label.ForeColor = System.Drawing.Color.Red;
            this.hata_label.Location = new System.Drawing.Point(322, 39);
            this.hata_label.Name = "hata_label";
            this.hata_label.Size = new System.Drawing.Size(48, 23);
            this.hata_label.TabIndex = 14;
            this.hata_label.Text = "label7";
            // 
            // sirket_resmi
            // 
            this.sirket_resmi.Image = ((System.Drawing.Image)(resources.GetObject("sirket_resmi.Image")));
            this.sirket_resmi.Location = new System.Drawing.Point(38, 39);
            this.sirket_resmi.Name = "sirket_resmi";
            this.sirket_resmi.Size = new System.Drawing.Size(270, 215);
            this.sirket_resmi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.sirket_resmi.TabIndex = 7;
            this.sirket_resmi.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Asteroid_Project_ManagerCEO.Properties.Resources._235_2357327_location_arithmetic_horizontal_horizontal_solid_black_line_hd;
            this.pictureBox2.Location = new System.Drawing.Point(38, 501);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(900, 3);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 15;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(627, 132);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(25, 22);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 16;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(627, 76);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(25, 22);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 16;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(38, 268);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(25, 22);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 16;
            this.pictureBox5.TabStop = false;
            // 
            // sirket_resmi_kaldir
            // 
            this.sirket_resmi_kaldir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sirket_resmi_kaldir.Font = new System.Drawing.Font("Bahnschrift SemiBold SemiConden", 9F, System.Drawing.FontStyle.Bold);
            this.sirket_resmi_kaldir.ForeColor = System.Drawing.Color.Red;
            this.sirket_resmi_kaldir.Location = new System.Drawing.Point(172, 230);
            this.sirket_resmi_kaldir.Name = "sirket_resmi_kaldir";
            this.sirket_resmi_kaldir.Size = new System.Drawing.Size(57, 24);
            this.sirket_resmi_kaldir.TabIndex = 5;
            this.sirket_resmi_kaldir.Text = "Kaldır";
            this.sirket_resmi_kaldir.UseVisualStyleBackColor = true;
            this.sirket_resmi_kaldir.Click += new System.EventHandler(this.button3_Click);
            // 
            // kayit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(950, 650);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.hata_label);
            this.Controls.Add(this.sirket_detay);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sirket_ismi);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.sirket_resmi_kaldir);
            this.Controls.Add(this.sirket_resmi_ekle);
            this.Controls.Add(this.sirket_resmi);
            this.Controls.Add(this.kayit_ol);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sifre_tekrar);
            this.Controls.Add(this.sifre);
            this.Controls.Add(this.kullanici_adi);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "kayit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KAYIT";
            this.Load += new System.EventHandler(this.kayit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sirket_resmi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox kullanici_adi;
        private System.Windows.Forms.TextBox sifre;
        private System.Windows.Forms.TextBox sifre_tekrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button kayit_ol;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox sirket_resmi;
        private System.Windows.Forms.Button sirket_resmi_ekle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sirket_ismi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox sirket_detay;
        private System.Windows.Forms.Label hata_label;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Button sirket_resmi_kaldir;
    }
}
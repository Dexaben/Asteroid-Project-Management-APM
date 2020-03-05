namespace Asteroid_Project_ManagerAPP.Forms
{
    partial class gorev_bilgileri_guncelle
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(gorev_bilgileri_guncelle));
            this.calisan_datagridview = new System.Windows.Forms.DataGridView();
            this.calisan_Ekle = new System.Windows.Forms.Button();
            this.bitis_tarih = new System.Windows.Forms.DateTimePicker();
            this.baslangic_tarih = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.acil_resim = new System.Windows.Forms.PictureBox();
            this.acil_checkBox = new System.Windows.Forms.CheckBox();
            this.guncelle = new System.Windows.Forms.Button();
            this.resim_geri = new System.Windows.Forms.Button();
            this.resim_ileri = new System.Windows.Forms.Button();
            this.gorev_Resim_Sil = new System.Windows.Forms.Button();
            this.gorev_Resim_Ekle = new System.Windows.Forms.Button();
            this.gorev_Resim = new System.Windows.Forms.PictureBox();
            this.gorev_detay = new System.Windows.Forms.TextBox();
            this.gorev = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.calisan_datagridview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.acil_resim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gorev_Resim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            this.SuspendLayout();
            // 
            // calisan_datagridview
            // 
            this.calisan_datagridview.AllowUserToAddRows = false;
            this.calisan_datagridview.AllowUserToDeleteRows = false;
            this.calisan_datagridview.AllowUserToResizeColumns = false;
            this.calisan_datagridview.AllowUserToResizeRows = false;
            this.calisan_datagridview.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.calisan_datagridview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.calisan_datagridview.Location = new System.Drawing.Point(651, 41);
            this.calisan_datagridview.MultiSelect = false;
            this.calisan_datagridview.Name = "calisan_datagridview";
            this.calisan_datagridview.ReadOnly = true;
            this.calisan_datagridview.RowHeadersVisible = false;
            this.calisan_datagridview.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.calisan_datagridview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.calisan_datagridview.Size = new System.Drawing.Size(506, 291);
            this.calisan_datagridview.TabIndex = 37;
            this.calisan_datagridview.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.calisan_datagridview.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.calisan_datagridview.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseLeave);
            this.calisan_datagridview.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // calisan_Ekle
            // 
            this.calisan_Ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calisan_Ekle.Font = new System.Drawing.Font("Bahnschrift SemiLight SemiConde", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.calisan_Ekle.Location = new System.Drawing.Point(907, 338);
            this.calisan_Ekle.Name = "calisan_Ekle";
            this.calisan_Ekle.Size = new System.Drawing.Size(248, 31);
            this.calisan_Ekle.TabIndex = 9;
            this.calisan_Ekle.Text = "Çalışan Ekle";
            this.calisan_Ekle.UseVisualStyleBackColor = true;
            this.calisan_Ekle.Click += new System.EventHandler(this.button3_Click);
            // 
            // bitis_tarih
            // 
            this.bitis_tarih.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.bitis_tarih.Font = new System.Drawing.Font("Bahnschrift Light", 15.75F);
            this.bitis_tarih.Location = new System.Drawing.Point(34, 471);
            this.bitis_tarih.Name = "bitis_tarih";
            this.bitis_tarih.Size = new System.Drawing.Size(306, 33);
            this.bitis_tarih.TabIndex = 8;
            this.bitis_tarih.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // baslangic_tarih
            // 
            this.baslangic_tarih.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.baslangic_tarih.Font = new System.Drawing.Font("Bahnschrift Light", 15.75F);
            this.baslangic_tarih.Location = new System.Drawing.Point(31, 393);
            this.baslangic_tarih.MinDate = new System.DateTime(2019, 12, 19, 10, 17, 12, 0);
            this.baslangic_tarih.Name = "baslangic_tarih";
            this.baslangic_tarih.Size = new System.Drawing.Size(306, 33);
            this.baslangic_tarih.TabIndex = 7;
            this.baslangic_tarih.Value = new System.DateTime(2019, 12, 19, 10, 17, 12, 0);
            this.baslangic_tarih.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F);
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(72, 450);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 18);
            this.label5.TabIndex = 30;
            this.label5.Text = "Görev Bitiş Tarihi";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(686, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 18);
            this.label7.TabIndex = 33;
            this.label7.Text = "Görevi Üstlenen Çalışanlar";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Bahnschrift", 11.25F);
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(838, 375);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 18);
            this.label8.TabIndex = 32;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F);
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(69, 372);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(165, 18);
            this.label6.TabIndex = 31;
            this.label6.Text = "Göreve Başlangıç Tarihi";
            // 
            // acil_resim
            // 
            this.acil_resim.Image = ((System.Drawing.Image)(resources.GetObject("acil_resim.Image")));
            this.acil_resim.Location = new System.Drawing.Point(123, 300);
            this.acil_resim.Name = "acil_resim";
            this.acil_resim.Size = new System.Drawing.Size(45, 40);
            this.acil_resim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.acil_resim.TabIndex = 20;
            this.acil_resim.TabStop = false;
            // 
            // acil_checkBox
            // 
            this.acil_checkBox.AutoSize = true;
            this.acil_checkBox.Font = new System.Drawing.Font("Bahnschrift Light", 22F);
            this.acil_checkBox.Location = new System.Drawing.Point(44, 300);
            this.acil_checkBox.Name = "acil_checkBox";
            this.acil_checkBox.Size = new System.Drawing.Size(85, 40);
            this.acil_checkBox.TabIndex = 6;
            this.acil_checkBox.Text = "Acil";
            this.acil_checkBox.UseVisualStyleBackColor = true;
            // 
            // guncelle
            // 
            this.guncelle.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.guncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.guncelle.Font = new System.Drawing.Font("Bahnschrift", 26F, System.Drawing.FontStyle.Bold);
            this.guncelle.ForeColor = System.Drawing.SystemColors.Highlight;
            this.guncelle.Location = new System.Drawing.Point(862, 397);
            this.guncelle.Name = "guncelle";
            this.guncelle.Size = new System.Drawing.Size(295, 109);
            this.guncelle.TabIndex = 10;
            this.guncelle.Text = "Güncelle";
            this.guncelle.UseVisualStyleBackColor = true;
            this.guncelle.Click += new System.EventHandler(this.button2_Click);
            // 
            // resim_geri
            // 
            this.resim_geri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resim_geri.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.resim_geri.Location = new System.Drawing.Point(44, 238);
            this.resim_geri.Name = "resim_geri";
            this.resim_geri.Size = new System.Drawing.Size(50, 28);
            this.resim_geri.TabIndex = 5;
            this.resim_geri.Text = "<";
            this.resim_geri.UseVisualStyleBackColor = true;
            this.resim_geri.Click += new System.EventHandler(this.button5_Click);
            // 
            // resim_ileri
            // 
            this.resim_ileri.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resim_ileri.Font = new System.Drawing.Font("Bahnschrift", 12F, System.Drawing.FontStyle.Bold);
            this.resim_ileri.Location = new System.Drawing.Point(104, 238);
            this.resim_ileri.Name = "resim_ileri";
            this.resim_ileri.Size = new System.Drawing.Size(50, 28);
            this.resim_ileri.TabIndex = 4;
            this.resim_ileri.Text = ">";
            this.resim_ileri.UseVisualStyleBackColor = true;
            this.resim_ileri.Click += new System.EventHandler(this.button4_Click);
            // 
            // gorev_Resim_Sil
            // 
            this.gorev_Resim_Sil.BackColor = System.Drawing.Color.White;
            this.gorev_Resim_Sil.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.gorev_Resim_Sil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gorev_Resim_Sil.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gorev_Resim_Sil.ForeColor = System.Drawing.Color.Red;
            this.gorev_Resim_Sil.Location = new System.Drawing.Point(181, 207);
            this.gorev_Resim_Sil.Name = "gorev_Resim_Sil";
            this.gorev_Resim_Sil.Size = new System.Drawing.Size(38, 25);
            this.gorev_Resim_Sil.TabIndex = 2;
            this.gorev_Resim_Sil.Text = "Sil";
            this.gorev_Resim_Sil.UseVisualStyleBackColor = false;
            this.gorev_Resim_Sil.Click += new System.EventHandler(this.button6_Click);
            // 
            // gorev_Resim_Ekle
            // 
            this.gorev_Resim_Ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gorev_Resim_Ekle.Font = new System.Drawing.Font("Bahnschrift SemiBold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gorev_Resim_Ekle.Location = new System.Drawing.Point(160, 238);
            this.gorev_Resim_Ekle.Name = "gorev_Resim_Ekle";
            this.gorev_Resim_Ekle.Size = new System.Drawing.Size(50, 28);
            this.gorev_Resim_Ekle.TabIndex = 3;
            this.gorev_Resim_Ekle.Text = "Ekle";
            this.gorev_Resim_Ekle.UseVisualStyleBackColor = true;
            this.gorev_Resim_Ekle.Click += new System.EventHandler(this.button1_Click);
            // 
            // gorev_Resim
            // 
            this.gorev_Resim.Image = ((System.Drawing.Image)(resources.GetObject("gorev_Resim.Image")));
            this.gorev_Resim.Location = new System.Drawing.Point(15, 50);
            this.gorev_Resim.Name = "gorev_Resim";
            this.gorev_Resim.Size = new System.Drawing.Size(204, 182);
            this.gorev_Resim.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gorev_Resim.TabIndex = 24;
            this.gorev_Resim.TabStop = false;
            this.gorev_Resim.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // gorev_detay
            // 
            this.gorev_detay.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gorev_detay.Location = new System.Drawing.Point(225, 111);
            this.gorev_detay.Multiline = true;
            this.gorev_detay.Name = "gorev_detay";
            this.gorev_detay.Size = new System.Drawing.Size(406, 221);
            this.gorev_detay.TabIndex = 1;
            // 
            // gorev
            // 
            this.gorev.Font = new System.Drawing.Font("Bahnschrift", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gorev.Location = new System.Drawing.Point(225, 51);
            this.gorev.Name = "gorev";
            this.gorev.Size = new System.Drawing.Size(406, 36);
            this.gorev.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(253, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Görev Detayları";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(12, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 18);
            this.label4.TabIndex = 18;
            this.label4.Text = "Görev Resmi";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(253, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 18);
            this.label2.TabIndex = 21;
            this.label2.Text = "Görev";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 22F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(351, -51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 36);
            this.label1.TabIndex = 16;
            this.label1.Text = "Görev Ekle";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(225, 90);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(22, 18);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 38;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(871, 338);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 31);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 38;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.Location = new System.Drawing.Point(225, 29);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(22, 18);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 38;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(31, 372);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(32, 18);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 38;
            this.pictureBox6.TabStop = false;
            // 
            // pictureBox7
            // 
            this.pictureBox7.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox7.Image")));
            this.pictureBox7.Location = new System.Drawing.Point(34, 450);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(32, 18);
            this.pictureBox7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox7.TabIndex = 38;
            this.pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox8.Image")));
            this.pictureBox8.Location = new System.Drawing.Point(650, 7);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(30, 31);
            this.pictureBox8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox8.TabIndex = 38;
            this.pictureBox8.TabStop = false;
            // 
            // gorev_bilgileri_guncelle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 538);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox7);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.calisan_datagridview);
            this.Controls.Add(this.calisan_Ekle);
            this.Controls.Add(this.bitis_tarih);
            this.Controls.Add(this.baslangic_tarih);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.acil_resim);
            this.Controls.Add(this.acil_checkBox);
            this.Controls.Add(this.guncelle);
            this.Controls.Add(this.resim_geri);
            this.Controls.Add(this.resim_ileri);
            this.Controls.Add(this.gorev_Resim_Sil);
            this.Controls.Add(this.gorev_Resim_Ekle);
            this.Controls.Add(this.gorev_Resim);
            this.Controls.Add(this.gorev_detay);
            this.Controls.Add(this.gorev);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "gorev_bilgileri_guncelle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Görev Bilgilerini Güncelle";
            this.Load += new System.EventHandler(this.gorev_bilgileri_guncelle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calisan_datagridview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.acil_resim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gorev_Resim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView calisan_datagridview;
        private System.Windows.Forms.Button calisan_Ekle;
        private System.Windows.Forms.DateTimePicker bitis_tarih;
        private System.Windows.Forms.DateTimePicker baslangic_tarih;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox acil_resim;
        private System.Windows.Forms.CheckBox acil_checkBox;
        private System.Windows.Forms.Button guncelle;
        private System.Windows.Forms.Button resim_geri;
        private System.Windows.Forms.Button resim_ileri;
        private System.Windows.Forms.Button gorev_Resim_Sil;
        private System.Windows.Forms.Button gorev_Resim_Ekle;
        private System.Windows.Forms.PictureBox gorev_Resim;
        private System.Windows.Forms.TextBox gorev_detay;
        private System.Windows.Forms.TextBox gorev;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.PictureBox pictureBox7;
        private System.Windows.Forms.PictureBox pictureBox8;
    }
}
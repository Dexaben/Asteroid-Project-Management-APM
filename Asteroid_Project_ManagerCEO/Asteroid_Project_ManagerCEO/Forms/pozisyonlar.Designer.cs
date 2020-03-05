namespace Asteroid_Project_ManagerCEO.Forms
{
    partial class pozisyonlar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(pozisyonlar));
            this.label1 = new System.Windows.Forms.Label();
            this.pozisyon_listview = new System.Windows.Forms.ListBox();
            this.pozisyon_ekle = new System.Windows.Forms.Button();
            this.pozisyon_duzenle = new System.Windows.Forms.Button();
            this.pozisyon_sil = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.label1.Location = new System.Drawing.Point(58, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 35);
            this.label1.TabIndex = 3;
            this.label1.Text = "Pozisyonlar";
            // 
            // pozisyon_listview
            // 
            this.pozisyon_listview.FormattingEnabled = true;
            this.pozisyon_listview.ItemHeight = 18;
            this.pozisyon_listview.Location = new System.Drawing.Point(193, 52);
            this.pozisyon_listview.Name = "pozisyon_listview";
            this.pozisyon_listview.Size = new System.Drawing.Size(242, 436);
            this.pozisyon_listview.TabIndex = 4;
            // 
            // pozisyon_ekle
            // 
            this.pozisyon_ekle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.pozisyon_ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pozisyon_ekle.Font = new System.Drawing.Font("Bahnschrift", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.pozisyon_ekle.ForeColor = System.Drawing.Color.White;
            this.pozisyon_ekle.Location = new System.Drawing.Point(441, 52);
            this.pozisyon_ekle.Name = "pozisyon_ekle";
            this.pozisyon_ekle.Size = new System.Drawing.Size(259, 77);
            this.pozisyon_ekle.TabIndex = 0;
            this.pozisyon_ekle.Text = "Pozisyon Ekle";
            this.pozisyon_ekle.UseVisualStyleBackColor = false;
            this.pozisyon_ekle.Click += new System.EventHandler(this.button1_Click);
            // 
            // pozisyon_duzenle
            // 
            this.pozisyon_duzenle.BackColor = System.Drawing.Color.DarkOrange;
            this.pozisyon_duzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pozisyon_duzenle.Font = new System.Drawing.Font("Bahnschrift", 19F, System.Drawing.FontStyle.Bold);
            this.pozisyon_duzenle.ForeColor = System.Drawing.Color.White;
            this.pozisyon_duzenle.Location = new System.Drawing.Point(441, 135);
            this.pozisyon_duzenle.Name = "pozisyon_duzenle";
            this.pozisyon_duzenle.Size = new System.Drawing.Size(259, 75);
            this.pozisyon_duzenle.TabIndex = 1;
            this.pozisyon_duzenle.Text = "Pozisyonu Düzenle";
            this.pozisyon_duzenle.UseVisualStyleBackColor = false;
            this.pozisyon_duzenle.Click += new System.EventHandler(this.button2_Click);
            // 
            // pozisyon_sil
            // 
            this.pozisyon_sil.BackColor = System.Drawing.Color.Firebrick;
            this.pozisyon_sil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pozisyon_sil.Font = new System.Drawing.Font("Bahnschrift", 9F, System.Drawing.FontStyle.Bold);
            this.pozisyon_sil.ForeColor = System.Drawing.Color.White;
            this.pozisyon_sil.Location = new System.Drawing.Point(441, 216);
            this.pozisyon_sil.Name = "pozisyon_sil";
            this.pozisyon_sil.Size = new System.Drawing.Size(259, 32);
            this.pozisyon_sil.TabIndex = 2;
            this.pozisyon_sil.Text = "Pozisyonu Sil (Önerilmez)";
            this.pozisyon_sil.UseVisualStyleBackColor = false;
            this.pozisyon_sil.Click += new System.EventHandler(this.button3_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // pozisyonlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pozisyon_sil);
            this.Controls.Add(this.pozisyon_duzenle);
            this.Controls.Add(this.pozisyon_ekle);
            this.Controls.Add(this.pozisyon_listview);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "pozisyonlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pozisyonlar";
            this.Load += new System.EventHandler(this.pozisyonlar_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox pozisyon_listview;
        private System.Windows.Forms.Button pozisyon_ekle;
        private System.Windows.Forms.Button pozisyon_duzenle;
        private System.Windows.Forms.Button pozisyon_sil;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
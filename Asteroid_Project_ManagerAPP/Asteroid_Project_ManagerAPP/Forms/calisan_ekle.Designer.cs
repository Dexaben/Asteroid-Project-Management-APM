namespace Asteroid_Project_ManagerAPP
{
    partial class calisan_ekle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(calisan_ekle));
            this.label7 = new System.Windows.Forms.Label();
            this.proje_calisanlari_datagrid = new System.Windows.Forms.DataGridView();
            this.calisan_aktar = new System.Windows.Forms.Button();
            this.aktarilacak_calisanlar_datagrid = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.proje_calisanlari_datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktarilacak_calisanlar_datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F);
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(443, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 18);
            this.label7.TabIndex = 14;
            this.label7.Text = "Aktarılacak Çalışanlar";
            // 
            // proje_calisanlari_datagrid
            // 
            this.proje_calisanlari_datagrid.AllowUserToAddRows = false;
            this.proje_calisanlari_datagrid.AllowUserToDeleteRows = false;
            this.proje_calisanlari_datagrid.AllowUserToResizeColumns = false;
            this.proje_calisanlari_datagrid.AllowUserToResizeRows = false;
            this.proje_calisanlari_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.proje_calisanlari_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.proje_calisanlari_datagrid.Location = new System.Drawing.Point(12, 32);
            this.proje_calisanlari_datagrid.MultiSelect = false;
            this.proje_calisanlari_datagrid.Name = "proje_calisanlari_datagrid";
            this.proje_calisanlari_datagrid.ReadOnly = true;
            this.proje_calisanlari_datagrid.RowHeadersVisible = false;
            this.proje_calisanlari_datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.proje_calisanlari_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.proje_calisanlari_datagrid.Size = new System.Drawing.Size(401, 301);
            this.proje_calisanlari_datagrid.TabIndex = 0;
            this.proje_calisanlari_datagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.proje_calisanlari_datagrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.proje_calisanlari_datagrid.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseLeave);
            this.proje_calisanlari_datagrid.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // calisan_aktar
            // 
            this.calisan_aktar.FlatAppearance.BorderColor = System.Drawing.Color.DimGray;
            this.calisan_aktar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.calisan_aktar.Font = new System.Drawing.Font("Bahnschrift", 19F, System.Drawing.FontStyle.Bold);
            this.calisan_aktar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.calisan_aktar.Location = new System.Drawing.Point(540, 356);
            this.calisan_aktar.Name = "calisan_aktar";
            this.calisan_aktar.Size = new System.Drawing.Size(237, 82);
            this.calisan_aktar.TabIndex = 2;
            this.calisan_aktar.Text = "Çalışanları Aktar";
            this.calisan_aktar.UseVisualStyleBackColor = true;
            this.calisan_aktar.Click += new System.EventHandler(this.button1_Click);
            // 
            // aktarilacak_calisanlar_datagrid
            // 
            this.aktarilacak_calisanlar_datagrid.AllowUserToAddRows = false;
            this.aktarilacak_calisanlar_datagrid.AllowUserToDeleteRows = false;
            this.aktarilacak_calisanlar_datagrid.AllowUserToResizeColumns = false;
            this.aktarilacak_calisanlar_datagrid.AllowUserToResizeRows = false;
            this.aktarilacak_calisanlar_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.aktarilacak_calisanlar_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.aktarilacak_calisanlar_datagrid.Location = new System.Drawing.Point(419, 32);
            this.aktarilacak_calisanlar_datagrid.MultiSelect = false;
            this.aktarilacak_calisanlar_datagrid.Name = "aktarilacak_calisanlar_datagrid";
            this.aktarilacak_calisanlar_datagrid.ReadOnly = true;
            this.aktarilacak_calisanlar_datagrid.RowHeadersVisible = false;
            this.aktarilacak_calisanlar_datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.aktarilacak_calisanlar_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.aktarilacak_calisanlar_datagrid.Size = new System.Drawing.Size(358, 301);
            this.aktarilacak_calisanlar_datagrid.TabIndex = 1;
            this.aktarilacak_calisanlar_datagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.aktarilacak_calisanlar_datagrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.aktarilacak_calisanlar_datagrid.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseLeave);
            this.aktarilacak_calisanlar_datagrid.DoubleClick += new System.EventHandler(this.dataGridView2_DoubleClick_1);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(418, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(19, 18);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(12, 339);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(90, 108);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 39;
            this.pictureBox4.TabStop = false;
            // 
            // calisan_ekle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.aktarilacak_calisanlar_datagrid);
            this.Controls.Add(this.calisan_aktar);
            this.Controls.Add(this.proje_calisanlari_datagrid);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "calisan_ekle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Çalışanları Ekle";
            this.Load += new System.EventHandler(this.calisan_ekle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.proje_calisanlari_datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aktarilacak_calisanlar_datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView proje_calisanlari_datagrid;
        private System.Windows.Forms.Button calisan_aktar;
        private System.Windows.Forms.DataGridView aktarilacak_calisanlar_datagrid;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pictureBox4;
    }
}
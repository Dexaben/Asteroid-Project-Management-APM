namespace Asteroid_Project_ManagerCEO.Forms
{
    partial class departman
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(departman));
            this.departman_ekle = new System.Windows.Forms.Button();
            this.departman_duzenle = new System.Windows.Forms.Button();
            this.departmanlar_datagrid = new System.Windows.Forms.DataGridView();
            this.departman_sil = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.departman_detay = new System.Windows.Forms.TextBox();
            this.departman_ismi = new System.Windows.Forms.TextBox();
            this.departman_id = new System.Windows.Forms.Label();
            this.departman_resmi = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.departmanlar_datagrid)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.departman_resmi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // departman_ekle
            // 
            this.departman_ekle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.departman_ekle.Font = new System.Drawing.Font("Bahnschrift", 15F, System.Drawing.FontStyle.Bold);
            this.departman_ekle.ForeColor = System.Drawing.Color.Teal;
            this.departman_ekle.Location = new System.Drawing.Point(464, 517);
            this.departman_ekle.Name = "departman_ekle";
            this.departman_ekle.Size = new System.Drawing.Size(288, 82);
            this.departman_ekle.TabIndex = 1;
            this.departman_ekle.Text = "Departman Ekle";
            this.departman_ekle.UseVisualStyleBackColor = true;
            this.departman_ekle.Click += new System.EventHandler(this.button1_Click);
            // 
            // departman_duzenle
            // 
            this.departman_duzenle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.departman_duzenle.Font = new System.Drawing.Font("Bahnschrift", 15F, System.Drawing.FontStyle.Bold);
            this.departman_duzenle.ForeColor = System.Drawing.Color.DarkOrange;
            this.departman_duzenle.Location = new System.Drawing.Point(758, 443);
            this.departman_duzenle.Name = "departman_duzenle";
            this.departman_duzenle.Size = new System.Drawing.Size(164, 68);
            this.departman_duzenle.TabIndex = 0;
            this.departman_duzenle.Text = "Departman Düzenle";
            this.departman_duzenle.UseVisualStyleBackColor = true;
            this.departman_duzenle.Click += new System.EventHandler(this.button2_Click);
            // 
            // departmanlar_datagrid
            // 
            this.departmanlar_datagrid.AllowUserToAddRows = false;
            this.departmanlar_datagrid.AllowUserToDeleteRows = false;
            this.departmanlar_datagrid.AllowUserToResizeColumns = false;
            this.departmanlar_datagrid.AllowUserToResizeRows = false;
            this.departmanlar_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.departmanlar_datagrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.departmanlar_datagrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.departmanlar_datagrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.departmanlar_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.departmanlar_datagrid.Location = new System.Drawing.Point(12, 49);
            this.departmanlar_datagrid.MultiSelect = false;
            this.departmanlar_datagrid.Name = "departmanlar_datagrid";
            this.departmanlar_datagrid.ReadOnly = true;
            this.departmanlar_datagrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.departmanlar_datagrid.RowHeadersVisible = false;
            this.departmanlar_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.departmanlar_datagrid.ShowCellErrors = false;
            this.departmanlar_datagrid.ShowEditingIcon = false;
            this.departmanlar_datagrid.Size = new System.Drawing.Size(446, 550);
            this.departmanlar_datagrid.TabIndex = 1;
            this.departmanlar_datagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.row_selected);
            // 
            // departman_sil
            // 
            this.departman_sil.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.departman_sil.Font = new System.Drawing.Font("Bahnschrift", 15F, System.Drawing.FontStyle.Bold);
            this.departman_sil.ForeColor = System.Drawing.Color.Firebrick;
            this.departman_sil.Location = new System.Drawing.Point(758, 517);
            this.departman_sil.Name = "departman_sil";
            this.departman_sil.Size = new System.Drawing.Size(164, 82);
            this.departman_sil.TabIndex = 2;
            this.departman_sil.Text = "Departman Sil";
            this.departman_sil.UseVisualStyleBackColor = true;
            this.departman_sil.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.label1.Location = new System.Drawing.Point(69, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 35);
            this.label1.TabIndex = 2;
            this.label1.Text = "Departmanlar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox3);
            this.groupBox1.Controls.Add(this.departman_detay);
            this.groupBox1.Controls.Add(this.departman_ismi);
            this.groupBox1.Controls.Add(this.departman_id);
            this.groupBox1.Controls.Add(this.departman_resmi);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(477, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(445, 388);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // departman_detay
            // 
            this.departman_detay.Enabled = false;
            this.departman_detay.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.departman_detay.Location = new System.Drawing.Point(19, 185);
            this.departman_detay.Multiline = true;
            this.departman_detay.Name = "departman_detay";
            this.departman_detay.Size = new System.Drawing.Size(420, 192);
            this.departman_detay.TabIndex = 5;
            // 
            // departman_ismi
            // 
            this.departman_ismi.Enabled = false;
            this.departman_ismi.Font = new System.Drawing.Font("Bahnschrift", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.departman_ismi.Location = new System.Drawing.Point(151, 89);
            this.departman_ismi.Name = "departman_ismi";
            this.departman_ismi.Size = new System.Drawing.Size(273, 30);
            this.departman_ismi.TabIndex = 5;
            // 
            // departman_id
            // 
            this.departman_id.AutoSize = true;
            this.departman_id.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.departman_id.Location = new System.Drawing.Point(15, 16);
            this.departman_id.Name = "departman_id";
            this.departman_id.Size = new System.Drawing.Size(138, 18);
            this.departman_id.TabIndex = 3;
            this.departman_id.Text = "Departman Bilgileri";
            // 
            // departman_resmi
            // 
            this.departman_resmi.Location = new System.Drawing.Point(18, 38);
            this.departman_resmi.Name = "departman_resmi";
            this.departman_resmi.Size = new System.Drawing.Size(123, 123);
            this.departman_resmi.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.departman_resmi.TabIndex = 4;
            this.departman_resmi.TabStop = false;
            this.departman_resmi.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(48, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 18);
            this.label4.TabIndex = 3;
            this.label4.Text = "Departman Detayları";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(147, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "Departman İsmi";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(51, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(19, 164);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(23, 18);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 7;
            this.pictureBox3.TabStop = false;
            // 
            // departman
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 611);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.departmanlar_datagrid);
            this.Controls.Add(this.departman_sil);
            this.Controls.Add(this.departman_duzenle);
            this.Controls.Add(this.departman_ekle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "departman";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "departman";
            this.Load += new System.EventHandler(this.departman_Load);
            ((System.ComponentModel.ISupportInitialize)(this.departmanlar_datagrid)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.departman_resmi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button departman_ekle;
        private System.Windows.Forms.Button departman_duzenle;
        private System.Windows.Forms.DataGridView departmanlar_datagrid;
        private System.Windows.Forms.Button departman_sil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox departman_detay;
        private System.Windows.Forms.TextBox departman_ismi;
        private System.Windows.Forms.Label departman_id;
        private System.Windows.Forms.PictureBox departman_resmi;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
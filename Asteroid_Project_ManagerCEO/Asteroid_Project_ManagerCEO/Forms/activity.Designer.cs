namespace Asteroid_Project_ManagerCEO.Forms
{
    partial class activity
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(activity));
            this.label1 = new System.Windows.Forms.Label();
            this.activity_datagrid = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listeleme_secim = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.activity_datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.label1.Location = new System.Drawing.Point(58, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 33);
            this.label1.TabIndex = 12;
            this.label1.Text = "Aktiviteler";
            // 
            // activity_datagrid
            // 
            this.activity_datagrid.AllowUserToAddRows = false;
            this.activity_datagrid.AllowUserToDeleteRows = false;
            this.activity_datagrid.AllowUserToResizeColumns = false;
            this.activity_datagrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.activity_datagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.activity_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.activity_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.activity_datagrid.Location = new System.Drawing.Point(18, 46);
            this.activity_datagrid.MultiSelect = false;
            this.activity_datagrid.Name = "activity_datagrid";
            this.activity_datagrid.ReadOnly = true;
            this.activity_datagrid.RowHeadersVisible = false;
            this.activity_datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Bahnschrift SemiCondensed", 9.75F, System.Drawing.FontStyle.Bold);
            this.activity_datagrid.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.activity_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.activity_datagrid.Size = new System.Drawing.Size(920, 592);
            this.activity_datagrid.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(18, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(34, 33);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // listeleme_secim
            // 
            this.listeleme_secim.FormattingEnabled = true;
            this.listeleme_secim.Items.AddRange(new object[] {
            "Tüm Kayıtlar",
            "Çalışan Kayıtları",
            "Yönetici Kayıtları",
            "Şuanda Aktif Olan Çalışanların Kayıtları"});
            this.listeleme_secim.Location = new System.Drawing.Point(457, 14);
            this.listeleme_secim.Name = "listeleme_secim";
            this.listeleme_secim.Size = new System.Drawing.Size(281, 26);
            this.listeleme_secim.TabIndex = 0;
            this.listeleme_secim.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(327, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "Listeleme Seçimi";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Firebrick;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Bahnschrift SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.button1.Location = new System.Drawing.Point(813, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 28);
            this.button1.TabIndex = 17;
            this.button1.Text = "Log\'ları Temizle";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // activity
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 650);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listeleme_secim);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.activity_datagrid);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Bahnschrift Light", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "activity";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "activity";
            this.Load += new System.EventHandler(this.activity_Load);
            ((System.ComponentModel.ISupportInitialize)(this.activity_datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView activity_datagrid;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox listeleme_secim;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}
namespace Asteroid_Project_ManagerAPP
{
    partial class team
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(team));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.sirket_takimi = new System.Windows.Forms.Button();
            this.proje_takimi = new System.Windows.Forms.Button();
            this.calisan_liste_datagrid = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.calisan_liste_datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.label1.Name = "label1";
            // 
            // sirket_takimi
            // 
            this.sirket_takimi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.sirket_takimi.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.sirket_takimi.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.sirket_takimi, "sirket_takimi");
            this.sirket_takimi.ForeColor = System.Drawing.Color.White;
            this.sirket_takimi.Name = "sirket_takimi";
            this.sirket_takimi.UseVisualStyleBackColor = false;
            this.sirket_takimi.Click += new System.EventHandler(this.button2_Click);
            // 
            // proje_takimi
            // 
            this.proje_takimi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.proje_takimi.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
            this.proje_takimi.FlatAppearance.BorderSize = 3;
            resources.ApplyResources(this.proje_takimi, "proje_takimi");
            this.proje_takimi.ForeColor = System.Drawing.Color.White;
            this.proje_takimi.Name = "proje_takimi";
            this.proje_takimi.UseVisualStyleBackColor = false;
            this.proje_takimi.Click += new System.EventHandler(this.button1_Click);
            // 
            // calisan_liste_datagrid
            // 
            this.calisan_liste_datagrid.AllowUserToAddRows = false;
            this.calisan_liste_datagrid.AllowUserToDeleteRows = false;
            this.calisan_liste_datagrid.AllowUserToResizeColumns = false;
            this.calisan_liste_datagrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.calisan_liste_datagrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.calisan_liste_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.calisan_liste_datagrid.BackgroundColor = System.Drawing.Color.White;
            this.calisan_liste_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.calisan_liste_datagrid.GridColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.calisan_liste_datagrid, "calisan_liste_datagrid");
            this.calisan_liste_datagrid.MultiSelect = false;
            this.calisan_liste_datagrid.Name = "calisan_liste_datagrid";
            this.calisan_liste_datagrid.ReadOnly = true;
            this.calisan_liste_datagrid.RowHeadersVisible = false;
            this.calisan_liste_datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.calisan_liste_datagrid.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.calisan_liste_datagrid.RowTemplate.Height = 50;
            this.calisan_liste_datagrid.RowTemplate.ReadOnly = true;
            this.calisan_liste_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.calisan_liste_datagrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.calisan_liste_datagrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseEnter);
            this.calisan_liste_datagrid.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellMouseLeave);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // team
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.sirket_takimi);
            this.Controls.Add(this.proje_takimi);
            this.Controls.Add(this.calisan_liste_datagrid);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "team";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.team_Load);
            ((System.ComponentModel.ISupportInitialize)(this.calisan_liste_datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sirket_takimi;
        private System.Windows.Forms.Button proje_takimi;
        private System.Windows.Forms.DataGridView calisan_liste_datagrid;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
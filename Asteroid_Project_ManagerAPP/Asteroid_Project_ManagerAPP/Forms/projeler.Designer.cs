namespace Asteroid_Project_ManagerAPP
{
    partial class projeler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(projeler));
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.proje_ekle = new System.Windows.Forms.Button();
            this.proje_gir = new System.Windows.Forms.Button();
            this.proje_sil = new System.Windows.Forms.Button();
            this.projeler_datagrid = new System.Windows.Forms.DataGridView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.projeler_datagrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(154)))), ((int)(((byte)(211)))));
            this.label8.Name = "label8";
            // 
            // proje_ekle
            // 
            resources.ApplyResources(this.proje_ekle, "proje_ekle");
            this.proje_ekle.ForeColor = System.Drawing.Color.Green;
            this.proje_ekle.Name = "proje_ekle";
            this.proje_ekle.UseVisualStyleBackColor = true;
            this.proje_ekle.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // proje_gir
            // 
            resources.ApplyResources(this.proje_gir, "proje_gir");
            this.proje_gir.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.proje_gir.Name = "proje_gir";
            this.proje_gir.UseVisualStyleBackColor = true;
            this.proje_gir.Click += new System.EventHandler(this.button2_Click);
            // 
            // proje_sil
            // 
            resources.ApplyResources(this.proje_sil, "proje_sil");
            this.proje_sil.ForeColor = System.Drawing.Color.Red;
            this.proje_sil.Name = "proje_sil";
            this.proje_sil.UseVisualStyleBackColor = true;
            this.proje_sil.Click += new System.EventHandler(this.button3_Click);
            // 
            // projeler_datagrid
            // 
            this.projeler_datagrid.AllowUserToAddRows = false;
            this.projeler_datagrid.AllowUserToDeleteRows = false;
            this.projeler_datagrid.AllowUserToResizeColumns = false;
            this.projeler_datagrid.AllowUserToResizeRows = false;
            this.projeler_datagrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.projeler_datagrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.projeler_datagrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.projeler_datagrid.EnableHeadersVisualStyles = false;
            resources.ApplyResources(this.projeler_datagrid, "projeler_datagrid");
            this.projeler_datagrid.MultiSelect = false;
            this.projeler_datagrid.Name = "projeler_datagrid";
            this.projeler_datagrid.ReadOnly = true;
            this.projeler_datagrid.RowHeadersVisible = false;
            this.projeler_datagrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
            this.projeler_datagrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // projeler
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.proje_sil);
            this.Controls.Add(this.proje_gir);
            this.Controls.Add(this.proje_ekle);
            this.Controls.Add(this.projeler_datagrid);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "projeler";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.projeler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.projeler_datagrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button proje_ekle;
        private System.Windows.Forms.Button proje_gir;
        private System.Windows.Forms.Button proje_sil;
        private System.Windows.Forms.DataGridView projeler_datagrid;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
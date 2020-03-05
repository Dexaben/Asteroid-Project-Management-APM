using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    public partial class calisan_ekle : Form
    {
        SqlCommand komut = new SqlCommand();
        public DataTable worker_table { get; set; }
        /// <summary>
        /// getirilecek çalışanların sql commandı (indexler = 0:worker_id , 1:worker_name , 2:worker_image)
        /// </summary>
        /// <param name="sqlCommand"></param>
        public calisan_ekle(SqlCommand sqlCommand)
        {
            komut = sqlCommand;
            InitializeComponent();
            this.AcceptButton = calisan_aktar;
        }
        FUNCTIONS F = new FUNCTIONS();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        DataTable table = new DataTable();
        private void calisan_ekle_Load(object sender, EventArgs e)
        {
           
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 3000);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            if (table !=null)
            {
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString(), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]));
                }
            }
            //datagridview2 columns add
            DataGridViewImageColumn dataGridViewImageColumn = new DataGridViewImageColumn();
            dataGridViewImageColumn.HeaderText = "Çalışan Resmi";
            dataGridViewImageColumn.Name = "worker_image";
            aktarilacak_calisanlar_datagrid.Columns.Add("worker_id", "Çalışan ID");
       
            aktarilacak_calisanlar_datagrid.Columns.Add("worker_name", "Çalışan İsmi");
            aktarilacak_calisanlar_datagrid.Columns.Add(dataGridViewImageColumn);
            aktarilacak_calisanlar_datagrid.Columns[0].ValueType = typeof(int);
        
            aktarilacak_calisanlar_datagrid.Columns[2].ValueType = typeof(string);
            aktarilacak_calisanlar_datagrid.Columns[1].ValueType = typeof(Image);
            ((DataGridViewImageColumn)aktarilacak_calisanlar_datagrid.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            aktarilacak_calisanlar_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            proje_calisanlari_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            proje_calisanlari_datagrid.DataSource = dataTable;
            for (int i = 0; i < proje_calisanlari_datagrid.Columns.Count; i++)
                if (proje_calisanlari_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)proje_calisanlari_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            gorev_ekle gorev_Ekle = (gorev_ekle)Application.OpenForms["gorev_ekle"];
            DataTable table = new DataTable();
            table.Columns.Add("worker_id", typeof(int));
            table.Columns.Add("worker_image", typeof(Image));
            table.Columns.Add("worker_name", typeof(string));
            for (int i = 0; i < aktarilacak_calisanlar_datagrid.Rows.Count; i++)
            {
                table.Rows.Add(aktarilacak_calisanlar_datagrid.Rows[i].Cells[0].Value, aktarilacak_calisanlar_datagrid.Rows[i].Cells[2].Value, aktarilacak_calisanlar_datagrid.Rows[i].Cells[1].Value);
            }
            this.worker_table = table;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(aktarilacak_calisanlar_datagrid.Rows.Count !=0)
            {
                List<int> worker_ids = new List<int>();
                for(int i = 0;i<aktarilacak_calisanlar_datagrid.Rows.Count;i++)
                {
                    worker_ids.Add(Convert.ToInt32(aktarilacak_calisanlar_datagrid.Rows[i].Cells[0].Value));
                }
                if (worker_ids.IndexOf(Convert.ToInt32(proje_calisanlari_datagrid.SelectedRows[0].Cells[0].Value)) == -1)
                {
                    aktarilacak_calisanlar_datagrid.Rows.Add(proje_calisanlari_datagrid.SelectedRows[0].Cells[0].Value, proje_calisanlari_datagrid.SelectedRows[0].Cells[1].Value.ToString(), proje_calisanlari_datagrid.SelectedRows[0].Cells[2].Value);
                }
                else F.DURUM_LABEL("Bu çalışan zaten ekli.", Color.BlueViolet, 2000);
            }
            else aktarilacak_calisanlar_datagrid.Rows.Add(proje_calisanlari_datagrid.SelectedRows[0].Cells[0].Value, proje_calisanlari_datagrid.SelectedRows[0].Cells[1].Value.ToString(), proje_calisanlari_datagrid.SelectedRows[0].Cells[2].Value);
        }
        private void dataGridView2_DoubleClick_1(object sender, EventArgs e)
        {
            if(aktarilacak_calisanlar_datagrid.SelectedRows.Count == 1)
            {
                aktarilacak_calisanlar_datagrid.Rows.Remove(aktarilacak_calisanlar_datagrid.SelectedRows[0]);
            }
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count > 0 && e.RowIndex == dataGridView.SelectedRows[0].Index)
            {
                string[] positions = F.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                string P = dataGridView.SelectedRows[0].Cells[1].Value.ToString() + "'nin pozisyonları \n";
                for (int i = 0; i < positions.Length; i++)
                {
                    P += positions[i] + "\n";
                }
                Rectangle cellRect = dataGridView.GetCellDisplayRectangle(1,e.RowIndex,false);
                toolTip1.Show(P,
                              this,
                              dataGridView.Location.X + cellRect.X+ 8,
                              dataGridView.Location.Y  + cellRect.Y+ cellRect.Size.Height + 30);    // Duration: 5 seconds.
            }
          
        }
        private void dataGridView2_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
                toolTip1.Hide(this);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1_CellMouseEnter((DataGridView)sender, e);
        }
    }
}

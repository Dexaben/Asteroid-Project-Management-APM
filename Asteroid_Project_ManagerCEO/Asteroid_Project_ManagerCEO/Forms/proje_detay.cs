using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Asteroid_Project_ManagerCEO
{
    public partial class proje_detay : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        public int project_id { get;set; }
        public proje_detay()
        {
            InitializeComponent();
        }

        private void proje_detay_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT W.worker_id,W.worker_name,W.worker_image,(SELECT COUNT(T.task_id) FROM TASK_WORKERS T WHERE T.worker_id = W.worker_id)as uzerindeki_gorev_say,(SELECT COUNT(T.task_id) FROM TASKS T WHERE T.task_status ='Görev Tamamlandı' AND W.worker_id IN (SELECT TASK_WORKERS.worker_id FROM TASK_WORKERS WHERE T.task_id = TASK_WORKERS.task_id))AS tamamladigi_gorev  FROM WORKERS W WHERE W.worker_id IN (SELECT PW.worker_id FROM PROJECT_WORKERS PW WHERE PW.project_id=@PROJECT_ID)");
            komut.Parameters.AddWithValue("@PROJECT_ID", project_id);
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje çalışan bilgileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable_calisan = new DataTable();
            dataTable_calisan.Columns.Add("Çalışan ID", typeof(int));
            dataTable_calisan.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable_calisan.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable_calisan.Columns.Add("Çalışanın Üzerindeki Görev Sayısı", typeof(int));
            dataTable_calisan.Columns.Add("Çalışanın Tamamladığı Görev Sayısı", typeof(int));
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable_calisan.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]),table.Rows[i]["worker_name"].ToString(),Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]),Convert.ToInt32(table.Rows[i]["uzerindeki_gorev_say"]),Convert.ToInt32(table.Rows[i]["tamamladigi_gorev"]));
                }
            }
            proje_calisanlari_datagrid.DataSource = dataTable_calisan;
            proje_calisanlari_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < proje_calisanlari_datagrid.Columns.Count; i++)
                if (proje_calisanlari_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)proje_calisanlari_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
            komut = new SqlCommand("SELECT project_name,project_image,project_detail,project_start_date,project_finish_date,project_status,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE DEPARTMENTS.department_id = PROJECTS.department_id)AS department_name,(SELECT W.worker_name FROM WORKERS W WHERE W.worker_id=PROJECTS.project_manager)AS manager_name FROM PROJECTS WHERE PROJECTS.project_id=@PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", project_id);
            SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje bilgileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table !=null)
            {
                proje_ismi.Text = (table.Rows[0]["project_name"].ToString());
                proje_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["project_image"]);
                proje_detayi.Text = (table.Rows[0]["project_detail"].ToString());
                proje_baslangic_tarihi.Text = Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["project_start_date"].ToString());
                proje_bitis_tarihi.Text = Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["project_finish_date"].ToString());
                proje_departmani.Text = table.Rows[0]["department_name"].ToString(); 
                proje_yoneticisi.Text = table.Rows[0]["manager_name"].ToString();
                proje_durumu.Text = table.Rows[0]["project_status"].ToString();
            }
            komut = new SqlCommand("SELECT TASKS.task_id,TASKS.task_name,TASKS.task_status,TASKS.task_start_date,TASKS.task_finish_date,(SELECT COUNT(TASK_WORKERS.task_id) FROM TASK_WORKERS WHERE TASKS.task_id = TASK_WORKERS.task_id)as calisan_sayisi FROM TASKS WHERE project_id = @PROJECT_ID AND task_status <>'Görev Tamamlandı'");
            komut.Parameters.AddWithValue("@PROJECT_ID", project_id);
           SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Görev ID", typeof(int));
            dataTable.Columns.Add("Görev İsmi", typeof(string));
            dataTable.Columns.Add("Görev Durumu", typeof(string));
            dataTable.Columns.Add("Görev Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Görev Bitiş Tarihi", typeof(string));
            dataTable.Columns.Add("Görevdeki Çalışan Sayısı", typeof(int));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["task_name"].ToString(), table.Rows[i]["task_status"].ToString(), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_start_date"].ToString()), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString()), Convert.ToInt32(table.Rows[i]["calisan_sayisi"]));
            }
            proje_gorevleri_datagrid.DataSource = dataTable;
            proje_gorevleri_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(proje_resmi.Image);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Forms.gorev grv = new Forms.gorev();
            grv.gorev_id = Convert.ToInt32(proje_gorevleri_datagrid.SelectedRows[0].Cells[0].Value);
            grv.ShowDialog();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            Forms.calisan_detay calisan_Detay = new Forms.calisan_detay();
            calisan_Detay.worker_id = Convert.ToInt32(proje_calisanlari_datagrid.SelectedRows[0].Cells[0].Value);
            calisan_Detay.ShowDialog();
        }
    }
}

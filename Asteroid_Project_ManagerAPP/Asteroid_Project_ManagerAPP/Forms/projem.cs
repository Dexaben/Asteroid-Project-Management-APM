using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    public partial class projem : Form
    {
        public projem()
        {
            InitializeComponent();
        }
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        System.Data.DataTable table = new System.Data.DataTable();
        System.Data.DataTable dataTable = new System.Data.DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, $"{Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {Scripts.SQL.SqlQueries.PROJECT_NAME_BY_PROJECT_ID(a.project_id)} projesinden çıkış yaptı.Proje ID:{a.project_id}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
            a.project_id = 0;
            a.ANAFORM_BILGILER_GUNCELLE();
            Scripts.Form.FormManager.FORM_AC(new projeler(), true);
        }

        public void projem_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT P.project_name,P.project_image,P.project_detail,(SELECT worker_name FROM WORKERS WHERE worker_id=P.project_manager)AS project_manager,P.project_start_date,P.project_finish_date,P.project_status,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE DEPARTMENTS.department_id = P.department_id)AS department_name FROM PROJECTS P WHERE P.project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje bilgileri alınırken hata oluştu.", System.Drawing.Color.Red, 4000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table.Rows.Count != 0)
            {
                proje_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["project_image"]);
                proje_adi.Text = table.Rows[0]["project_name"].ToString();
                proje_yoneticisi.Text = table.Rows[0]["project_manager"].ToString();
                proje_departmani.Text = table.Rows[0]["department_name"].ToString();
                proje_detayi.Text = table.Rows[0]["project_detail"].ToString();
                proje_baslangic_tarihi.Text = Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["project_start_date"].ToString());
                proje_bitis_tarihi.Text = Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["project_finish_date"].ToString());
                proje_durumu.Text = table.Rows[0]["project_status"].ToString();
            }
            komut = new SqlCommand("SELECT T.task_id,T.task_name,T.task_urgency,T.task_status,T.task_finish_date,(SELECT COUNT(TASK_WORKERS.worker_id) FROM TASK_WORKERS WHERE T.task_id=TASK_WORKERS.task_id)AS gorevdeki_calisanlar FROM TASKS T WHERE T.project_id = @PROJECT_ID AND T.task_status<>'Görev Tamamlandı'");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
           sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Aktif görevler alınırken sorun oluştu.", System.Drawing.Color.Red, 4000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Görev ID", typeof(int));
            dataTable.Columns.Add("Görev İsmi", typeof(string));
            dataTable.Columns.Add("Görev Aciliyeti", typeof(string));
            dataTable.Columns.Add("Görev Durumu", typeof(string));
            dataTable.Columns.Add("Görev Bitiş Tarihi", typeof(string));
            dataTable.Columns.Add("Görevli Olan Çalışan Sayısı", typeof(int));
            if (table.Rows.Count != 0)
            {
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["task_name"].ToString(), table.Rows[i]["task_urgency"].ToString(), table.Rows[i]["task_status"].ToString(), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString()), Convert.ToInt32(table.Rows[i]["gorevdeki_calisanlar"]));
                }
            }
            aktif_gorevler_datagrid.DataSource = dataTable;
            for (int i = 0; i < aktif_gorevler_datagrid.Columns.Count; i++)
                if (aktif_gorevler_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)aktif_gorevler_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
            komut = new SqlCommand("SELECT T.task_id,T.task_name,T.task_status FROM TASKS T WHERE T.project_id = @PROJECT_ID AND T.task_status='Görev Tamamlandı'");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Aktif görevler alınırken sorun oluştu.", System.Drawing.Color.Red, 4000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Görev ID", typeof(int));
            dataTable.Columns.Add("Görev İsmi", typeof(string));
            dataTable.Columns.Add("Görev Durumu", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["task_name"].ToString(), table.Rows[i]["task_status"].ToString());
                }
            }
            tamamlanmis_gorevler.DataSource = dataTable;
            komut = new SqlCommand("SELECT W.worker_id,W.worker_image,W.worker_name,(SELECT COUNT(PROJECT_WORKERS.worker_id) FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.worker_id=P.worker_id)AS calisan_proje_say,(SELECT COUNT(TASK_WORKERS.worker_id) FROM TASK_WORKERS WHERE TASK_WORKERS.worker_id=P.worker_id)AS gorev_calisan_say FROM PROJECT_WORKERS P INNER JOIN WORKERS W ON P.worker_id=W.worker_id  WHERE P.project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Aktif görevler alınırken sorun oluştu.", System.Drawing.Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan Resmi", typeof(System.Drawing.Image));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable.Columns.Add("Çalışanın Bulunduğu Proje Sayısı", typeof(int));
            dataTable.Columns.Add("Çalışanın Bulunduğu Görev Sayısı", typeof(int));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), table.Rows[i]["worker_name"].ToString(),Convert.ToInt32(table.Rows[i]["calisan_proje_say"]), Convert.ToInt32(table.Rows[i]["gorev_calisan_say"]));
                }
            }
            proje_calisanlari_datagrid.DataSource = dataTable;
            for (int i = 0; i < proje_calisanlari_datagrid.Columns.Count; i++)
                if (proje_calisanlari_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)proje_calisanlari_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }

        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(proje_resmi.Image);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gorev_liste gorev_Liste= new gorev_liste();
            gorev_Liste.gorev_listeleme_turu = 1;
            Scripts.Form.FormManager.FORM_AC(gorev_Liste, true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
         
        }

     

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (aktif_gorevler_datagrid.Rows.Count > 0 && aktif_gorevler_datagrid.SelectedRows.Count > 0)
            {
                gorev gorev_Form = new gorev();
                gorev_Form.gorev_id = Convert.ToInt32(aktif_gorevler_datagrid.SelectedRows[0].Cells[0].Value);
                gorev_Form.ShowDialog();
            }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (tamamlanmis_gorevler.Rows.Count > 0 && tamamlanmis_gorevler.SelectedRows.Count > 0)
            {
                gorev gorev_Form = new gorev();
                gorev_Form.gorev_id = Convert.ToInt32(tamamlanmis_gorevler.SelectedRows[0].Cells[0].Value);
                gorev_Form.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(Scripts.SQL.SqlQueries.PROJE_YONETICISI_KONTROL(a.worker_id,a.project_id))
            Scripts.Form.FormManager.FORM_AC(new Forms.proje_guncelle(), true);
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView3_CellMouseEnter((DataGridView)sender, e);
        }

        private void dataGridView3_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count > 0 && e.RowIndex == dataGridView.SelectedRows[0].Index)
            {
                string[] positions = Scripts.SQL.SqlQueries.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                string P = dataGridView.SelectedRows[0].Cells[2].Value.ToString() + "'nin pozisyonları \n";
                for (int i = 0; i < positions.Length; i++)
                {
                    P += positions[i] + "\n";
                }
                System.Drawing.Rectangle cellRect = dataGridView.GetCellDisplayRectangle(2, e.RowIndex, false);
                toolTip1.Show(P,
                              this,
                              dataGridView.Location.X + cellRect.X ,
                              dataGridView.Location.Y + cellRect.Y + cellRect.Size.Height );    // Duration: 5 seconds.
            }

        }

        private void dataGridView3_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            toolTip1.Hide(this);
        }
    }
}

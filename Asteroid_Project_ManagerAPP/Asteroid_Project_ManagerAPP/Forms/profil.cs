using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Asteroid_Project_ManagerAPP
{
    public partial class profil : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        DataTable table=new DataTable();
        DataTable dataTable = new DataTable();
        SqlCommand komut = new SqlCommand();
        public profil()
        {
            InitializeComponent();
            this.AcceptButton = gorevlere_git;
        }
        public void profil_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT worker_name,worker_image,worker_mail,worker_gender FROM WORKERS WHERE worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görevler veritabanından çekilemedi.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if(table != null)
            {
                isim.Text = table.Rows[0]["worker_name"].ToString();
                cinsiyet.Text = table.Rows[0]["worker_gender"].ToString();
                email.Text = table.Rows[0]["worker_mail"].ToString();
                try
                {
                    pozisyonlar_listview.Items.AddRange(Scripts.SQL.SqlQueries.WORKER_ROLE_CALL_BY_ID(a.worker_id));
                }
                catch
                {
                    
                }
                pictureBox1.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["worker_image"]);
            }
            komut = new SqlCommand("select DISTINCT PROJECTS.project_id,PROJECTS.project_name,PROJECTS.project_start_date,PROJECTS.project_finish_date FROM PROJECTS INNER JOIN PROJECT_WORKERS on(PROJECT_WORKERS.project_id = PROJECTS.project_id AND PROJECT_WORKERS.worker_id = @WORKER_ID)");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görevler veritabanından çekilemedi.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            dataTable = new DataTable();
            dataTable.Columns.Add("Proje ID", typeof(int));
            dataTable.Columns.Add("Proje ismi", typeof(string));
            dataTable.Columns.Add("Proje Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Proje Bitiş Tarihi", typeof(string));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_start_date"].ToString()), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_finish_date"].ToString())) ;
            }
            gorev_aldigi_projeler_datagrid.DataSource = dataTable;
            dataGridView1_CellClick(this.gorev_aldigi_projeler_datagrid, null);
            komut = new SqlCommand("SELECT TASKS.task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name,task_start_date, task_finish_date FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = @WORKER_ID WHERE(TASK_WORKERS.task_id = TASKS.task_id AND TASKS.task_status<>'Görev Tamamlandı')");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görevler veritabanından çekilemedi.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            dataTable = new DataTable();
            dataTable.Columns.Add("Görev ID", typeof(int));
            dataTable.Columns.Add("Proje İsmi", typeof(string));
            dataTable.Columns.Add("Görev İsmi", typeof(string));
            dataTable.Columns.Add("Görev Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Görev Bitiş Tarihi", typeof(string));
            for(int i = 0;i<table.Rows.Count;i++)
            {
                if ((DateTime)table.Rows[i]["task_finish_date"] >= Scripts.SQL.SqlQueries.GET_SERVER_DATE() && (DateTime)table.Rows[i]["task_finish_date"] <= Scripts.SQL.SqlQueries.GET_SERVER_DATE().AddDays(8))
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_name"].ToString(),Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_start_date"].ToString()), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString())) ;
            }
            bu_hafta_bitecek_gorev_datagrid.DataSource = dataTable;
            komut = new SqlCommand("SELECT (SELECT COUNT(DISTINCT TASK_WORKERS.task_id) FROM TASK_WORKERS WHERE TASK_WORKERS.worker_id  =W.worker_id)as gorev_aldigi,(SELECT  COUNT(DISTINCT TASKS.task_id) FROM TASKS)as gorev_sayisi,(SELECT COUNT(TASKS.task_id) FROM TASKS,TASK_WORKERS WHERE TASK_WORKERS.task_id=TASKS.task_id and TASK_WORKERS.worker_id = W.worker_id and task_status <>'Görev Tamamlandı')as tamamlanmayan,(SELECT COUNT(TASKS.task_id) FROM TASKS,TASK_WORKERS WHERE TASK_WORKERS.task_id=TASKS.task_id and TASK_WORKERS.worker_id = W.worker_id and task_status ='Görev Tamamlandı')as tamamlanan FROM WORKERS W WHERE W.worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri veritabanından çekilemedi.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                //LABEL 10 TAMAMLAMA
                double gorev_tamamlama = (int)((Convert.ToDouble(table.Rows[i]["tamamlanan"]) / (Convert.ToDouble(table.Rows[i]["tamamlanmayan"])+ (Convert.ToDouble(table.Rows[i]["tamamlanan"])))) * 100);
                if (gorev_tamamlama < 0) gorev_tamamlama = 0;
                //LABEL 13 GOREV ALMA
                double gorev_alma = (int)((Convert.ToDouble(table.Rows[i]["gorev_aldigi"]) / Convert.ToDouble(table.Rows[i]["gorev_sayisi"])) * 100);
                if (gorev_alma < 0) gorev_alma = 0;
                gorev_tamamlama_label.Text = "%" + gorev_tamamlama;
                gorev_alma_label.Text = "%" + gorev_alma;
                gorev_tamamlama_label.ForeColor = Scripts.Tools.ColorTools.RED_YELLOW_GREEN_100(gorev_tamamlama);
                gorev_alma_label.ForeColor = Scripts.Tools.ColorTools.RED_YELLOW_GREEN_100(gorev_alma);
            }
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e) //profil resmi ac
        {
            Scripts.Tools.ImageTools.OpenImage(pictureBox1.Image);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(gorev_aldigi_projeler_datagrid.Rows.Count != 0)
            {
                if(gorev_aldigi_projeler_datagrid.SelectedRows.Count != 0)
                {
                    a.project_id = Convert.ToInt32(gorev_aldigi_projeler_datagrid.SelectedRows[0].Cells[0].Value);
                    a.ANAFORM_BILGILER_GUNCELLE();
                    a.menuStrip2.Visible = true;
                    Scripts.Form.FormManager.FORM_AC(new gorev_liste(), true);
                }
                else Scripts.Form.Status.STATUS_LABEL("Uyarı : Lütfen görevlerine gitmek istediğiniz bir proje seçin.", Color.Yellow, 2000);
            }
            else Scripts.Form.Status.STATUS_LABEL("Durum: Herhangibir projede değilsiniz.", Color.White, 2000);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gorev_aldigi_projeler_datagrid.SelectedRows.Count == 1)
            {
                komut = new SqlCommand("select task_status, TASKS.task_id FROM TASKS INNER JOIN TASK_WORKERS ON(TASKS.task_id = TASK_WORKERS.task_id AND worker_id = @WORKER_ID AND TASKS.project_id = @PROJECT_ID)");
                komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                komut.Parameters.AddWithValue("@PROJECT_ID", gorev_aldigi_projeler_datagrid.SelectedRows[0].Cells[0].Value);
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri veritabanından çekilemedi.", Color.Red, 4000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                int tamamlanmisGorev = 0;
                int tamamlanmamisGorev = 0;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i][0].ToString() == "Görev Tamamlandı") tamamlanmisGorev++;
                    else tamamlanmamisGorev++;
                }
                label5.Text = "(" + gorev_aldigi_projeler_datagrid.Rows[0].Cells[0].Value + ") Tamamlanmamış Görev Sayısı";
                label6.Text = "(" + gorev_aldigi_projeler_datagrid.Rows[0].Cells[0].Value + ") Tamamlanmış Görev Sayısı";
                tamamlanmamis_gorev.Text = tamamlanmamisGorev.ToString();
                tamamlanmis_gorev.Text = tamamlanmisGorev.ToString();
            }
            else Scripts.Form.Status.STATUS_LABEL("Kayıtlı proje bulunamadı.", Color.White, 3000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.bilgileri_guncelle(), true);
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            if(bu_hafta_bitecek_gorev_datagrid.SelectedRows.Count > 0)
            {
                gorev gorev_Form = new gorev();
                gorev_Form.gorev_id = Convert.ToInt32(bu_hafta_bitecek_gorev_datagrid.SelectedRows[0].Cells[0].Value);
                gorev_Form.ShowDialog();
            }
        }
    }
}

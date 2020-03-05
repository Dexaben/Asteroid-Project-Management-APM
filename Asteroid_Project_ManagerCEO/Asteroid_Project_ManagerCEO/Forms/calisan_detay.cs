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

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class calisan_detay : Form
    {
        public int worker_id { get; set; }
        public calisan_detay()
        {
            InitializeComponent();
            this.AcceptButton = calisan_bilgileri_guncelle;
        }
        Image imag_Onayli;
        Image imag_Onaysiz;
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        private void calisan_detay_Load(object sender, EventArgs e)
        {
            this.Text = "Çalışan Bilgileri - ID:" + worker_id;
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources");
            filePath = System.IO.Path.Combine(filePath, "iconlar");
            imag_Onaysiz = Image.FromFile(filePath + @"\onaysiz.png");
            imag_Onayli = Image.FromFile(filePath + @"\onayli.png");
            komut = new SqlCommand("SELECT worker_name,worker_image,worker_gender,worker_onay,worker_mail FROM WORKERS WHERE worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 4000);
            if(table != null)
            {                calisan_resmi.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["worker_image"]);
                calisan_ismi.Text = table.Rows[0]["worker_name"].ToString();
                calisan_cinsiyet.Text = table.Rows[0]["worker_gender"].ToString();
                calisan_email.Text = table.Rows[0]["worker_mail"].ToString();
                if(Convert.ToBoolean(table.Rows[0]["worker_onay"]))
                {
                    onay_label.Text = "Onaylı";
                    onay_label.ForeColor = Color.Green;
                    onay_resim.Image = imag_Onayli;
                }
                else
                {
                    onay_label.Text = "Onaysız";
                    onay_label.ForeColor = Color.Red;
                    onay_resim.Image = imag_Onaysiz;
                }
                komut = new SqlCommand("SELECT worker_job FROM WORKER_POSITIONS WHERE worker_id = @WORKER_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan pozisyon alınamadı.", Color.Red, 4000);
                calisan_pozisyonlari.Items.Clear();
                if (table !=null)
                {
                    for(int i = 0;i<table.Rows.Count;i++)
                    {
                        calisan_pozisyonlari.Items.Add(table.Rows[i]["worker_job"].ToString());
                    }
                }
                
                komut = new SqlCommand("SELECT P.project_id,P.project_name,P.project_image,P.project_manager,P.project_start_date,P.project_finish_date,(SELECT COUNT(worker_id) FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.project_id=P.project_id)AS project_workers_count,(SELECT COUNT(task_id) FROM TASKS WHERE TASKS.project_id = P.project_id AND TASKS.task_status<>'Görev Tamamlandı')AS uncomplated_tasks FROM PROJECTS P WHERE P.project_manager = @WORKER_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Proje bilgileri alınırken hata oluştu.", Color.Red, 4000);
                DataTable MANAGED_PROJECTS = new DataTable();
                MANAGED_PROJECTS.Columns.Add("Proje ID", typeof(int));
                MANAGED_PROJECTS.Columns.Add("Proje İsmi", typeof(string));
                MANAGED_PROJECTS.Columns.Add("Proje Resmi", typeof(Image));
                MANAGED_PROJECTS.Columns.Add("Proje Yöneticisi", typeof(string));
                MANAGED_PROJECTS.Columns.Add("Proje Başlangıç Tarihi", typeof(string));
                MANAGED_PROJECTS.Columns.Add("Proje Bitiş Tarihi", typeof(string));
                MANAGED_PROJECTS.Columns.Add("Projedeki Çalışan Sayısı", typeof(int));
                MANAGED_PROJECTS.Columns.Add("Projedeki Tamamlanmamış Görev Sayısı", typeof(int));
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        MANAGED_PROJECTS.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["project_image"]), table.Rows[i]["project_manager"].ToString(), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_start_date"].ToString()), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_finish_date"].ToString()), Convert.ToInt32(table.Rows[i]["project_workers_count"]), Convert.ToInt32(table.Rows[i]["uncomplated_tasks"]));
                    } 
                }
                yoneticilik_yaptigi_projeler.DataSource = MANAGED_PROJECTS;
                for (int i = 0; i < yoneticilik_yaptigi_projeler.Columns.Count; i++)
                    if (yoneticilik_yaptigi_projeler.Columns[i] is DataGridViewImageColumn)
                    {
                        ((DataGridViewImageColumn)yoneticilik_yaptigi_projeler.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                        break;
                    }
                komut = new SqlCommand("SELECT P.project_id,P.project_name,P.project_image,P.project_manager,P.project_start_date,P.project_finish_date,(SELECT COUNT(worker_id) FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.project_id=P.project_id)AS project_workers_count,(SELECT COUNT(task_id) FROM TASKS WHERE TASKS.project_id = P.project_id AND TASKS.task_status<>'Görev Tamamlandı')AS uncomplated_tasks FROM PROJECTS P WHERE P.project_id IN (SELECT PROJECT_WORKERS.project_id FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.worker_id = @WORKER_ID)");
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Proje bilgileri alınırken hata oluştu.", Color.Red, 4000);
                DataTable PROJELER = new DataTable();
                PROJELER.Columns.Add("Proje ID", typeof(int));
                PROJELER.Columns.Add("Proje İsmi", typeof(string));
                PROJELER.Columns.Add("Proje Resmi", typeof(Image));
                PROJELER.Columns.Add("Proje Yöneticisi", typeof(string));
                PROJELER.Columns.Add("Proje Başlangıç Tarihi", typeof(string));
                PROJELER.Columns.Add("Proje Bitiş Tarihi", typeof(string));
                PROJELER.Columns.Add("Projedeki Çalışan Sayısı", typeof(int));
                PROJELER.Columns.Add("Projedeki Tamamlanmamış Görev Sayısı", typeof(int));
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        PROJELER.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["project_image"]), table.Rows[i]["project_manager"].ToString(), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_start_date"].ToString()), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_finish_date"].ToString()), Convert.ToInt32(table.Rows[i]["project_workers_count"]), Convert.ToInt32(table.Rows[i]["uncomplated_tasks"]));
                    }
                }
                gorev_aldigi_projeler.DataSource = PROJELER;
                for (int i = 0; i < gorev_aldigi_projeler.Columns.Count; i++)
                    if (gorev_aldigi_projeler.Columns[i] is DataGridViewImageColumn)
                    {
                        ((DataGridViewImageColumn)gorev_aldigi_projeler.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                        break;
                    }
                if (gorev_aldigi_projeler.Rows.Count > 0)
                    gorev_aldigi_projeler.Rows[0].Selected = true;
                dataGridView1_Click(this, null);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            F.OpenImage(calisan_resmi.Image);
        }
        private void dataGridView1_Click(object sender, EventArgs e) //projeler 
        {
            if (gorev_aldigi_projeler.Rows.Count > 0 && gorev_aldigi_projeler.SelectedRows.Count > 0)
            {
                label9.Text = "Görevler - Proje ID:" + Convert.ToInt32(gorev_aldigi_projeler.SelectedRows[0].Cells[0].Value);
                komut = new SqlCommand("SELECT T.task_id, (SELECT PROJECTS.project_name FROM PROJECTS WHERE PROJECTS.project_id = T.project_id)AS project_name, T.task_name,T.task_status,T.task_start_date,T.task_finish_date,(SELECT COUNT(worker_id) FROM TASK_WORKERS WHERE TASK_WORKERS.task_id = T.task_id)AS task_workers_count,(SELECT COUNT(TASKS.task_id) FROM TASKS WHERE TASKS.project_id = T.project_id AND TASKS.task_status <> 'Görev Tamamlandı')AS tamamlanmayan_gorev_say,(SELECT COUNT(TASKS.task_id) FROM TASKS WHERE TASKS.project_id = T.project_id AND TASKS.task_status = 'Görev Tamamlandı')AS tamamlanan_gorev_say FROM TASKS T WHERE project_id = @PROJECT_ID");
                komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(gorev_aldigi_projeler.SelectedRows[0].Cells[0].Value));
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Proje bilgileri alınırken hata oluştu.", Color.Red, 4000);
                DataTable GOREVLER = new DataTable();
                GOREVLER.Columns.Add("Görev ID", typeof(int));
                GOREVLER.Columns.Add("Görev Projesi", typeof(string));
                GOREVLER.Columns.Add("Görev İsmi", typeof(string));
                GOREVLER.Columns.Add("Görev Durumu", typeof(string));
                GOREVLER.Columns.Add("Görev Başlangıç Tarihi", typeof(string));
                GOREVLER.Columns.Add("Görev Bitiş Tarihi", typeof(string));
                GOREVLER.Columns.Add("Görevdeki Çalışan Sayısı", typeof(int));
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        GOREVLER.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_name"].ToString(), table.Rows[i]["task_status"].ToString(), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_start_date"].ToString()), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString()), Convert.ToInt32(table.Rows[i]["task_workers_count"]));
                        tamamlanmamis_gorev.Text = table.Rows[i]["tamamlanmayan_gorev_say"].ToString();
                        tamamlanmis_gorev.Text = table.Rows[i]["tamamlanan_gorev_say"].ToString();
                    }
                    
                }
                
                gorevleri_datagrid.DataSource = GOREVLER;
            }
           
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e) //projeler
        {
            proje_detay prj_dty = new proje_detay();
            prj_dty.project_id = Convert.ToInt32(gorev_aldigi_projeler.SelectedRows[0].Cells[0].Value);
            prj_dty.ShowDialog();
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e) //gorevleri
        {
            gorev grv = new gorev();
            grv.gorev_id = Convert.ToInt32(gorevleri_datagrid.SelectedRows[0].Cells[0].Value);
            grv.ShowDialog();
        }

        private void dataGridView3_DoubleClick(object sender, EventArgs e) //yöneticilik yaptıgı
        {
            proje_detay prj_dty = new proje_detay();
            prj_dty.project_id = Convert.ToInt32(yoneticilik_yaptigi_projeler.SelectedRows[0].Cells[0].Value);
            prj_dty.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new calisan(), true);
            this.Close();
        }
    }
}

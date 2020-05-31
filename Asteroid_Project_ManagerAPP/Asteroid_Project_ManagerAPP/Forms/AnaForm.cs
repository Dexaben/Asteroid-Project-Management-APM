using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    public partial class AnaForm : Form
    {
        public  int worker_id=0;
        public int project_id =0;
  
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        public TimeSpan timer_baslangic;
        public DateTime sonGirisZamani;

        public AnaForm()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e) //ZAMAN FARKINA GÖRE SAATİ HESAPLIYOR VE GÖSTERİYOR.
        {
            panel_saat.Text = (DateTime.Now - timer_baslangic).ToString("T");
        }
      
        private void AnaForm_Load(object sender, EventArgs e)
        {
            timer_baslangic = DateTime.Now- Scripts.SQL.SqlQueries.GET_SERVER_DATE(); //LOCAL MAKİNE ZAMANI İLE SERVER MAKİNESİ ZAMANININ , ZAMAN FARKINI ALIYOR.
            sonGirisZamani = Scripts.SQL.SqlQueries.GET_SERVER_DATE(); //UYGULAMAYA GİRİŞ ZAMANI ALINIYOR.
            panel_tarih.Text = Scripts.SQL.SqlQueries.GET_SERVER_DATE().ToString("dddd, dd MMMM yyyy");
            timer1.Start();
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White; //MDI CONTAINER ARKA PLAN RENGİNİ BEYAZ YAPIYOR.
            menuStrip1.Visible = false;
            Scripts.Form.FormManager.FORM_AC(new calisan_giris(), true);
            Scripts.Form.Status.STATUS_LABEL("Durum : Kayıt,giriş bekleniyor.",Color.White);
        }
        public void ANAFORM_BILGILER_GUNCELLE()
        {

            if (worker_id != 0) //ANA PANEL UZERİNDEKİ PROFİL BİLGİLERİNİ SENKRONİZE EDER
            {
                menuStrip1.Visible = true;
                profil_bilgileri_groupBox.Visible = true;
                komut = new SqlCommand("SELECT worker_id,worker_name,worker_image,worker_gender FROM WORKERS WHERE worker_id=@wrk_id");
                komut.Parameters.AddWithValue("@wrk_id", worker_id);
                komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata Bilgiler güncellenirken sorun oluştu.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if (table.Rows.Count != 0)
                {
                    profil_bilgileri_groupBox.Text = "Profil Bilgileri - ID(" + table.Rows[0]["worker_id"].ToString() + ")";
                    profil_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["worker_image"]);
                    textBox6.Text = (table.Rows[0]["worker_name"]).ToString();
                    if (Scripts.SQL.SqlQueries.WORKER_HAS_ROLE_BY_WORKER_ID_AND_WORKER_ROLE(Convert.ToInt32(table.Rows[0]["worker_id"]), "Proje Yöneticisi"))
                    {
                        proje_yoneticisi_label.Visible = true;
                        proje_yoneticisi_label.Text = "Proje Yöneticisi";
                        proje_yoneticisi_logo.Visible = true;
                    }
                    else { proje_yoneticisi_label.Visible = false; proje_yoneticisi_logo.Visible = false; }
                }
            }
            else
            {
                menuStrip1.Visible = false;
                profil_bilgileri_groupBox.Visible = false;
                Application.Exit();
            }
            if (project_id != 0) //ANA PANEL UZERİNDEKİ GOREV VE PROJE BİLGİLERİNİ SENKRONİZE EDER
            {
                proje_bilgileri_groupBox.Visible = true;
                gorev_bilgileri_groupBox.Visible = true;
                komut = new SqlCommand("SELECT TASKS.task_status,TASKS.task_id,(SELECT PROJECTS.project_name  FROM PROJECTS WHERE (TASKS.project_id = PROJECTS.project_id))as project_name, task_name,task_finish_date FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = @WORKER_ID WHERE(project_id = @PROJECT_ID AND TASK_WORKERS.task_id = TASKS.task_id AND TASKS.task_status<>'Görev Tamamlandı')");
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                komut.Parameters.AddWithValue("@PROJECT_ID", project_id);
                komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                Scripts.SQL.SQL_COMMAND  sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:En yakın görev bilgisi alınırken sorun oluştu.", Color.Red, 2000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                tasks = new List<Tasks>(table.Rows.Count);
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Tasks t = new Tasks();
                    t.task_id = Convert.ToInt32(table.Rows[i]["task_id"]);
                    t.task_name = table.Rows[i]["task_name"].ToString();
                    t.task_project = table.Rows[i]["project_name"].ToString();
                    DateTime dt = Convert.ToDateTime(table.Rows[i]["task_finish_date"].ToString());
                    t.task_finish_date = dt;
                    if (t.task_finish_date >= Scripts.SQL.SqlQueries.GET_SERVER_DATE()) tasks.Add(t);
                }
                if (tasks.Count > 0)
                {
                    bubbleSort(tasks);
                    en_yakin_gorev.Text = tasks[0].task_name;
                    gorev_tarih.Text = Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(tasks[0].task_finish_date.ToString());
                    gorev_proje_ismi.Text = tasks[0].task_project;
                }
                else gorev_bilgileri_groupBox.Visible = false;

                komut = new SqlCommand("SELECT project_id,project_name,project_image,project_detail,project_start_date,project_finish_date,project_status,(SELECT worker_name FROM WORKERS WHERE worker_id=project_manager)AS project_manager,(SELECT department_name FROM DEPARTMENTS WHERE (department_id = PROJECTS.department_id))AS department_name FROM PROJECTS WHERE project_id=@project_id");
                komut.Parameters.AddWithValue("@project_id", project_id);
                komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                 sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje bilgileri alınırken hata oluştu.", Color.Red, 2000);
              table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if(table.Rows.Count != 0)
                {
                    proje_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["project_image"]);
                    proje_bilgileri_groupBox.Text = "Proje Bilgileri (ID-" + (table.Rows[0]["project_id"]).ToString() + ")";
                    projeismi.Text = (table.Rows[0]["project_name"]).ToString();
                    projeYoneticisi.Text = (table.Rows[0]["project_manager"]).ToString();
                    projeDetay.Text = (table.Rows[0]["project_detail"]).ToString();
                    projeDepartmani.Text = (table.Rows[0]["department_name"]).ToString();
                }
                else Application.Exit();
            }
            else
            {
                proje_bilgileri_groupBox.Visible = false;
                gorev_bilgileri_groupBox.Visible = false;
            }
        }
        List<Tasks> tasks;
        class Tasks
        {
            public int task_id { get; set; }
            public string task_name { get; set; }
            public string task_project { get; set; }
            public DateTime task_finish_date { get; set; }
        }
        static void bubbleSort(List<Tasks> arr) //GOREVLERİ EN YAKIN BİTİŞ TARIHINE GORE SIRALAR
        {
            int n = arr.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = 0; j < n - i - 1; j++)
                    if (arr[j].task_finish_date.Date > arr[j + 1].task_finish_date.Date)
                    {
                        Tasks temp = arr[j+1];
                        arr[j+1] = arr[j];
                        arr[j] = temp;
                    }
        }
        private void sohbetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (project_id != 0)
            {
                Scripts.Form.FormManager.FORM_AC(new sohbet(), true);
            }
            else Scripts.Form.Status.STATUS_LABEL("Durum: Herhangibir proje seçimi yapılmamış.", Color.White, 2000);
        }

        private void profilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new profil(), true);
        }

        private void projelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(project_id != 0)
            {
                Scripts.Form.FormManager.FORM_AC(new projem(), true);
            }
            else
            {
                Scripts.Form.FormManager.FORM_AC(new projeler(), true);
            }
        }

        private void gorevlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (project_id != 0)
            {
                menuStrip2.Visible = true;
                Scripts.Form.FormManager.FORM_AC(new gorev_liste(), true);
            }
            else Scripts.Form.Status.STATUS_LABEL("Durum: Herhangibir proje seçimi yapılmamış.", Color.White, 2000);
        }

        private void takimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (project_id != 0)
            {
                Scripts.Form.FormManager.FORM_AC(new team(), true);
            }
            else Scripts.Form.Status.STATUS_LABEL("Durum: Herhangibir proje seçimi yapılmamış.", Color.White, 2000);
        }
        private void görevEkleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new gorev_ekle(), true);
        }
        private void görevlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new gorev_liste(), true);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            gorev gorev_Form = new gorev();
            gorev_Form.gorev_id = tasks[0].task_id;
            gorev_Form.ShowDialog(); //EN YAKIN BİTİŞ TARİHİNE GÖRE SIRALANAN GÖREVLERİN İLKİNİ AÇIYOR.
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
           Scripts.Tools.ImageTools.OpenImage(profil_resmi.Image);
        }
        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(proje_resmi.Image);
        }

        private void takvimToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.gorev_takvim(), true);
        }

        private void sonTeslimTarihiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.deadline(), true);
        }

        private void AnaForm_FormClosed(object sender, FormClosedEventArgs e) //PROGRAM KAPATILDIGINDA ÇALIŞACAK FONKSIYON
        {
            if(worker_id != 0)
            {
                Scripts.Tools.LogTools.LOG_ENTER(worker_id, Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(worker_id)+" adlı çalışan uygulamadan çıkış yaptı. Uygulamada kalma süresi : "+ (Scripts.SQL.SqlQueries.GET_SERVER_DATE() - sonGirisZamani), Scripts.SQL.SqlQueries.GET_SERVER_DATE());
            }
        }

     
    }
}

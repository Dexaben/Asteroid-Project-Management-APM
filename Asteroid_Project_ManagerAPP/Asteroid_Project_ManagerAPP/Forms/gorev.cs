using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Asteroid_Project_ManagerAPP
{
    public partial class gorev : Form
    {
        public int gorev_id;
        public gorev()
        {
            InitializeComponent();
            this.AcceptButton = gorev_durumunu_guncelle;
        }

        FUNCTIONS F = new FUNCTIONS();
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        List<Image> task_images = new List<Image>();
        public void gorev_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT  T.task_name,T.task_details,T.task_start_date,T.task_finish_date,T.task_status,T.task_urgency,(SELECT PROJECTS.project_name FROM PROJECTS WHERE(PROJECTS.project_id = T.project_id)) AS project_name,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE(DEPARTMENTS.department_id = (SELECT PROJECTS.department_id FROM PROJECTS WHERE(PROJECTS.project_id = T.project_id)))) AS department_name FROM TASKS T WHERE  T.task_id = @task_id");
            komut.Parameters.AddWithValue("@task_id", gorev_id);
            table = F.SQL_SELECT_DATATABLE(komut,"Hata:Görev bilgileri alınırken hata oluştu.",Color.Red,2000);
            if(table.Rows.Count !=0)
            {
                if (table.Rows[0]["task_urgency"].ToString() == "Acil") groupBox1.Visible = true;
                else groupBox1.Visible = false;
                this.Text = "Görev (ID-" + gorev_id+")";
                gorev_projesi.Text = table.Rows[0]["project_name"].ToString();
                gorev_departmani.Text = table.Rows[0]["department_name"].ToString();
                gorev_textbox.Text =table.Rows[0]["task_name"].ToString();
                gorev_detay.Text = table.Rows[0]["task_details"].ToString();
                gorev_baslangic_tarihi.Text = F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["task_start_date"].ToString());
                gorev_bitis_tarihi.Text = F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["task_finish_date"].ToString()) ;
                durum_Text.Text = table.Rows[0]["task_status"].ToString();
                if (table.Rows[0]["task_status"].ToString() == "Görev Tamamlandı")
                {
                    gorev_tamamla.Visible = false;
                    gorev_durumunu_guncelle.Visible = false;
                    gorev_bilgilerini_guncelle.Visible = false;
                    durum_Text.BackColor = Color.Green;
                    durum_Text.ForeColor = Color.White;
                }
                if(!GOREV_CALISANLARINDANMIYIM(a.worker_id))
                {
                    gorev_tamamla.Visible = false;
                    gorev_durumunu_guncelle.Visible = false;
                    gorev_bilgilerini_guncelle.Visible = false;
                }
            }
            komut = new SqlCommand("SELECT task_image FROM TASK_IMAGES WHERE task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", gorev_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:", Color.Red, 3000);
            task_images.Clear();
            if(table != null)
            {
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    task_images.Add(F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["task_image"]));
                }
                resim_goster(0);
            }
            komut = new SqlCommand("SELECT TASK_WORKERS.worker_id,worker_name,worker_image FROM TASK_WORKERS INNER JOIN WORKERS ON TASK_WORKERS.worker_id = WORKERS.worker_id WHERE TASK_WORKERS.task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", gorev_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan isimleri alınırken sorun oluştu.", Color.Red, 3000);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            if (table.Rows.Count != 0)
            {
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString(), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]));
                }
            }
            dataGridView1.DataSource = dataTable;
            ((DataGridViewImageColumn)dataGridView1.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (GOREV_CALISANLARINDANMIYIM(a.worker_id))
            {
                komut = new SqlCommand("UPDATE TASKS SET task_status = @task_status WHERE task_id=@task_id");
                komut.Parameters.AddWithValue("@task_id", gorev_id);
                komut.Parameters.AddWithValue("@task_status", "Görev Tamamlandı");
                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Görev durumu güncellenemedi.", Color.Red, 3000))
                {
                    F.LOG_ENTER(a.worker_id, $"{F.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {gorev_textbox.Text} görevinin durumunu ({durum_Text.Text} => {"Görev Tamamlandı"}) güncelledi.Görev ID:{gorev_id},Görevin Projesi:{gorev_projesi.Text}", F.GET_SERVER_DATE()) ;
                    durum_Text.Text = "Görev Tamamlandı";
                    gorev_tamamla.Visible = false;
                    gorev_durumunu_guncelle.Visible = false;
                    durum_Text.BackColor = Color.Green;
                    durum_Text.ForeColor = Color.White;
                    gorev_liste gl = (gorev_liste)Application.OpenForms["gorev_liste"];
                    if (gl != null)
                    gl.gorev_liste_Load(gl, null);
                    projem projem_ = (projem)Application.OpenForms["projem"];
                    if (projem_ != null)
                        projem_.projem_Load(projem_, null);
                    Forms.deadline deadline_ = (Forms.deadline)Application.OpenForms["deadline"];
                    if (deadline_ != null)
                        deadline_.deadline_Load(deadline_, null);
                    Forms.gorev_takvim takvim = (Forms.gorev_takvim)Application.OpenForms["gorev_takvim"];
                    if (takvim != null)
                        takvim.gorev_takvim_Load(takvim, null);
                    profil profil_ = (profil)Application.OpenForms["profil"];
                    if (profil_ != null)
                        profil_.profil_Load(profil_, null);
                }
            }
            else F.DURUM_LABEL("Bu görevi yönetmeye yetkiniz yok.", Color.White, 4000);
          
        }
        bool GOREV_CALISANLARINDANMIYIM(int worker_id)
        {
            komut = new SqlCommand("SELECT COUNT(task_id)AS task_count,(SELECT TASKS.task_finish_date FROM TASKS WHERE TASKS.task_id=@TASK_ID)AS task_finish_date FROM TASK_WORKERS WHERE  task_id = @TASK_ID AND worker_id = @WORKER_ID");
            komut.Parameters.AddWithValue("@TASK_ID", gorev_id);
            komut.Parameters.AddWithValue("@WORKER_ID",a.worker_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgisi alınamadı.", Color.Red, 3000);
            if (table != null)
            {
                if (Convert.ToInt32(table.Rows[0][0]) != 0)
                {
                    DateTime dt = F.DATETIME_CONVERTER(table.Rows[0][1].ToString());
                    if (dt >= F.GET_SERVER_DATE())
                    {
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Görevin süresi dolmuş.");
                        return false;
                    }                } 
                else return false;
            }
            else return false;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(GOREV_CALISANLARINDANMIYIM(a.worker_id))
            {
                string t = Interaction.InputBox("Görevin şuanki durumunu kısaca girin.", "Görev Durumunu Güncelle", durum_Text.Text, -1, -1);
                if (t.Length > 0 && t.Length < 251)
                {
                    komut = new SqlCommand("UPDATE TASKS SET task_status = @task_status WHERE task_id=@task_id");
                    komut.Parameters.AddWithValue("@task_id", gorev_id);
                    komut.Parameters.AddWithValue("@task_status", t);
                    if (F.SQL_EXECUTENONQUERY(komut, "Hata:Görev durumu güncellenemedi.", Color.Red, 3000))
                    {
                        F.LOG_ENTER(a.worker_id, $"{F.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {gorev_textbox.Text} görevinin durumunu ({durum_Text.Text} => {t}) güncelledi.Görev ID:{gorev_id},Görevin Projesi:{gorev_projesi.Text}", F.GET_SERVER_DATE());
                        durum_Text.Text = t;
                        gorev_liste gl = (gorev_liste)Application.OpenForms["gorev_liste"];
                        if (gl!=null)
                        gl.gorev_liste_Load(gl, null);
                        projem projem_ = (projem)Application.OpenForms["projem"];
                        if (projem_ != null)
                            projem_.projem_Load(projem_, null);
                        Forms.deadline deadline_ = (Forms.deadline)Application.OpenForms["deadline"];
                        if (deadline_ != null)
                            deadline_.deadline_Load(deadline_, null);
                        Forms.gorev_takvim takvim = (Forms.gorev_takvim)Application.OpenForms["gorev_takvim"];
                        if (takvim != null)
                            takvim.gorev_takvim_Load(takvim, null);
                        profil profil_ = (profil)Application.OpenForms["profil"];
                        if (profil_ != null)
                            profil_.profil_Load(profil_, null);
                    }
                }
                else
                { F.DURUM_LABEL("Hata:Görev durumu boş veya 250 karakterden uzun olamaz.", Color.PaleVioletRed, 2000); }
            }
            else F.DURUM_LABEL("Bu görevi yönetmeye yetkiniz yok.", Color.White, 4000);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if(task_images.Count>0)
            F.OpenImage(gorev_Resim.Image);
        }

        private void resim_Geri_Click(object sender, EventArgs e)
        {
            if (task_images.Count > 0)
            {
                if (current_task_image_index - 1 >= 0)
                {
                    current_task_image_index--;

                }
                else F.DURUM_LABEL("Zaten ilk resimdesiniz.", Color.White, 3000);
            }
            resim_goster(current_task_image_index);
        }

        private void resim_Ileri_Click(object sender, EventArgs e)
        {
            if (task_images.Count > 0)
            {
                if (current_task_image_index + 1 < task_images.Count)
                {
                    current_task_image_index++;
                }
                else F.DURUM_LABEL("Zaten son resimdesiniz.", Color.White, 3000);
            }
            resim_goster(current_task_image_index);
        }
        int current_task_image_index = 0;
        void resim_goster(int index)
        {
            if (task_images.Count > 0)
            {
                resim_Geri.Visible = true;
                resim_Ileri.Visible = true;
                gorev_Resim.Image = task_images[index];
                current_task_image_index = index;
                label2.Text = "Görev Resmi (" + (index + 1) + "/" + task_images.Count + ")";
            }
            else
            {
                resim_Geri.Visible = false;
                resim_Ileri.Visible = false;
                var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
                gorev_Resim.Image = Image.FromFile(filePath + @"\default_task_image.jpg");
                label2.Text = "Görev Resmi";
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1_CellMouseEnter((DataGridView)sender, e);

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
                System.Drawing.Rectangle cellRect = dataGridView.GetCellDisplayRectangle(1, e.RowIndex, false);
                toolTip1.Show(P,
                              this,
                              dataGridView.Location.X + cellRect.X+10,
                              dataGridView.Location.Y + cellRect.Y + cellRect.Size.Height+30);    // Duration: 5 seconds.
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void gorev_Bilgi_Guncelle(object sender, EventArgs e)
        {
            if (GOREV_CALISANLARINDANMIYIM(a.worker_id))
            {
                Forms.gorev_bilgileri_guncelle gorev_Bilgileri_Guncelle = new Forms.gorev_bilgileri_guncelle();
                gorev_Bilgileri_Guncelle.task_id = gorev_id;
                gorev_Bilgileri_Guncelle.ShowDialog();

            }

        }
    }
}

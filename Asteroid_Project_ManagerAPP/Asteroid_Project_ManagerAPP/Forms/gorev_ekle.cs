using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Asteroid_Project_ManagerAPP
{
    public partial class gorev_ekle : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        FUNCTIONS F = new FUNCTIONS();
        public gorev_ekle()
        {
            InitializeComponent();
            this.AcceptButton = gorev_ekle_button;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (gorev_text.Text.Length > 0 && gorev_detay.Text.Length > 0)
            {
                kaydet();
            }
            else label8.Text = "Görev ismi ve Görev detayı boş bırakılamaz";
        }
        int TASK_ID_GENERATE(int[] task_ids)
        {
            int task_id;
            Random rnd = new Random();
            task_id = rnd.Next(1, 999999999);
            for(int i=0;i< task_ids.Length;i++)
            {
                if(task_id == task_ids[i])
                {
                    TASK_ID_GENERATE(task_ids);
                }
            }
            return task_id;
        }
        void kaydet()
        {

            komut = new SqlCommand("SELECT task_id FROM TASKS");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Görev bilgileri alınırken hata oluştu.", Color.Red, 3000);
            int[] task_ids = new int[table.Rows.Count];
            if (table.Rows.Count !=0)
            {
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    task_ids[i] = Convert.ToInt32(table.Rows[i][0]);
                }
            }
            int task_id = TASK_ID_GENERATE(task_ids);
            komut = new SqlCommand("INSERT INTO TASKS(task_id,task_name,project_id,task_details,task_urgency,task_start_date,task_finish_date,task_status) VALUES(@task_id,@task_name,@project_id,@task_details,@task_urgency,@task_start_date,@task_finish_date,@task_status)");
            komut.Parameters.AddWithValue("@task_id", task_id);
            komut.Parameters.AddWithValue("@task_name", gorev_text.Text);
            komut.Parameters.AddWithValue("@project_id", a.project_id);
            komut.Parameters.AddWithValue("@task_details", gorev_detay.Text);
            SqlDateTime time_Start = gorev_baslangic_tarih.Value;
            SqlDateTime time_Finish = gorev_bitis_tarih.Value;
            komut.Parameters.AddWithValue("@task_start_date", time_Start);
            komut.Parameters.AddWithValue("@task_finish_date", time_Finish);
            komut.Parameters.AddWithValue("@task_status", "Görev Çalışana Verildi.");
            if (acil_checkbox.Checked) komut.Parameters.AddWithValue("@task_urgency", "Acil");
            else komut.Parameters.AddWithValue("@task_urgency", "");
            if (F.SQL_EXECUTENONQUERY(komut,"Hata:Görev veritabanına eklenemedi.",Color.Red,2000))
            {
                string workers = "";
                for(int i = 0;i<gorev_calisanlari_datagrid.Rows.Count;i++)
                {
                    workers += $" {F.WORKER_NAME_BY_WORKER_ID(Convert.ToInt32(gorev_calisanlari_datagrid.Rows[i].Cells[0].Value))} ,";
                    komut = new SqlCommand("INSERT INTO TASK_WORKERS(task_id,worker_id) VALUES(@task_id,@worker_id)");
                    komut.Parameters.AddWithValue("@task_id", task_id);
                    komut.Parameters.AddWithValue("@worker_id", gorev_calisanlari_datagrid.Rows[i].Cells[0].Value);
                    F.SQL_EXECUTENONQUERY(komut,"Hata:Çalışan göreve eklenemedi.", Color.Red, 2000);
                }
                if(task_images.Count>0)
                {
                    for (int i = 0; i < task_images.Count; i++)
                    {
                        komut = new SqlCommand("INSERT INTO TASK_IMAGES(task_id,task_image) VALUES(@task_id,@task_image)");
                        komut.Parameters.AddWithValue("@task_id", task_id);
                        komut.Parameters.AddWithValue("@task_image", F.CONVERT_IMAGE_TO_BYTE_ARRAY(task_images[i], System.Drawing.Imaging.ImageFormat.Jpeg));
                        F.SQL_EXECUTENONQUERY(komut, "Hata:Görev görseli eklenemedi.", Color.Red, 4000);
                    }
                }
                F.LOG_ENTER(a.worker_id, $"{F.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {F.PROJECT_NAME_BY_PROJECT_ID(a.project_id)} projesine {gorev_text.Text} görevini ekledi.Görev ID:{task_id},Görev Başlangıç Tarihi:{time_Start},Görev Bitiş Tarihi:{time_Finish},Görev Çalışanları:{workers}",F.GET_SERVER_DATE());
                F.DURUM_LABEL(" | Görevi Eklendi. | " + task_id, Color.GreenYellow, 9000);
                F.FORM_AC(new gorev_liste(), true);
            }
        }
        private void gorev_ekle_Load(object sender, EventArgs e)
        {
            resmi_goster(0);
            gorev_calisanlari_datagrid.Columns.Add("worker_id","Çalışan ID");
            gorev_calisanlari_datagrid.Columns["worker_id"].ValueType = typeof(int);
            DataGridViewImageColumn gridViewImageColumn = new DataGridViewImageColumn();
            gridViewImageColumn.ValueType = typeof(Image);
            gridViewImageColumn.HeaderText = "Çalışan Resmi";
            gridViewImageColumn.Name = "worker_image";
            gorev_calisanlari_datagrid.Columns.Add(gridViewImageColumn);
            gorev_calisanlari_datagrid.Columns.Add("worker_name","Çalışan İsim");
            
            gorev_calisanlari_datagrid.Columns["worker_name"].ValueType = typeof(string);
            gorev_calisanlari_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < gorev_calisanlari_datagrid.Columns.Count; i++)
                if (gorev_calisanlari_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)gorev_calisanlari_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
            komut = new SqlCommand("SELECT project_start_date,project_finish_date FROM PROJECTS WHERE project_id=@project_id");
            komut.Parameters.AddWithValue("@project_id", a.project_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Proje bilgileri alınırken hata oluştu.", Color.Red, 3000);
            if(table.Rows.Count !=0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    DateTime projectStart = (DateTime)table.Rows[i]["project_start_date"];
                    DateTime projectFinish = (DateTime)table.Rows[i]["project_finish_date"];
                    gorev_baslangic_tarih.MinDate = projectStart;
                    gorev_bitis_tarih.MinDate = projectStart.AddDays(1);
                    gorev_baslangic_tarih.MaxDate = projectFinish.AddDays(-1);
                    gorev_bitis_tarih.MaxDate = projectFinish;
                }
                gorev_baslangic_tarih.Value = gorev_baslangic_tarih.MinDate;
                gorev_bitis_tarih.Value = gorev_bitis_tarih.MinDate;
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (gorev_bitis_tarih.Value <= gorev_baslangic_tarih.Value)
            {
                F.DURUM_LABEL("Hata: Proje bitiş tarihi, başlangıç tarihinin gerisinde olamaz.", Color.White, 2000);
                gorev_bitis_tarih.Value = gorev_baslangic_tarih.Value.AddDays(1);
            }
       }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (gorev_bitis_tarih.Value <= gorev_baslangic_tarih.Value)
            {
                F.DURUM_LABEL("Hata: Proje bitiş tarihi, başlangıç tarihinin gerisinde olamaz.", Color.White, 2000);
                gorev_bitis_tarih.Value = gorev_baslangic_tarih.Value.AddDays(1);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (task_images.Count < 15)
            {
                string fileName = F.OPENFILEIMAGE();
                if(fileName != null)
                {
                    task_images.Add(Image.FromFile(fileName));
                }
            }
            else F.DURUM_LABEL("En fazla 15 adet görev resmi ekleyebilirsiniz.", Color.White, 4000);
            resmi_goster(task_images.Count - 1);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT WORKERS.worker_id,worker_name,worker_image FROM WORKERS INNER JOIN PROJECT_WORKERS ON PROJECT_WORKERS.project_id = @PROJECT_ID where worker_onay = 1 AND WORKERS.worker_id = PROJECT_WORKERS.worker_id");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            using (var form = new calisan_ekle(komut))
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DataTable val = form.worker_table;
                    System.Collections.Generic.List<int> ids = new System.Collections.Generic.List<int>();
                    for (int i = 0; i <gorev_calisanlari_datagrid.Rows.Count;i++)
                    {
                        ids.Add(Convert.ToInt32(gorev_calisanlari_datagrid.Rows[i].Cells[0].Value));
                    }
                    for(int i =0;i<val.Rows.Count;i++)
                    {
                        if(ids.IndexOf(Convert.ToInt32(val.Rows[i]["worker_id"])) == -1)
                        {
                            gorev_calisanlari_datagrid.Rows.Add(Convert.ToInt32(val.Rows[i]["worker_id"]), (Image)val.Rows[i]["worker_image"], val.Rows[i]["worker_name"].ToString());
                        }
                    }
                }
            }
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            F.OpenImage(gorev_resmi.Image);
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(gorev_calisanlari_datagrid.Rows.Count > 0 && gorev_calisanlari_datagrid.SelectedRows != null)
            {
                if(gorev_calisanlari_datagrid.SelectedRows.Count == 1)
                {
                    gorev_calisanlari_datagrid.Rows.Remove(gorev_calisanlari_datagrid.SelectedRows[0]);
                }
            }
        }
        System.Collections.Generic.List<Image> task_images = new System.Collections.Generic.List<Image>();
        int current_task_image_index=0;
        private void geri_Click(object sender, EventArgs e)
        {
            if(task_images.Count > 0)
            {
                if(current_task_image_index - 1 >= 0)
                {
                    current_task_image_index--;

                }else F.DURUM_LABEL("Zaten ilk resimdesiniz.", Color.White, 3000);
            }
            resmi_goster(current_task_image_index);
        }

        private void ileri_Click(object sender, EventArgs e)
        {
            if (task_images.Count > 0)
            {
                if (current_task_image_index + 1 < task_images.Count)
                {
                    current_task_image_index++;
                }
                else F.DURUM_LABEL("Zaten son resimdesiniz.", Color.White, 3000);
            }
            resmi_goster(current_task_image_index);
        }

        private void sil(object sender, EventArgs e)
        {
            if(task_images.Count>0)
            {
                task_images.Remove(task_images[current_task_image_index]);
            }
            resmi_goster(0);
        }
        void resmi_goster(int index)
        {
            if(task_images.Count>0)
            {
                gorev_resmi.Image = task_images[index];
                current_task_image_index = index;
                label4.Text = "Görev Resmi (" + (index + 1) + "/" + task_images.Count + ")";
            }
            else
            {
               
                var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
                gorev_resmi.Image = Image.FromFile(filePath + @"\default_task_image.jpg");
                label4.Text = "Görev Resmi";
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
                string P = dataGridView.SelectedRows[0].Cells[2].Value.ToString() + "'nin pozisyonları \n";
                for (int i = 0; i < positions.Length; i++)
                {
                    P += positions[i] + "\n";
                }
                System.Drawing.Rectangle cellRect = dataGridView.GetCellDisplayRectangle(2, e.RowIndex, false);
                toolTip1.Show(P,
                              this,
                              dataGridView.Location.X + cellRect.X,
                              dataGridView.Location.Y + cellRect.Y + cellRect.Size.Height);    // Duration: 5 seconds.
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new gorev_liste(), true);
        }
    }
}

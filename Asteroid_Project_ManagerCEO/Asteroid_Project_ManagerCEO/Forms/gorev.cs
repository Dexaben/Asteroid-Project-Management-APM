using Microsoft.VisualBasic;
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

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class gorev : Form
    {
        public int gorev_id { get; set; }
        public gorev()
        {
            InitializeComponent();
        }
        FUNCTIONS F = new FUNCTIONS();
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        List<Image> task_images = new List<Image>();
        private void gorev_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT  T.task_name,T.task_details,T.task_start_date,T.task_finish_date,T.task_status,T.task_urgency,(SELECT PROJECTS.project_name FROM PROJECTS WHERE(PROJECTS.project_id = T.project_id)) AS project_name,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE(DEPARTMENTS.department_id = (SELECT PROJECTS.department_id FROM PROJECTS WHERE(PROJECTS.project_id = T.project_id)))) AS department_name FROM TASKS T WHERE  T.task_id = @task_id");
            komut.Parameters.AddWithValue("@task_id", gorev_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Görev bilgileri alınırken hata oluştu.", Color.Red, 2000);
            if (table.Rows.Count != 0)
            {
                if (table.Rows[0]["task_urgency"].ToString() == "Acil") groupBox1.Visible = true;
                else groupBox1.Visible = false;
                gorev_projesi.Text = table.Rows[0]["project_name"].ToString();
                gorev_departmani.Text = table.Rows[0]["department_name"].ToString();
                gorev_textbox.Text = table.Rows[0]["task_name"].ToString();
                gorev_detayi.Text = table.Rows[0]["task_details"].ToString();
                gorev_baslangic_tarihi.Text = F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["task_start_date"].ToString());
                textBox6.Text = F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[0]["task_finish_date"].ToString());
                gorev_durum.Text = table.Rows[0]["task_status"].ToString();
                if (table.Rows[0]["task_status"].ToString() == "Görev Tamamlandı")
                {

                    gorev_durum.BackColor = Color.Green;
                    gorev_durum.ForeColor = Color.White;
                }
               
            }
            komut = new SqlCommand("SELECT task_image FROM TASK_IMAGES WHERE task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", gorev_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:", Color.Red, 3000);
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
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
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString(), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]));
                }
            }
            gorevdeki_calisanlar_datagrid.DataSource = dataTable;
            ((DataGridViewImageColumn)gorevdeki_calisanlar_datagrid.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        int current_task_image_index = 0;

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
    
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (task_images.Count > 0)
                F.OpenImage(gorev_resmi.Image);
        }
        void resim_goster(int index)
        {
            if (task_images.Count > 0)
            {
                resim_Geri.Visible = true;
                resim_Ileri.Visible = true;
                gorev_resmi.Image = task_images[index];
                current_task_image_index = index;
                label2.Text = "Görev Resmi (" + (index + 1) + "/" + task_images.Count + ")";
            }
            else
            {
                resim_Geri.Visible = false;
                resim_Ileri.Visible = false;
                var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
                gorev_resmi.Image = Image.FromFile(filePath + @"\default_task_image.png");
                label2.Text = "Görev Resmi";
            }
        }
    }
}

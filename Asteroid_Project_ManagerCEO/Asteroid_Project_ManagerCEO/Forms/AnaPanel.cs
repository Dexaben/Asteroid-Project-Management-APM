using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class AnaPanel : Form
    {
        SqlCommand komut = new SqlCommand();
        
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public AnaPanel()
        {
            InitializeComponent();
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void AnaPanel_Load(object sender, EventArgs e)
        {
            PROJELER_LISTELE();
            CALISAN_LISTELE();
            GOREVLER_LISTELE();
            proje_id_groupbox.Visible = false;
            calisan_id_groupbox.Visible = false;
            gorev_id_groupbox.Visible = false;
        }
        void PROJELER_LISTELE()
        {
            //DATAGRIDVIEW 1
            komut = new SqlCommand("SELECT project_id,project_name,project_image,project_start_date,project_finish_date FROM PROJECTS");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje bilgileri veritabanından alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Proje ID", typeof(int));
            dataTable.Columns.Add("Proje İsmi", typeof(string));
            dataTable.Columns.Add("Proje Resmi", typeof(Image));
            dataTable.Columns.Add("Proje Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Proje Bitiş Tarihi", typeof(string));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["project_image"]), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_start_date"].ToString()), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_finish_date"].ToString()));
            }

            proje_datargid.DataSource = dataTable;
            proje_datargid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < proje_datargid.Columns.Count; i++)
                if (proje_datargid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)proje_datargid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        void CALISAN_LISTELE()
        {
            //DATAGRIDVIEW 2
            komut = new SqlCommand("SELECT worker_id,worker_name,worker_image,(SELECT COUNT(worker_id) FROM TASK_WORKERS INNER JOIN TASKS ON TASKS.task_id = TASK_WORKERS.task_id WHERE(WORKERS.worker_id = TASK_WORKERS.worker_id AND TASKS.task_status <> 'Görev Tamamlandı'))as gorevleri FROM WORKERS WHERE worker_onay = 1");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan bilgileri alınırken hata oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable.Columns.Add("Çalışanın Aktif Görevleri", typeof(int));
            for (int i = 0; i < table.Rows.Count; i++)
            {
               dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString(), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), Convert.ToInt32(table.Rows[i]["gorevleri"]));
            }
            calisan_datagrid.DataSource = dataTable;
            calisan_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < calisan_datagrid.Columns.Count; i++)
                if (calisan_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)calisan_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        void GOREVLER_LISTELE()
        {
            //DATAGRIDVIEW 3
            komut = new SqlCommand("SELECT task_id,task_name,(SELECT COUNT(TASK_WORKERS.task_id) FROM TASK_WORKERS WHERE TASKS.task_id = TASK_WORKERS.task_id)as calisan_sayisi,(SELECT PROJECTS.project_name FROM PROJECTS WHERE project_id = TASKS.project_id)AS gorev_projesi FROM TASKS WHERE task_status <> 'Görev Tamamlandı'");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri alınırken sorun oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Görev ID", typeof(int));
            dataTable.Columns.Add("Görev", typeof(string));
            dataTable.Columns.Add("Görevdeki Çalışan Sayısı", typeof(int));
            dataTable.Columns.Add("Projesi", typeof(string));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["task_name"].ToString(), Convert.ToInt32(table.Rows[i]["calisan_sayisi"]), table.Rows[i]["gorev_projesi"].ToString());
            }
            gorev_datagrid.DataSource = dataTable;
            gorev_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;        
        }
        int project_id;
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (!proje_id_groupbox.Visible) proje_id_groupbox.Visible = true;
            proje_id_groupbox.Text = "Proje (ID-"+proje_datargid.SelectedRows[0].Cells[0].Value.ToString()+")";
            project_id = Convert.ToInt32(proje_datargid.SelectedRows[0].Cells[0].Value);
            proje_ismi.Text = proje_datargid.SelectedRows[0].Cells[1].Value.ToString();
            proje_resmi.Image = (Image)proje_datargid.SelectedRows[0].Cells[2].Value;
            proje_resmi.SizeMode = PictureBoxSizeMode.Zoom;
            proje_baslangic_tarihi.Text = proje_datargid.SelectedRows[0].Cells[3].Value.ToString();
            proje_bitis_tarihi.Text = proje_datargid.SelectedRows[0].Cells[4].Value.ToString();
            komut = new SqlCommand("SELECT(SELECT COUNT(PROJECT_WORKERS.worker_id) FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.project_id = PROJECTS.project_id) as calisan_say,(SELECT COUNT(TASKS.task_id) FROM TASKS WHERE TASKS.project_id = PROJECTS.project_id AND TASKS.task_status = 'Görev Tamamlandı')AS tamamlanmis_gorev,(SELECT COUNT(TASKS.task_id) FROM TASKS WHERE TASKS.project_id = PROJECTS.project_id AND TASKS.task_status <> 'Görev Tamamlandı')AS tamamlanmis_gorev FROM PROJECTS WHERE project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", proje_datargid.SelectedRows[0].Cells[0].Value);
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Bilgiler veritabanından çekilirken sorun oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table.Rows.Count !=0)
            {
                projede_calisan_say.Text =  table.Rows[0][0].ToString();
                tamamlanmis_gorev_say.Text = table.Rows[0][1].ToString();
                tamamlanmamis_gorev_say.Text = table.Rows[0][2].ToString();
                proje_tamamlanma_yuzde.Text = "%"+((Math.Ceiling(100/(Convert.ToDouble(table.Rows[0][1]) + Convert.ToDouble(table.Rows[0][2]))* Convert.ToDouble(table.Rows[0][1])))).ToString();
                proje_tamamlanma_yuzde.BackColor = Scripts.Tools.ColorTools.RED_YELLOW_GREEN_100((100 / (Convert.ToDouble(table.Rows[0][1]) + Convert.ToDouble(table.Rows[0][2])) * Convert.ToDouble(table.Rows[0][1])));
            }
            else
            {
                projede_calisan_say.Text = "";
                tamamlanmis_gorev_say.Text = "";
                proje_tamamlanma_yuzde.Text = "";
                tamamlanmamis_gorev_say.Text = "";
                proje_tamamlanma_yuzde.BackColor = Color.Gray;
            }
        }
        int worker_id;
        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if (!calisan_id_groupbox.Visible) calisan_id_groupbox.Visible = true;
            calisan_id_groupbox.Text = "Çalışan (ID-"+calisan_datagrid.SelectedRows[0].Cells[0].Value.ToString()+")";
            worker_id = Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value);
            calisan_ismi.Text = calisan_datagrid.SelectedRows[0].Cells[1].Value.ToString();
            calisan_resmi.Image = (Image)calisan_datagrid.SelectedRows[0].Cells[2].Value;
            calisan_resmi.SizeMode = PictureBoxSizeMode.Zoom;
            calisan_pozisyonlari.Items.Clear();
            calisan_pozisyonlari.Items.AddRange(Scripts.SQL.SqlQueries.WORKER_ROLE_CALL_BY_ID((int)calisan_datagrid.SelectedRows[0].Cells[0].Value));
          
            komut = new SqlCommand("SELECT PROJECTS.project_id, PROJECTS.project_name, (SELECT W.worker_name FROM WORKERS W WHERE W.worker_id=PROJECTS.project_manager)AS project_manager, (SELECT COUNT(TASK_WORKERS.task_id) FROM TASK_WORKERS INNER JOIN TASKS ON TASKS.task_id = TASK_WORKERS.task_id WHERE(TASK_WORKERS.worker_id = @WORKER_ID AND TASKS.task_status <> 'Görev Tamamlandı')) as aktif_gorev_say FROM PROJECTS INNER JOIN PROJECT_WORKERS ON PROJECT_WORKERS.worker_id = @WORKER_ID WHERE PROJECTS.project_id = PROJECT_WORKERS.project_id ");
            komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value));
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje bilgileri çekilirken sorun oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Proje ID", typeof(int));
            dataTable.Columns.Add("Proje İsmi", typeof(string));
            dataTable.Columns.Add("Proje Yöneticisi", typeof(string));
            dataTable.Columns.Add("Üzerindeki Aktif Görev Sayısı", typeof(int));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["project_manager"].ToString(), Convert.ToInt32(table.Rows[i]["aktif_gorev_say"]));
            }
            calisan_gorev_aldigi_datagrid.DataSource = dataTable;
            calisan_gorev_aldigi_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        int gorev_id = 0;
        private void dataGridView3_DoubleClick(object sender, EventArgs e)
        {
            if (!gorev_id_groupbox.Visible) gorev_id_groupbox.Visible = true;
            gorev_id = Convert.ToInt32(gorev_datagrid.SelectedRows[0].Cells[0].Value);
            gorev_id_groupbox.Text = "Görev (ID-" + gorev_datagrid.SelectedRows[0].Cells[0].Value.ToString() + ")";
            gorev.Text = gorev_datagrid.SelectedRows[0].Cells[1].Value.ToString();
            gorev_projesi.Text = gorev_datagrid.SelectedRows[0].Cells[3].Value.ToString();
            komut = new SqlCommand("SELECT TASKS.task_status,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE DEPARTMENTS.department_id = (SELECT PROJECTS.department_id FROM PROJECTS WHERE PROJECTS.project_id = TASKS.project_id))AS department_name FROM TASKS WHERE TASKS.task_id=@TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", Convert.ToInt32(gorev_datagrid.SelectedRows[0].Cells[0].Value));
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri çekilirken sorun oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table.Rows.Count !=0)
            {
                gorev_resmi.SizeMode = PictureBoxSizeMode.Zoom;
                gorev_durumu.Text = table.Rows[0][0].ToString();
                gorev_departmani.Text = table.Rows[0][1].ToString();
            }
            komut = new SqlCommand("SELECT task_image FROM TASK_IMAGES WHERE task_id=@TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", Convert.ToInt32(gorev_datagrid.SelectedRows[0].Cells[0].Value));
          SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri çekilirken sorun oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            task_images.Clear();
            current_task_image_index = 0;
            if (table != null)
            {
                for(int i = 0;i<table.Rows.Count;i++)
                {
                    task_images.Add(Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["task_image"]));
                }
            }
            
            resmi_goster(0);
            komut = new SqlCommand("SELECT (SELECT WORKERS.worker_name FROM WORKERS WHERE WORKERS.worker_id = TASK_WORKERS.worker_id)AS worker_name FROM TASK_WORKERS WHERE TASK_WORKERS.task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", Convert.ToInt32(gorev_datagrid.SelectedRows[0].Cells[0].Value));
         SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri çekilirken sorun oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            gorev_calisanlari.Items.Clear();
            if(table.Rows.Count !=0)
            {
                for(int i =0;i< table.Rows.Count;i++)
                {
                    gorev_calisanlari.Items.Add(table.Rows[i][0].ToString());
                }
            }
        }
        private void pictureBox6_DoubleClick(object sender, EventArgs e)
        {
            if(task_images.Count > 0)
            Scripts.Tools.ImageTools.OpenImage(gorev_resmi.Image);
        }
        List<Image> task_images = new List<Image>();
        int current_task_image_index = 0;
        private void geri_Click(object sender, EventArgs e)
        {
            if (task_images.Count > 0)
            {
                if (current_task_image_index - 1 >= 0)
                {
                    current_task_image_index--;

                }
                else Scripts.Form.Status.STATUS_LABEL("Zaten ilk resimdesiniz.", Color.White, 3000);
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
                else Scripts.Form.Status.STATUS_LABEL("Zaten son resimdesiniz.", Color.White, 3000);
            }
            resmi_goster(current_task_image_index);
        }
        void resmi_goster(int index)
        {
            if (task_images.Count > 0)
            {
                gorev_resmi.Image = task_images[index];
                current_task_image_index = index;
                geri.Visible = true;
                ileri.Visible = true ;
            }
            else
            {

                var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
                gorev_resmi.Image = Image.FromFile(filePath + @"\default_task_image.jpg");
                geri.Visible = false;
                ileri.Visible = false;
            }


        }

        private void button3_Click(object sender, EventArgs e) //gorev ayrıntı
        {
            gorev grv = new gorev();
            grv.gorev_id = this.gorev_id;
            grv.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) // proje ayrıntı
        {
            proje_detay pd = new proje_detay();
            pd.project_id = this.project_id;
            pd.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e) //çalışan ayrıntı
        {
            calisan_detay clsn_detay = new calisan_detay();
            clsn_detay.worker_id = this.worker_id;
            clsn_detay.ShowDialog();
        }
    }
}

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

namespace Asteroid_Project_ManagerAPP.Forms
{
    public partial class gorev_bilgileri_guncelle : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        FUNCTIONS F = new FUNCTIONS();
        public int task_id {get;set;}
        public gorev_bilgileri_guncelle()
        {
            InitializeComponent();
            this.AcceptButton = guncelle;
        }

        private void gorev_bilgileri_guncelle_Load(object sender, EventArgs e)
        {
           
            komut = new SqlCommand("SELECT T.task_name,T.task_details,T.task_urgency,T.task_start_date,T.task_finish_date,(SELECT project_start_date FROM PROJECTS WHERE PROJECTS.project_id = T.project_id)AS project_start_date,(SELECT project_finish_date FROM PROJECTS WHERE PROJECTS.project_id = T.project_id)AS project_finish_date FROM TASKS T WHERE T.task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", task_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Görev bilgileri alınırken hata oluştu.", Color.Red,4000);
            if(table != null)
            {
                gorev.Text = table.Rows[0]["task_name"].ToString();
                gorev_detay.Text = table.Rows[0]["task_details"].ToString();
                if (table.Rows[0]["task_urgency"].ToString() == "Acil")
                    acil_checkBox.Checked = true;
                else acil_checkBox.Checked = false;
                baslangic_tarih.MinDate = (DateTime)table.Rows[0]["project_start_date"];
                bitis_tarih.MinDate = baslangic_tarih.MinDate.AddDays(1);
                bitis_tarih.MaxDate = (DateTime)table.Rows[0]["project_finish_date"];
                baslangic_tarih.MaxDate = bitis_tarih.MaxDate.AddDays(-1);
                baslangic_tarih.Value = (DateTime)table.Rows[0]["task_start_date"];
                bitis_tarih.Value = (DateTime)table.Rows[0]["task_finish_date"];

                komut = new SqlCommand("SELECT worker_id,worker_image,worker_name FROM WORKERS WHERE worker_onay = 1 AND worker_id IN (SELECT worker_id FROM TASK_WORKERS WHERE task_id = @TASK_ID)");
                komut.Parameters.AddWithValue("@TASK_ID", task_id);
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Görev çalışanları alınamadı.", Color.Red, 4000);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Çalışan ID", typeof(int));
                dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
                dataTable.Columns.Add("Çalışan İsmi", typeof(string));
              
                if (table != null)
                {
                    for(int i =0;i<table.Rows.Count;i++)
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]),table.Rows[i]["worker_name"].ToString());
                }
                calisan_datagridview.DataSource = dataTable;
                for (int i = 0; i < calisan_datagridview.Columns.Count; i++)
                    if (calisan_datagridview.Columns[i] is DataGridViewImageColumn)
                    {
                        ((DataGridViewImageColumn)calisan_datagridview.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                        break;
                    }

                komut = new SqlCommand("SELECT task_image FROM TASK_IMAGES WHERE task_id = @TASK_ID");
                komut.Parameters.AddWithValue("@TASK_ID", task_id);
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Görev çalışanları alınamadı.", Color.Red, 4000);
                if(table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                        task_images.Add(F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["task_image"]));
                }
                resmi_goster(0);
            }
        }
        List<Image> task_images = new List<Image>();
        int current_task_image_index = 0;
        private void button6_Click(object sender, EventArgs e) //resim sil
        {
            if (task_images.Count > 0) task_images.Remove(task_images[current_task_image_index]);
            resmi_goster(0);
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            if (task_images.Count < 15)
            {
                string fileName = F.OPENFILEIMAGE();
                if (fileName != null)
                {
                    task_images.Add(Image.FromFile(fileName));
                }
            }
            else F.DURUM_LABEL("En fazla 15 adet görev resmi ekleyebilirsiniz.", Color.White, 4000);
            resmi_goster(task_images.Count - 1);
        }

        private void button4_Click(object sender, EventArgs e)
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

        private void button5_Click(object sender, EventArgs e)
        {
            if (task_images.Count > 0)
            {
                if (current_task_image_index - 1 >= 0)
                {
                    current_task_image_index--;

                }
                else F.DURUM_LABEL("Zaten ilk resimdesiniz.", Color.White, 3000);
            }
            resmi_goster(current_task_image_index);
        }
        void resmi_goster(int index)
        {
            if (task_images.Count > 0)
            {
                gorev_Resim.Image = task_images[index];
                current_task_image_index = index;
                label4.Text = "Görev Resmi (" + (index + 1) + "/" + task_images.Count + ")";
            }
            else
            {
                var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
                gorev_Resim.Image = Image.FromFile(filePath + @"\default_task_image.jpg");
                label4.Text = "Görev Resmi";
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            F.OpenImage(gorev_Resim.Image);
        }

        private void button2_Click(object sender, EventArgs e) //güncelle
        {
            if (gorev.Text.Length > 0 && gorev_detay.Text.Length > 0)
            {
                kaydet();
            }
            else label8.Text = "Görev ismi ve Görev detayı boş bırakılamaz";
        }
        void kaydet()
        {
            komut = new SqlCommand("UPDATE TASKS SET task_name=@TASK_NAME,task_details=@TASK_DETAILS,task_urgency=@TASK_URGENCY,task_start_date=@TASK_START,task_finish_date=@TASK_FINISH WHERE task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", task_id);
            komut.Parameters.AddWithValue("@TASK_NAME", gorev.Text);
            komut.Parameters.AddWithValue("@TASK_DETAILS", gorev_detay.Text);
            System.Data.SqlTypes.SqlDateTime time_Start = baslangic_tarih.Value;
            System.Data.SqlTypes.SqlDateTime time_Finish = bitis_tarih.Value;
            komut.Parameters.AddWithValue("@TASK_START", time_Start);
            komut.Parameters.AddWithValue("@TASK_FINISH", time_Finish);
            if (acil_checkBox.Checked) komut.Parameters.AddWithValue("@TASK_URGENCY", "Acil");
            else komut.Parameters.AddWithValue("@TASK_URGENCY", "");
            if (F.SQL_EXECUTENONQUERY(komut, "Hata:Görev bilgileri güncellenemedi.", Color.Red, 2000))
            {
                string workers = "";
                for (int i = 0; i < calisan_datagridview.Rows.Count; i++)
                {
                    workers += $" {F.WORKER_NAME_BY_WORKER_ID(Convert.ToInt32(calisan_datagridview.Rows[i].Cells[0].Value))} ,";
                }
                komut = new SqlCommand("DELETE FROM TASK_IMAGES WHERE task_id =@TASK_ID");
                komut.Parameters.AddWithValue("@TASK_ID", task_id);
                F.SQL_EXECUTENONQUERY(komut, "Hata:Görev resimleri güncellenemedi.", Color.Red, 4000);
                for (int i= 0; i<task_images.Count ; i++)
                {
                    komut = new SqlCommand("INSERT INTO TASK_IMAGES(task_id,task_image) VALUES(@TASK_ID,@TASK_IMAGE)");
                    komut.Parameters.AddWithValue("@TASK_ID", task_id);
                    komut.Parameters.AddWithValue("@TASK_IMAGE", F.CONVERT_IMAGE_TO_BYTE_ARRAY(task_images[i],System.Drawing.Imaging.ImageFormat.Jpeg));
                    F.SQL_EXECUTENONQUERY(komut, "Hata:Görev resim güncellenemedi.", Color.Red, 4000);
                }
           
                F.LOG_ENTER(a.worker_id, $"{F.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {F.PROJECT_NAME_BY_PROJECT_ID(a.project_id)} projesindeki {gorev.Text} görevini güncelledi.Görev ID:{task_id},Görev Başlangıç Tarihi:{time_Start},Görev Bitiş Tarihi:{time_Finish},Görev Çalışanları:{workers}", F.GET_SERVER_DATE());
                F.DURUM_LABEL(" | Görevi Güncellendi. | " + task_id, Color.Yellow, 9000);
                ((gorev)Application.OpenForms["gorev"]).gorev_Load((gorev)Application.OpenForms["gorev"], null);
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e) //çalışan ekle
        {
            
                komut = new SqlCommand("SELECT worker_id,worker_image,worker_name FROM WORKERS WHERE worker_onay = 1 AND worker_id IN (SELECT worker_id FROM PROJECT_WORKERS WHERE project_id = @PROJECT_ID)");
                komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
                using (var form = new calisan_ekle(komut))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        DataTable val = form.worker_table;
                        List<int> ids = new List<int>();
                        for (int i = 0; i < calisan_datagridview.Rows.Count; i++)
                        {
                            ids.Add(Convert.ToInt32(calisan_datagridview.Rows[i].Cells[0].Value));
                        }
                        for (int i = 0; i < val.Rows.Count; i++)
                        {
                            if (ids.IndexOf(Convert.ToInt32(val.Rows[i]["worker_id"])) == -1)
                            {
                                F.LOG_ENTER(a.worker_id, $"{F.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan ({task_id}){gorev.Text} görevinde ({val.Rows[i]["worker_id"].ToString()}){val.Rows[i]["worker_name"].ToString()} adlı çalışanı görevlendirdi.", F.GET_SERVER_DATE());
                                komut = new SqlCommand("INSERT INTO TASK_WORKERS(task_id,worker_id) VALUES(@TASK_ID,@WORKER_ID)");
                                komut.Parameters.AddWithValue("@TASK_ID", task_id);
                                komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(val.Rows[i]["worker_id"]));
                                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan göreve eklenemedi.", System.Drawing.Color.Red, 4000))
                                {
                                    CalisanTablosu();
                                }
                            }
                        }
                    }
                }
         
        }
        void CalisanTablosu()
        {
            
            komut = new SqlCommand("SELECT W.worker_id,W.worker_image,W.worker_name FROM TASK_WORKERS TW INNER JOIN WORKERS W ON TW.worker_id=W.worker_id  WHERE TW.task_id = @TASK_ID");
            komut.Parameters.AddWithValue("@TASK_ID", task_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Aktif çalışanlar alınırken sorun oluştu.", System.Drawing.Color.Red, 4000);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), table.Rows[i]["worker_name"].ToString());
                }
            }
            calisan_datagridview.DataSource = dataTable;
            for (int i = 0; i < calisan_datagridview.Columns.Count; i++)
                if (calisan_datagridview.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)calisan_datagridview.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (calisan_datagridview.Rows.Count > 0 && calisan_datagridview.SelectedRows.Count > 0)
            {
                komut = new SqlCommand("DELETE FROM TASK_WORKERS WHERE task_id=@TASK_ID AND worker_id=@WORKER_ID");
                komut.Parameters.AddWithValue("@TASK_ID", task_id);
                komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(calisan_datagridview.SelectedRows[0].Cells[0].Value));
                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan görevden silinmedi.", System.Drawing.Color.Red, 4000))
                {
                    if (calisan_datagridview.Rows.Count > 0 && calisan_datagridview.SelectedRows.Count == 1)
                    {
                        F.LOG_ENTER(a.worker_id, $"{F.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan { F.PROJECT_NAME_BY_PROJECT_ID(a.project_id) } projesindeki ({task_id}){gorev.Text} görevinden ({calisan_datagridview.Rows[calisan_datagridview.SelectedRows[0].Index].Cells[0].Value.ToString()}){calisan_datagridview.Rows[calisan_datagridview.SelectedRows[0].Index].Cells[2].Value.ToString()} adlı çalışanı çıkarttı.", F.GET_SERVER_DATE());
                        CalisanTablosu();
                    }
                }
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (bitis_tarih.Value <= baslangic_tarih.Value)
            {
                F.DURUM_LABEL("Hata: Proje bitiş tarihi, başlangıç tarihinin gerisinde olamaz.", Color.White, 2000);
                bitis_tarih.Value = baslangic_tarih.Value.AddDays(1);
            }
        }
    }
}

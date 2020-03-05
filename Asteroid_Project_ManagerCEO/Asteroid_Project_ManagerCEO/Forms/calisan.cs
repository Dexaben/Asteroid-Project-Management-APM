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
    public partial class calisan : Form
    {
        public calisan()
        {
            InitializeComponent();
            this.AcceptButton = onay_button;
        }
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        int i = 0;
        Image imag_onayli;
        Image imag_onaysiz;
        private void calisan_Load(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources");
            filePath = System.IO.Path.Combine(filePath, "iconlar");
            imag_onaysiz = Image.FromFile(filePath + @"\onaysiz.png");
            imag_onayli = Image.FromFile(filePath + @"\onayli.png");
            komut = new SqlCommand("SELECT worker_id,worker_image,worker_name,worker_onay FROM WORKERS");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan verileri alınamadı.", Color.Red, 4000);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable.Columns.Add("Çalışan Onayı", typeof(bool));
            if (table !=null)
            {
                for (int z= 0; z < table.Rows.Count; z++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[z]["worker_id"]), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[z]["worker_image"]), table.Rows[z]["worker_name"].ToString(), Convert.ToBoolean(table.Rows[z]["worker_onay"]));
                }
            }
            calisan_datagrid.DataSource = dataTable;
            for (int z = 0; z < calisan_datagrid.Columns.Count; z++)
                if (calisan_datagrid.Columns[z] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)calisan_datagrid.Columns[z]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
            calisan_datagrid.ClearSelection();
            if (calisan_datagrid.Rows.Count > 0 && i < calisan_datagrid.Rows.Count)
                calisan_datagrid.Rows[i].Selected = true;
            else try { calisan_datagrid.Rows[0].Selected = true; } catch { }
            row_selected(this, null);
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            if (calisan_datagrid.Rows.Count > 0)
            {
                i = calisan_datagrid.SelectedRows[0].Index;
                    komut = new SqlCommand("UPDATE WORKERS SET worker_onay=@ONAY WHERE worker_id=@SECIM");
                    if (Convert.ToInt32(calisan_datagrid.Rows[i].Cells[3].Value) == 0)
                    {
                        komut.Parameters.AddWithValue("@ONAY", 1);
                            onay_durum.Text = "Onaylı";
                            onay_button.ForeColor = Color.Red;
                    onay_button.Text = "Onay Kaldır";
                            onay_durum.ForeColor = Color.Green;
                    onay_resim.Image = imag_onayli;
                    F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından {calisan_datagrid.Rows[i].Cells[2].Value} çalışanının onay durumu güncellendi.(Onaysız => ONAYLI)",F.GET_SERVER_DATE());
                     }
                else
                    {
                        komut.Parameters.AddWithValue("@ONAY", 0);
                    onay_durum.Text = "Onaysız";
                    onay_button.ForeColor = Color.ForestGreen;
                    onay_button.Text = "Onayla";
                    onay_durum.ForeColor = Color.Red;
                    onay_resim.Image = imag_onaysiz;
                    F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından {calisan_datagrid.Rows[i].Cells[2].Value} çalışanının onay durumu güncellendi.(Onaylı => ONAYSIZ)", F.GET_SERVER_DATE());
                }
                int a = Convert.ToInt32(calisan_datagrid.Rows[i].Cells[0].Value);
                    komut.Parameters.AddWithValue("@SECIM", a);
                    if(F.SQL_EXECUTENONQUERY(komut,"Hata:Çalışan bilgisi güncellenemedi",Color.Red,3000))
                    {
                    
                    calisan_Load(this, null);

                    }
            }
            else F.DURUM_LABEL("Hata:Çalışan verisi bulunamadı.", Color.Purple, 2000);
        }
        private void button2_Click(object sender, EventArgs e)
        {
           F.FORM_AC(new calisan_ekle(), true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (calisan_datagrid.Rows.Count > 0)
                F.FORM_AC(new calisan_guncelle(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)), true);
            else F.DURUM_LABEL("Hata:Çalışan verisi bulunamadı.", Color.Purple, 2000);
        }
        int WORKER_MANAGED_PROJECT_COUNT(int worker_id)
        {
            komut = new SqlCommand("SELECT COUNT(project_id)AS managed_projects FROM PROJECTS WHERE project_manager = @WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgisi alınamadı.", Color.Red, 3000);
            if (table != null)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }
        int PROJECT_WORKERS_COUNT(int worker_id)
        {
            komut = new SqlCommand("SELECT COUNT(worker_id)AS worker_id FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.project_manager=@WORKER_ID)");
            komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgisi alınamadı.", Color.Red, 3000);
            if (table != null)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }
        int PROJECT_TASKS_COUNT(int worker_id)
        {
            komut = new SqlCommand("SELECT COUNT(task_id)AS task_id FROM TASKS WHERE TASKS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.project_manager=@WORKER_ID)");
            komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgisi alınamadı.", Color.Red, 3000);
            if (table != null)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (calisan_datagrid.Rows.Count > 0)
            {
                string message = "Bu çalışanı gerçekten SİLMEK istiyormusunuz?";
                if(F.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)).Length >0)
                {
                    message += " Bu çalışanın " +F.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)).Length + " adet pozisyon kaydı bulunuyor.";
                }
                if(WORKER_MANAGED_PROJECT_COUNT(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value))>0)
                {
                    message += " Bu çalışanın " + WORKER_MANAGED_PROJECT_COUNT(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)).ToString()+ " adet projeye yöneticilik yapmakta.";
                    if (PROJECT_WORKERS_COUNT(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)) > 0)
                    {
                        message += " Bu projelerde " + PROJECT_WORKERS_COUNT(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)).ToString() + " adet çalışan görev alıyor.";
                    }
                    if(PROJECT_TASKS_COUNT(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value))>0)
                    {
                        message += " Bu projelerde " + PROJECT_TASKS_COUNT(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)).ToString() + " adet görev bulunuyor.";
                    }
                }
                DialogResult dialog = MessageBox.Show(message, "ÇIKIŞ", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value);
                    komut = new SqlCommand("DELETE FROM WORKER_POSITIONS WHERE worker_id=@id");
                    komut.Parameters.AddWithValue("@id", id);
                    if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan verisi silinemedi.", Color.Red, 3000))
                    {
                        komut = new SqlCommand("DELETE FROM PROJECT_WORKERS WHERE worker_id=@id");
                        komut.Parameters.AddWithValue("@id", id);
                        if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan verisi silinemedi.", Color.Red, 4000))
                        {
                            komut = new SqlCommand("DELETE FROM TASK_WORKERS WHERE worker_id=@id");
                            komut.Parameters.AddWithValue("@id", id);
                            if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan verisi silinemedi.", Color.Red, 4000))
                            {
                                komut = new SqlCommand("DELETE FROM WORKERS WHERE worker_id=@id");
                                komut.Parameters.AddWithValue("@id", id);
                                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan verisi silinemedi.", Color.Red, 4000))
                                {
                                    komut = new SqlCommand("SELECT project_id FROM PROJECTS WHERE project_manager=@id");
                                    komut.Parameters.AddWithValue("@id", id);
                                    table = F.SQL_SELECT_DATATABLE(komut, "Hata:Görev verileri silinemedi.", Color.Red, 4000);
                                    if (table != null)
                                    {
                                        for (int z = 0;z< table.Rows.Count; z++)
                                        {
                                            komut = new SqlCommand("DELETE FROM TASK_WORKERS WHERE TASK_WORKERS.task_id IN (SELECT TASKS.task_id FROM TASKS WHERE TASKS.project_id = @PROJECT_ID)");
                                            komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(table.Rows[z]["project_id"]));
                                            if (F.SQL_EXECUTENONQUERY(komut, "Hata:Görev çalışanları silinemedi.", Color.Red, 4000))
                                            {
                                                komut = new SqlCommand("DELETE FROM TASK_IMAGES WHERE TASK_IMAGES.task_id IN (SELECT TASKS.task_id FROM TASKS WHERE TASKS.project_id = @PROJECT_ID)");
                                                komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(table.Rows[z]["project_id"]));
                                                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Görev resimleri silinemedi.", Color.Red, 4000))
                                                {
                                                    komut = new SqlCommand("DELETE FROM TASKS WHERE project_id = @PROJECT_ID");
                                                    komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(table.Rows[z]["project_id"]));
                                                    if(F.SQL_EXECUTENONQUERY(komut, "Hata:Görevler silinemedi.", Color.Red, 4000))
                                                    {
                                                        komut = new SqlCommand("DELETE FROM PROJECTS WHERE project_id=@PROJECT_ID");
                                                        komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(table.Rows[z]["project_id"]));
                                                    }

                                                }
                                            }
                                        }
                                    }
                  
                                            F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından (ID-{id}) {calisan_datagrid.SelectedRows[0].Cells[2].Value} adlı çalışan SİLİNDİ.Bu işlem ile çalışanın pozisyon kayıtları,Proje Yöneticiliği yaptığı projeler, bu projelerin görevleri ve içerisindeki veriler SİLİNDİ.", F.GET_SERVER_DATE());
                                            calisan_Load(this, null);
           
                                    
                                }
                            }

                        }
                    }
                }
                else F.DURUM_LABEL("Hata:Çalışan verisi bulunamadı.", Color.Purple, 2000);
                if (calisan_datagrid.Rows.Count <= 0) groupBox1.Visible = false;
                else calisan_datagrid.Rows[0].Selected = true;
            }
        }
        private void row_selected(object sender, DataGridViewCellEventArgs e)
        {
            
            if (calisan_datagrid.Rows.Count > 0)
            {
                i = calisan_datagrid.SelectedRows[0].Index;
                komut = new SqlCommand("SELECT worker_id,worker_name,worker_image,worker_gender,worker_mail,worker_onay FROM WORKERS WHERE worker_id=@wrk_id");
                komut.Parameters.AddWithValue("@wrk_id", Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value));
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 4000);
                if (table != null)
                {
                    calisan_resmi.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["worker_image"]);
                    calisan_id.Text = "Çalışan Bilgileri (" + table.Rows[0]["worker_id"].ToString() + ")";
                    calisan_isim.Text = table.Rows[0]["worker_name"].ToString();
                    pozisyon_listview.Items.Clear();

                        pozisyon_listview.Items.AddRange(F.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value)));

                    calisan_email.Text = table.Rows[0]["worker_mail"].ToString();
                    calisan_cinsiyet.Text = table.Rows[0]["worker_gender"].ToString();
                    if ((table.Rows[0]["worker_onay"]).ToString() == "False")
                    {
                        onay_durum.Text = "Onaysız";
                        onay_durum.ForeColor = Color.Red;
                        onay_button.ForeColor = Color.ForestGreen;
                        onay_button.Text = "Onayla";
                        onay_resim.Image = imag_onaysiz;
                    }
                    else
                    {
                        onay_durum.Text = "Onaylı";
                        onay_durum.ForeColor = Color.Green;
                        onay_button.ForeColor = Color.Red;
                        onay_button.Text = "Onay Kaldır";
                        onay_resim.Image = imag_onayli;
                    }
                }
            }
            else
            {
                F.DURUM_LABEL("Hata:Çalışan verisi bulunamadı.", Color.Purple, 2000);
                groupBox1.Visible = false;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(calisan_datagrid.Rows.Count >0 && calisan_datagrid.SelectedRows.Count>0)
            {
                calisan_detay calisan_Detay = new calisan_detay();
                calisan_Detay.worker_id = Convert.ToInt32(calisan_datagrid.SelectedRows[0].Cells[0].Value);
                calisan_Detay.ShowDialog();
            }
          
        }
    }
}

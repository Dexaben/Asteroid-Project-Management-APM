using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP.Forms
{
    public partial class proje_guncelle : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        System.Data.DataTable table = new System.Data.DataTable();
        System.Data.DataTable dataTable = new System.Data.DataTable();
        public proje_guncelle()
        {
            InitializeComponent();
            this.AcceptButton = proje_bilgileri_guncelle;
        }
        Dictionary<string, int> departmentDict;
        Dictionary<string, int> managerDict;
        private void proje_guncelle_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT worker_id,worker_name FROM WORKERS");
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            managerDict = new Dictionary<string, int>();
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (Scripts.SQL.SqlQueries.WORKER_HAS_ROLE_BY_WORKER_ID_AND_WORKER_ROLE(Convert.ToInt32(table.Rows[i]["worker_id"]), "Proje Yöneticisi"))
                    {
                        managerDict.Add(table.Rows[i]["worker_name"].ToString(), Convert.ToInt32(table.Rows[i]["worker_id"]));
                    }
                    proje_yoneticileri.Items.Clear();
                    foreach (string Deger in managerDict.Keys)
                    {
                        proje_yoneticileri.Items.Add(Deger);
                    }
                    if (proje_yoneticileri.Items.Count > 0)
                    {
                        proje_yoneticileri.Text = proje_yoneticileri.Items[0].ToString();
                    }
                    else
                    {
                        Scripts.Form.Status.STATUS_LABEL("Proje ekleyebilmeniz için bir'Proje Yöneticisi'ne ihtiyacınız var", Color.PaleVioletRed, 4000);

                    }
                }
            }
                if (proje_yoneticileri.Items.Count > 0)
            {
                proje_yoneticileri.Text = proje_yoneticileri.Items[0].ToString();
            }
            komut = new SqlCommand("SELECT department_id,department_name FROM DEPARTMENTS");
            sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgileri alınırken hata oluştu.", Color.Red, 2000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            departmentDict = new Dictionary<string, int>();
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    departmentDict.Add(table.Rows[i]["department_name"].ToString(), Convert.ToInt32(table.Rows[i]["department_id"]));
                }
                foreach (string Deger in departmentDict.Keys)
                {
                    departmanlar.Items.Add(Deger);
                }
            }
            if (departmanlar.Items.Count > 0)
            {
                departmanlar.Text = departmanlar.Items[0].ToString();
            }
            else
            {
                Scripts.Form.Status.STATUS_LABEL("Proje Ekleyebilmeniz için en az bir DEPARTMAN'a ihtiyacınız var", Color.PaleVioletRed, 5000);
                Scripts.Form.FormManager.FORM_AC(new AnaPanel(), true);
            }

            komut = new SqlCommand("SELECT P.project_name,P.project_image,P.project_detail,(SELECT worker_name FROM WORKERS WHERE worker_id=P.project_manager)AS project_manager,P.project_start_date,P.project_finish_date,P.project_status,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE DEPARTMENTS.department_id = P.department_id)AS department_name FROM PROJECTS P WHERE P.project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
        sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje bilgileri alınırken hata oluştu.", System.Drawing.Color.Red, 4000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table.Rows.Count != 0)
            {
                proje_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["project_image"]);
                proje_adi.Text = table.Rows[0]["project_name"].ToString();
                proje_yoneticileri.Text = table.Rows[0]["project_manager"].ToString();
                departmanlar.Text = table.Rows[0]["department_name"].ToString();
                proje_detayi.Text = table.Rows[0]["project_detail"].ToString();
                proje_baslangic_tarihi.Value = (DateTime)table.Rows[0]["project_start_date"];
                proje_bitis_tarihi.Value = (DateTime)table.Rows[0]["project_finish_date"];
                proje_durumu.Text = table.Rows[0]["project_status"].ToString();
            }
            CalisanTablosu();
           

        }
        string fileName;
        private void button2_Click(object sender, EventArgs e)
        {
            fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if (fileName != null)
                proje_resmi.Image = Image.FromFile(fileName);
        }
        void kaydet()
        {
            Image imag;
            imag = proje_resmi.Image;
            string resim = "Güncellenmedi";
            if (fileName != null)
            {
                resim = "Güncellendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("UPDATE PROJECTS SET project_name = @PROJECT_NAME,project_detail=@PROJECT_DETAIL,project_start_date=@PROJECT_START,project_finish_date=@PROJECT_FINISH,project_image=@PROJECT_IMAGE,project_status=@PROJECT_STATUS,project_manager=@PROJECT_MANAGER,department_id=@DEPARTMENT_ID WHERE project_id=@PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut.Parameters.AddWithValue("@PROJECT_NAME", proje_adi.Text);
            komut.Parameters.AddWithValue("@PROJECT_DETAIL", proje_detayi.Text);
            SqlDateTime time_Start = proje_baslangic_tarihi.Value;
            SqlDateTime time_Finish = proje_bitis_tarihi.Value;
            komut.Parameters.AddWithValue("@PROJECT_START", time_Start);
            komut.Parameters.AddWithValue("@PROJECT_FINISH", time_Finish);
            komut.Parameters.AddWithValue("@PROJECT_STATUS", proje_durumu.Text);
            komut.Parameters.AddWithValue("@PROJECT_MANAGER", managerDict[proje_yoneticileri.Text]);
            komut.Parameters.AddWithValue("@DEPARTMENT_ID", departmentDict[departmanlar.Text]);
            komut.Parameters.Add("@PROJECT_IMAGE", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje veritabanına eklenemedi.", Color.Red, 3000);

            if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
            {
                Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, $"{Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {proje_adi.Text} projesinin bilgilerini güncelledi.Proje Yöneticisi:{proje_yoneticileri.Text},Proje Başlangıç Tarihi:{time_Start},Proje Bitiş Tarihi:{time_Finish},Proje Departmanı:{departmanlar.Text},Proje Durumu:{proje_durumu.Text},Proje Resmi:{resim}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                a.ANAFORM_BILGILER_GUNCELLE();
                Scripts.Form.FormManager.FORM_AC(new projem(), true);
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(proje_resmi.Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (proje_adi.Text.Length > 0 && proje_detayi.Text.Length > 0 && proje_durumu.Text.Length > 0 && proje_durumu.Text.Length <= 30)
            {
                kaydet();
            }
            else label6.Text = "Proje ismi ve Proje detayı boş bırakılamaz.Proje Durumu boş veya 30 karakterden fazla olamaz.";
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
            if (proje_bitis_tarihi.Value <= proje_baslangic_tarihi.Value)
            {
                Scripts.Form.Status.STATUS_LABEL("Hata: Proje bitiş tarihi, başlangıç tarihinin gerisinde olamaz.", Color.White, 2000);
                proje_bitis_tarihi.Value = proje_baslangic_tarihi.Value.AddDays(1);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (proje_calisanlari_dataGridView.Rows.Count > 0 && proje_calisanlari_dataGridView.SelectedRows.Count > 0 && Scripts.SQL.SqlQueries.PROJE_YONETICISI_KONTROL(a.worker_id, a.project_id))
            {
                komut = new SqlCommand("DELETE FROM PROJECT_WORKERS WHERE project_id=@PROJECT_ID AND worker_id=@WORKER_ID");
                komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
                komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(proje_calisanlari_dataGridView.SelectedRows[0].Cells[0].Value));
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan projeden silinmedi.", System.Drawing.Color.Red, 4000);

                if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
                {
                    if (proje_calisanlari_dataGridView.Rows.Count > 0 && proje_calisanlari_dataGridView.SelectedRows.Count == 1)
                    {
                        Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id) + " adlı 'Proje Yöneticisi' " + Scripts.SQL.SqlQueries.PROJECT_NAME_BY_PROJECT_ID(a.project_id) + " projesinden " + proje_calisanlari_dataGridView.Rows[proje_calisanlari_dataGridView.SelectedRows[0].Index].Cells[2].Value.ToString() + " adlı çalışanı çıkarttı.", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                        CalisanTablosu();
                    }
                }
            }
            else Scripts.Form.Status.STATUS_LABEL("Hata:Bu işlem için 'Proje Yöneticisi' olmanız gerekmektedir.'", System.Drawing.Color.Red, 4000);
        }
        void CalisanTablosu()
        {
            komut = new SqlCommand("SELECT W.worker_id,W.worker_image,W.worker_name FROM PROJECT_WORKERS P INNER JOIN WORKERS W ON P.worker_id=W.worker_id  WHERE P.project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Aktif çalışanlar alınırken sorun oluştu.", System.Drawing.Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan Resmi", typeof(System.Drawing.Image));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), table.Rows[i]["worker_name"].ToString());
                }
            }
            proje_calisanlari_dataGridView.DataSource = dataTable;
            for (int i = 0; i < proje_calisanlari_dataGridView.Columns.Count; i++)
                if (proje_calisanlari_dataGridView.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)proje_calisanlari_dataGridView.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (Scripts.SQL.SqlQueries.PROJE_YONETICISI_KONTROL(a.worker_id, a.project_id))
            {
                komut = new SqlCommand("SELECT WORKERS.worker_id,worker_name,worker_image FROM WORKERS  where worker_onay = 1 ");
                komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
                using (var form = new calisan_ekle(komut))
                {
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        DataTable val = form.worker_table;
                        List<int> ids = new List<int>();
                        for (int i = 0; i < proje_calisanlari_dataGridView.Rows.Count; i++)
                        {
                            ids.Add(Convert.ToInt32(proje_calisanlari_dataGridView.Rows[i].Cells[0].Value));
                        }
                        for (int i = 0; i < val.Rows.Count; i++)
                        {
                            if (ids.IndexOf(Convert.ToInt32(val.Rows[i]["worker_id"])) == -1)
                            {
                                Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id) + " adlı 'Proje Yöneticisi' " + Scripts.SQL.SqlQueries.PROJECT_NAME_BY_PROJECT_ID(a.project_id) + " projesine " + val.Rows[i]["worker_name"].ToString() + " adlı çalışanı ekledi.", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                                komut = new SqlCommand("INSERT INTO PROJECT_WORKERS(project_id,worker_id) VALUES(@PROJECT_ID,@WORKER_ID)");
                                komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
                                komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(val.Rows[i]["worker_id"]));
                                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan projeye eklenemedi.", System.Drawing.Color.Red, 4000);

                                if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
                                {
                                    CalisanTablosu();
                                }
                            }
                        }
                    }
                }
            }
            else MessageBox.Show("Proje yöneticisi olmadığınızdan proje bilgilerini güncelleyemezsiniz.");
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new projem(), true);
        }

        private void resmi_Kaldir_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            proje_resmi.Image = Image.FromFile(filePath + @"\default_project_image.jpg");
        }
    }
}

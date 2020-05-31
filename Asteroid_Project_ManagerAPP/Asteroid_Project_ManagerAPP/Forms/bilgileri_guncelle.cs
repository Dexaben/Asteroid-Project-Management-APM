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
    public partial class bilgileri_guncelle : Form
    {
        public bilgileri_guncelle()
        {
            InitializeComponent();
            this.AcceptButton = guncelle;
        }
   
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        private void bilgileri_guncelle_Load(object sender, EventArgs e)
        {
            label1.Text = "";
            label1.ForeColor = Color.Red;
            komut = new SqlCommand("SELECT worker_jobs FROM JOBS");
            komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Pozisyon bilgileri veritabanından çekilemedi.", Color.Red, 2000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);

            if (table.Rows.Count != 0)
            {
                pozisyon_comboBox.Enabled = true;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    pozisyon_comboBox.Items.Add(table.Rows[i][0].ToString());
                }
            }
            else
            {
                pozisyon_comboBox.Enabled = false;
            }
            komut = new SqlCommand("SELECT worker_name,worker_image,worker_gender,worker_mail FROM WORKERS WHERE worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
           sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 4000);
            table =Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if(table != null)
            {
                if(table.Rows.Count !=0)
                {
                    gorev_Resim.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["worker_image"]);
                    calisan_ismi.Text = table.Rows[0]["worker_name"].ToString();
                    pozisyonlarListBox.Items.Clear();
                    pozisyonlarListBox.Items.AddRange(Scripts.SQL.SqlQueries.WORKER_ROLE_CALL_BY_ID(a.worker_id));
                    if(table.Rows[0]["worker_gender"].ToString()=="Erkek")
                    {
                        erkek_radioButton.Checked = true;
                        kadin_radioButton.Checked = false;
                    }
                    else
                    {
                        erkek_radioButton.Checked = false;
                        kadin_radioButton.Checked = true;
                    }
                    calisan_Mail.Text = table.Rows[0]["worker_mail"].ToString();
                }
            }
        }
        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(gorev_Resim.Image);
        }
        void Kaydet()
        {
            Image imag;
            string resim_guncellendi = "Güncellenmedi";
            if (fileName != null)
            {
                imag = Image.FromFile(fileName);
                resim_guncellendi = "Güncellendi";
            }
            else imag = gorev_Resim.Image;
            komut = new SqlCommand("UPDATE WORKERS SET worker_name = @WORKER_NAME,worker_mail=@WORKER_MAIL,worker_gender=@WORKER_GENDER,worker_image=@WORKER_IMAGE,worker_onay=@WORKER_ONAY WHERE worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            komut.Parameters.AddWithValue("@WORKER_NAME", calisan_ismi.Text);
            komut.Parameters.AddWithValue("@WORKER_MAIL", calisan_Mail.Text);
            string cinsiyet = "";
            if (kadin_radioButton.Checked) { komut.Parameters.AddWithValue("@WORKER_GENDER", "Kadın"); cinsiyet = "Kadın"; }
            else { komut.Parameters.AddWithValue("@WORKER_GENDER", "Erkek"); cinsiyet = "Erkek"; }
            komut.Parameters.Add("@WORKER_IMAGE", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            komut.Parameters.AddWithValue("@WORKER_ONAY", Convert.ToBoolean(0));
            komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Bilgiler veritabanına aktarılırken sorun oluştu.", Color.Red, 3000);
            if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
            {
                string positions = "";
                komut = new SqlCommand("DELETE FROM WORKER_POSITIONS WHERE worker_id=@WORKER_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan pozisyonları güncellenemedi.", Color.Red, 4000);

                if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
                {

                    for(int i = 0;i<pozisyonlarListBox.Items.Count;i++)
                    {
                        positions += " {" + pozisyonlarListBox.Items[i].ToString() + "} ";
                        komut = new SqlCommand("INSERT INTO WORKER_POSITIONS(worker_id,worker_job) VALUES(@WORKER_ID,@WORKER_JOB)");
                        komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                        komut.Parameters.AddWithValue("@WORKER_JOB", pozisyonlarListBox.Items[i].ToString());
                        komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                        sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan pozisyonları güncellenemedi.", Color.Red, 3000);
                        Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand);
                    }
                    Application.Restart();
                    Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id) + " adlı çalışan bilgilerini güncelledi. (isim:" + calisan_ismi.Text + ",mail:" + calisan_Mail.Text + ",cinsiyet:" + cinsiyet + ",resim:" + resim_guncellendi + ",pozisyonlar:" + positions + ")",Scripts.SQL.SqlQueries.GET_SERVER_DATE());

                }

            }
        }
        bool EMAIL_KONTROL(string email)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (r.IsMatch(email))
            {
                komut = new SqlCommand("SELECT worker_mail,(SELECT W.worker_mail FROM WORKERS W WHERE W.worker_id=@WORKER_ID)AS my_mail  FROM WORKERS");
                komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan mail bilgileri çekilemedi.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if (table != null)
                {
                    if (table.Rows.Count != 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (table.Rows[i]["worker_mail"].ToString() != table.Rows[i]["my_mail"].ToString())
                            {
                                if (calisan_Mail.Text == table.Rows[i]["worker_mail"].ToString())
                                {
                                    return false;
                                }
                            }
                            else return true;
                        }   
                    }
                }
                return true;
            }
            else return false;
        }
        bool KULLANICIAD_KONTROL(string kullanıcıad)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-ZİıŞşĞğÜüÇçÖö ]+?)+$");
            if (r.IsMatch(kullanıcıad))
            {
                komut = new SqlCommand("SELECT worker_name,(SELECT W.worker_name FROM WORKERS W WHERE W.worker_id=@WORKER_ID)AS my_name FROM WORKERS");
                komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                komut.Connection = Scripts.SQL.SqlConnections.GET_SQLCONNECTION();
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Kullanıcı isimleri veritabanından alınamadı.", Color.Red, 2000);

                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["worker_name"].ToString() != table.Rows[i]["my_name"].ToString())
                        {
                            if (calisan_ismi.Text == table.Rows[i]["worker_name"].ToString())
                            {
                                return false;
                            }
                        }
                        else return true;
                    }
                }
                return true;
            }
            else return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
                if (calisan_ismi.Text.Length > 0 && calisan_Mail.Text.Length > 0)
                {
                    if (pozisyonlarListBox.Items.Count >0)
                    {

                                if (EMAIL_KONTROL(calisan_Mail.Text))
                                {
                                    if (KULLANICIAD_KONTROL(calisan_ismi.Text))
                                    {
                                        Kaydet();
                                    }
                                    else label1.Text = "Kullanıcı adı geçersiz veya kullanılıyor.";
                                }
                                else label1.Text = "Email uygun değil veya kullanılıyor.";
                    }
                    else label1.Text = "En az bir pozisyona sahip olmalısınız.";
                }
                else
                {
                    label1.Text = "'Kullanıcı Adı' , 'Mail' boş bırakılamaz!";
                }
         
        }
        string fileName;
        private void button2_Click(object sender, EventArgs e)
        {

                fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if(fileName != null)
                gorev_Resim.Image = Image.FromFile(fileName);

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (pozisyon_diger.Text != "") pozisyon_comboBox.Enabled = false;
            else pozisyon_comboBox.Enabled = true;
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(pozisyonlarListBox.Items.Count >0 && pozisyonlarListBox.SelectedItems.Count ==1)
            {
                pozisyonlarListBox.Items.Remove(pozisyonlarListBox.Items[pozisyonlarListBox.SelectedIndex]);
            }
        }
      
        private void button3_Click(object sender, EventArgs e)
        {

            if (pozisyon_diger.Text.Length > 0)
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyon_diger.Text) && pozisyonlarListBox.Items.IndexOf(pozisyon_diger.Text) == -1)
                {
                    pozisyonlarListBox.Items.Add(pozisyon_diger.Text);
                }
            }
            else
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyon_comboBox.Text) && pozisyonlarListBox.Items.IndexOf(pozisyon_comboBox.Text) == -1)
                {
                    pozisyonlarListBox.Items.Add(pozisyon_comboBox.Text);
                }
            }
        }
        bool POZISYON_UYGUNLUK_KONTROL(string POZISYON)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9 ğüşıöçĞÜŞİÖÇ#+\+()]{2,50}$");
            if (r.IsMatch(POZISYON)) return true;
            else return false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new profil(), true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            gorev_Resim.Image = Image.FromFile(filePath + @"\default_worker_image.jpg");
        }
    }
}

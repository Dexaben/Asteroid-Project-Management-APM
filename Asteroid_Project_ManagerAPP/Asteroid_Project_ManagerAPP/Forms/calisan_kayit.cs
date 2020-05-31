using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    
    public partial class calisan_kayit : Form
    {
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public calisan_kayit()
        {
            InitializeComponent();
            this.AcceptButton = kayit_ol;
        }
        string fileName;
        void Kaydet()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources\\iconlar");
            Image imag = Image.FromFile(filePath + @"\default_worker_image.jpg");
            string resim = "Eklenmedi";
            if (fileName != null)
            {
                resim = "Eklendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("INSERT INTO WORKERS(worker_name,worker_password,worker_mail,worker_gender,worker_image) VALUES(@worker_name,@worker_password,@worker_mail,@worker_gender,@worker_image)");
            komut.Parameters.AddWithValue("@worker_name", kullanici_adi.Text);
            komut.Parameters.AddWithValue("@worker_password", Scripts.Tools.CryptographyFunctions.Encrypt(sifre_tekrar.Text));
            komut.Parameters.AddWithValue("@worker_mail", email.Text);
            string cinsiyet;
            if (kadin_radioButton.Checked)
            { komut.Parameters.AddWithValue("@worker_gender", "Kadın"); cinsiyet = "Kadın"; }
            else
            {
                komut.Parameters.AddWithValue("@worker_gender", "Erkek");
                cinsiyet = "Erkek";
            }
            if (imag == null) komut.Parameters.Add("@worker_image", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            else komut.Parameters.Add("@worker_image", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            Scripts.SQL.SQL_COMMAND sqlcommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Bilgiler veritabanına aktarılırken sorun oluştu.", Color.Red, 3000);
            if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlcommand))
            {
                komut = new SqlCommand("SELECT TOP 1 worker_id FROM WORKERS WHERE worker_name = @WORKER_NAME");
                komut.Parameters.AddWithValue("@WORKER_NAME", kullanici_adi.Text);
                sqlcommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan ID'si alınamadı.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlcommand);
                string positions = "";
                if (table != null)
                    for (int i = 0; i < pozisyon_liste.Items.Count; i++)
                    {
                        positions += " {"+pozisyon_liste.Items[i].ToString()+"} ";
                        komut = new SqlCommand("INSERT INTO WORKER_POSITIONS(worker_id,worker_job) VALUES((SELECT TOP 1 worker_id FROM WORKERS WHERE worker_name = @WORKER_NAME),@WORKER_JOB)");
                        komut.Parameters.AddWithValue("@WORKER_NAME", kullanici_adi.Text);
                        komut.Parameters.AddWithValue("@WORKER_JOB", pozisyon_liste.Items[i].ToString());
                        sqlcommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan pozisyonları güncellenemedi.", Color.Red, 3000);
                        Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlcommand);
                    }
                calisan_giris calisan_Giris = new calisan_giris();
                calisan_Giris.kullanici_adi_textBox.Text = kullanici_adi.Text;
                Scripts.Form.FormManager.FORM_AC(calisan_Giris, true);
                Scripts.Tools.LogTools.LOG_ENTER(Convert.ToInt32(table.Rows[0][0]), Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(Convert.ToInt32(table.Rows[0][0])) + " adlı çalışanın kaydı oluşturuldu. (isim:" + kullanici_adi.Text + ",mail:" + email.Text + ",cinsiyet:" + cinsiyet + ",resim:" + resim + ",pozisyonlar:" + positions + ")", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
            }
        }
        bool SIFRE_KONTROL(string sifre)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$");
            if (r.IsMatch(sifre))
                return true;
            else return false;
        }
        bool EMAIL_KONTROL(string email)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (r.IsMatch(email))
            {
                komut = new SqlCommand("SELECT worker_mail FROM WORKERS");
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan mail bilgileri çekilemedi.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if (table != null)
                {
                    if (table.Rows.Count != 0)
                    {
                        for(int i = 0;i<table.Rows.Count;i++)
                        {
                            if (this.email.Text == table.Rows[i]["worker_mail"].ToString()) return false;
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
                komut = new SqlCommand("SELECT worker_name FROM WORKERS");
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Kullanıcı isimleri veritabanından alınamadı.", Color.Red, 2000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if(table.Rows.Count != 0)
                {
                    for(int i = 0;i<table.Rows.Count;i++)
                    {
                        if (kullanici_adi.Text == table.Rows[i]["worker_name"].ToString())
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else return false;
        }
   
        private void button2_Click(object sender, EventArgs e)
        {
            if (sifre.Text == sifre_tekrar.Text)
            {
                if (sifre.Text.Length > 0 && sifre_tekrar.Text.Length > 0 && email.Text.Length > 0)
                {
                    if (pozisyon_liste.Items.Count > 0)
                    {
                        if (sifre.Text == sifre_tekrar.Text)
                        {
                            if (SIFRE_KONTROL(sifre_tekrar.Text))
                            {
                                if(EMAIL_KONTROL(email.Text))
                                {
                                    if (KULLANICIAD_KONTROL(kullanici_adi.Text))
                                    {
                                        Kaydet();
                                    }
                                    else hata_label.Text = "Kullanıcı adı geçersiz veya kullanılıyor.";
                                }
                                else hata_label.Text = "Email uygun değil veya kullanılıyor.";
                            }
                            else hata_label.Text = "Şifre, 6 karakterden uzun olmalı, en az bir rakam ve en az bir harf içermelidir.";
                        }
                        else hata_label.Text = "Şifreler eşleşmiyor!";
                    }
                    else hata_label.Text = "En az bir pozisyon seçmelisiniz.";
                }
                else
                {
                    hata_label.Text = "'Kullanıcı Adı' , 'Şifre' , 'Mail' boş bırakılamaz!";
                }
            }
            else
            {
                hata_label.Text = "Şifreler birbiriyle uyuşmuyor.";
            }
        }
        private void calisan_kayit_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT worker_jobs FROM JOBS");
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Pozisyon bilgileri veritabanından çekilemedi.", Color.Red, 2000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table.Rows.Count != 0)
            {
                pozisyonlar_comboBox.Enabled = true;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    pozisyonlar_comboBox.Items.Add(table.Rows[i][0].ToString());
                }
            }
            else
            {
                pozisyonlar_comboBox.Enabled = false;
            }
            hata_label.Text = "";
            hata_label.ForeColor = Color.Red;
        }
        private void button1_Click(object sender, EventArgs e)
        {
                fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
                if(fileName != null)
                calisan_resim.Image = Image.FromFile(fileName);

           
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (pozisyonlar_diger.Text != "") pozisyonlar_comboBox.Enabled = false;
            else pozisyonlar_comboBox.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new calisan_giris(), true);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (pozisyon_liste.Items.Count > 0 && pozisyon_liste.SelectedItems.Count == 1)
            {
                pozisyon_liste.Items.Remove(pozisyon_liste.Items[pozisyon_liste.SelectedIndex]);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (pozisyonlar_diger.Text.Length > 0)
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyonlar_diger.Text) && pozisyon_liste.Items.IndexOf(pozisyonlar_diger.Text) == -1)
                {
                    pozisyon_liste.Items.Add(pozisyonlar_diger.Text);
                }
            }
            else
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyonlar_comboBox.Text) && pozisyon_liste.Items.IndexOf(pozisyonlar_comboBox.Text) == -1)
                {
                    pozisyon_liste.Items.Add(pozisyonlar_comboBox.Text);
                }
            }
        }
        bool POZISYON_UYGUNLUK_KONTROL(string POZISYON)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9 ğüşıöçĞÜŞİÖÇ#+\+()]{2,50}$");
            if (r.IsMatch(POZISYON)) return true;
            else return false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            calisan_resim.Image = Image.FromFile(filePath + @"\default_worker_image.jpg");
        }
    }
}

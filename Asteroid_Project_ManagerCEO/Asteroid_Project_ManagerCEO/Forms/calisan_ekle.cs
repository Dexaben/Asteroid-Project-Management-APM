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
    public partial class calisan_ekle : Form
    {
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        public calisan_ekle()
        {
            InitializeComponent();
            this.AcceptButton = calisan_ekle_button;
        }

        private void text_Changed(object sender, EventArgs e)
        {
            if(pozisyon_diger.Text.Length > 0) pozisyonlar.Enabled = false;
            else pozisyonlar.Enabled = true;
        }
        string fileName;

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = F.OPENFILEIMAGE();
            if (fileName != null)
                calisan_resmi.Image = Image.FromFile(fileName);

        }
        void Kaydet()
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            System.Drawing.Image imag = Image.FromFile(filePath + @"\default_worker_image.jpg");
            string resim = "Eklenmedi";
            if (fileName != null)
            {
                resim = "Eklendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("INSERT INTO WORKERS(worker_name,worker_image,worker_gender,worker_password,worker_mail,worker_onay) VALUES(@wrk_name,@wrk_image,@wrk_gender,@wrk_password,@wrk_mail,@wrk_onay)");
                    komut.Parameters.AddWithValue("@wrk_name", calisan_ismi.Text);
                    komut.Parameters.AddWithValue("@wrk_password", F.Encrypt(calisan_sifre.Text));
            string cinsiyet;
            if(kadin_radioButton.Checked)
                    {
                        komut.Parameters.AddWithValue("@wrk_gender", kadin_radioButton.Text);
                cinsiyet = "Kadın";
                    }
                     else
                    {
                cinsiyet = "Erkek";
                        komut.Parameters.AddWithValue("@wrk_gender", erkek_radioButton.Text);
                    }
                    komut.Parameters.AddWithValue("@wrk_mail", calisan_mail.Text);
                    komut.Parameters.AddWithValue("@wrk_onay", 1);
                    komut.Parameters.Add("@wrk_image", SqlDbType.Image, 0).Value = F.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan veritabanına eklenemedi.", Color.Red, 3000))
            {
                string positions = "";

                for (int i = 0; i < pozisyonlar_liste.Items.Count; i++)
                {
                    positions += " {" + pozisyonlar_liste.Items[i].ToString() + "} ";
                    komut = new SqlCommand("INSERT INTO WORKER_POSITIONS(worker_id,worker_job) VALUES((SELECT TOP 1 worker_id FROM WORKERS WHERE worker_name = @WORKER_NAME),@WORKER_JOB)");
                    komut.Parameters.AddWithValue("@WORKER_NAME", calisan_ismi.Text);
                    komut.Parameters.AddWithValue("@WORKER_JOB", pozisyonlar_liste.Items[i].ToString());
                    F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan pozisyonları güncellenemedi.", Color.Red, 3000);
                }
                F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından {calisan_ismi.Text} adlı çalışanın kaydı oluşturuldu.İsim:{calisan_ismi.Text},Mail:{calisan_mail.Text},Cinsiyet:{cinsiyet},Resim:{resim},Pozisyonlar:{positions}", F.GET_SERVER_DATE());
                F.FORM_AC(new calisan(), true);
            }
                
        }
        bool SIFRE_KONTROL(string sifre)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$");
            if (r.IsMatch(sifre))
                return true;
            else return false;
        }
        bool KULLANICIAD_KONTROL(string kullanıcıad)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-ZİıŞşĞğÜüÇçÖö ]+?)+$");
            if (r.IsMatch(kullanıcıad))
            {
                komut = new SqlCommand("SELECT worker_name FROM WORKERS");
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan isimleri çekilemedi.", Color.Red, 3000);
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (calisan_ismi.Text == table.Rows[i]["worker_name"].ToString()) return false;
                    }
                }
                return true;
            }
            else return false;
        }
        bool EMAIL_KONTROL(string email)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (r.IsMatch(email))
            {
                komut = new SqlCommand("SELECT worker_mail FROM WORKERS");
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan mail bilgileri çekilemedi.", Color.Red, 3000);
                if (table != null)
                {
                   if(table.Rows.Count !=0)
                    {
                        if (calisan_mail.Text == table.Rows[0]["worker_mail"].ToString()) return false;
                    }
                }
                return true;
            }
            else return false;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (calisan_sifre.Text == sifre_tekrar.Text)
            {
                if (calisan_ismi.Text.Length > 0 && sifre_tekrar.Text.Length > 0 && calisan_mail.Text.Length > 0)
                {
                    if (calisan_sifre.Text == sifre_tekrar.Text)
                    {
                        if (pozisyonlar_liste.Items.Count > 0)
                        {
                            if (SIFRE_KONTROL(sifre_tekrar.Text))
                            {
                                if (EMAIL_KONTROL(calisan_mail.Text))
                                {
                                    if (KULLANICIAD_KONTROL(calisan_ismi.Text))
                                    {
                                        Kaydet();
                                    }
                                    else label8.Text = "'Çalışan İsmi' geçersiz veya kullanılıyor. Boşluk bırakılamaz.";
                                }
                              else label8.Text = "Email uygun değil veya kullanılıyor.";
                            }
                            else label8.Text = "Şifre, 6 karakterden uzun olmalı, en az bir rakam ve en az bir harf içermelidir.";
                        }
                        else label8.Text = "Çalışan en az bir pozisyon seçmelidir.";
                    }
                    else label8.Text = "Şifreler eşleşmiyor!";
                }
                else
                {
                    label8.Text = "'Çalışan İsmi' , 'Çalışan Şifre' , 'Çalışan Mail' boş bırakılamaz!";
                }
            }
            else
            {
                label8.Text = "Şifreler birbiriyle uyuşmuyor.";
            }
        }
        private void calisan_ekle_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT worker_jobs FROM JOBS");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Pozisyon isimleri veritabanından çekilemedi", Color.Red, 3000);
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    pozisyonlar.Items.Add(table.Rows[i][0].ToString());
                }
            }
            else pozisyonlar.Enabled = false;
            if (pozisyonlar.Items.Count >0) pozisyonlar.Text = pozisyonlar.Items[0].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pozisyon_diger.Text.Length > 0)
            {
                
                if (POZISYON_UYGUNLUK_KONTROL(pozisyon_diger.Text) && pozisyonlar_liste.Items.IndexOf(pozisyon_diger.Text) == -1)
                {
                    pozisyonlar_liste.Items.Add(pozisyon_diger.Text);
                }
                pozisyon_diger.Text = "";
            }
            else
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyonlar.Text) && pozisyonlar_liste.Items.IndexOf(pozisyonlar.Text) == -1)
                {
                    pozisyonlar_liste.Items.Add(pozisyonlar.Text);
                }
                pozisyon_diger.Text = "";
            }
        }
        bool POZISYON_UYGUNLUK_KONTROL(string POZISYON)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9 ğüşıöçĞÜŞİÖÇ]{2,50}$");
            if (r.IsMatch(POZISYON)) return true;
            else return false;
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if(pozisyonlar_liste.Items.Count >0)
                if(pozisyonlar_liste.SelectedItems.Count >0)
            pozisyonlar_liste.Items.Remove(pozisyonlar_liste.Items[pozisyonlar_liste.SelectedIndex]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new calisan(), true);
        }
    }
}

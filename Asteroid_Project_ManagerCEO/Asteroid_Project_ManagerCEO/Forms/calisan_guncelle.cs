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
    public partial class calisan_guncelle : Form
    {
        int calisanID;
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        public calisan_guncelle(int calisan_id)
        {
            calisanID = calisan_id;
            InitializeComponent();
            this.AcceptButton = calisan_bilgi_guncelle;
        }

        private void calisan_guncelle_Load(object sender, EventArgs e)
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
            if (pozisyonlar.Items.Count > 0) pozisyonlar.Text = pozisyonlar.Items[0].ToString();
            komut = new SqlCommand("SELECT worker_id,worker_name,worker_image,worker_gender,worker_mail FROM WORKERS WHERE worker_id=@wrk_id");
                komut.Parameters.AddWithValue("@wrk_id", calisanID);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri alınırken hata oluştu.", Color.Red, 3000);
            if(table !=null)
            {
                calisan_resim.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["worker_image"]);
                calisan_id.Text = (table.Rows[0]["worker_id"]).ToString();
                calisan_isim.Text = (table.Rows[0]["worker_name"]).ToString();
                textBox2.Text = (table.Rows[0]["worker_mail"]).ToString();
                calisan_pozisyonlari.Items.Clear();
                calisan_pozisyonlari.Items.AddRange(F.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(table.Rows[0]["worker_id"])));
                if ((table.Rows[0]["worker_gender"]).ToString() == "Erkek")
                {
                    erkek_radioButton.Checked = true;
                    kadin_radioButton.Checked = false;
                }
                else
                {
                    kadin_radioButton.Checked = true;
                    erkek_radioButton.Checked = false;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (calisan_isim.Text.Length > 0 && textBox2.Text.Length > 0)
            {
                if (calisan_pozisyonlari.Items.Count > 0)
                {

                    if (EMAIL_KONTROL(textBox2.Text))
                    {
                        if (KULLANICIAD_KONTROL(calisan_isim.Text))
                        {
                            Kaydet();
                        }
                        else label8.Text = "Kullanıcı adı geçersiz veya kullanılıyor.";
                    }
                    else label8.Text = "Email uygun değil veya kullanılıyor.";
                }
                else label8.Text = "En az bir pozisyona sahip olmalısınız.";
            }
            else
            {
                label8.Text = "'Kullanıcı Adı' , 'Mail' boş bırakılamaz!";
            }

        }
        private void text_Changed(object sender, EventArgs e)
        {
            if (pozisyon_diger.Text.Length > 0) pozisyonlar.Enabled = false;
            else pozisyonlar.Enabled = true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            fileName = F.OPENFILEIMAGE();
            if(fileName != null)
            calisan_resim.Image = Image.FromFile(fileName);
        }
        string fileName;
        void Kaydet()
        {
            System.Drawing.Image imag = calisan_resim.Image;
           string resim = "Güncellenmedi";
            if (fileName != null)
            {
                resim = "Güncellendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("UPDATE WORKERS SET worker_name=@wrk_name,worker_image=@wrk_image,worker_gender=@wrk_gender,worker_mail=@wrk_mail WHERE worker_id=@wrk_id");
            komut.Parameters.AddWithValue("@wrk_id", calisanID);
            komut.Parameters.AddWithValue("@wrk_name", calisan_isim.Text);
            string cinsiyet = "";
            if (kadin_radioButton.Checked)
            {
                cinsiyet = "Kadın";
                komut.Parameters.AddWithValue("@wrk_gender", kadin_radioButton.Text);
            }
            else
            {
                cinsiyet = "Erkek";
                komut.Parameters.AddWithValue("@wrk_gender", erkek_radioButton.Text);
            }
            komut.Parameters.AddWithValue("@wrk_mail", textBox2.Text);
            komut.Parameters.AddWithValue("@wrk_onay", 1);
            komut.Parameters.Add("@wrk_image", SqlDbType.Image, 0).Value = F.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (calisan_isim.Text.Length > 0)
            {
                if(F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan bilgileri güncellenirken sorun oluştu.", Color.Red, 4000))
                {
                    komut = new SqlCommand("DELETE FROM WORKER_POSITIONS WHERE worker_id=@WORKER_ID");
                    komut.Parameters.AddWithValue("@WORKER_ID", calisanID);
                    if (F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan bilgileri güncellenirken sorun oluştu.", Color.Red, 4000))
                    {
                        string positions="";
                        for (int i = 0; i < calisan_pozisyonlari.Items.Count; i++)
                        {
                            positions += " {" + calisan_pozisyonlari.Items[i].ToString() + "} ";
                            komut = new SqlCommand("INSERT INTO WORKER_POSITIONS(worker_id,worker_job) VALUES(@WORKER_ID,@WORKER_JOB)");
                            komut.Parameters.AddWithValue("@WORKER_ID", calisanID);
                            komut.Parameters.AddWithValue("@WORKER_JOB", calisan_pozisyonlari.Items[i].ToString());
                            F.SQL_EXECUTENONQUERY(komut, "Hata:Çalışan pozisyonları güncellenemedi.", Color.Red, 3000);
                        }
                        F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından (ID-{calisan_id.Text}){calisan_isim.Text} adlı çalışanın kaydı oluşturuldu.İsim:{calisan_isim.Text},Mail:{textBox2.Text},Cinsiyet:{cinsiyet},Resim:{resim},Pozisyonlar:{positions}", F.GET_SERVER_DATE());
                        F.FORM_AC(new calisan(), true);
                    }     
                }
            }
        }
        bool EMAIL_KONTROL(string email)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            if (r.IsMatch(email))
            {
                komut = new SqlCommand("SELECT worker_mail,(SELECT W.worker_mail FROM WORKERS W WHERE W.worker_id=@WORKER_ID)AS my_mail  FROM WORKERS");
                komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(calisan_id.Text));
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan mail bilgileri çekilemedi.", Color.Red, 3000);
                if (table != null)
                {
                    if (table.Rows.Count != 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            if (table.Rows[i]["worker_mail"].ToString() != table.Rows[i]["my_mail"].ToString())
                            {
                                if (textBox2.Text == table.Rows[i]["worker_mail"].ToString())
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
                komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(calisan_id.Text));
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Kullanıcı isimleri veritabanından alınamadı.", Color.Red, 2000);
                if (table.Rows.Count != 0)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["worker_name"].ToString() != table.Rows[i]["my_name"].ToString())
                        {
                            if (calisan_isim.Text == table.Rows[i]["worker_name"].ToString())
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
        private void button3_Click(object sender, EventArgs e)
        {
            if (pozisyon_diger.Text.Length > 0)
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyon_diger.Text) && calisan_pozisyonlari.Items.IndexOf(pozisyon_diger.Text) == -1)
                {
                    calisan_pozisyonlari.Items.Add(pozisyon_diger.Text);
                }
            }
            else
            {
                if (POZISYON_UYGUNLUK_KONTROL(pozisyonlar.Text) && calisan_pozisyonlari.Items.IndexOf(pozisyonlar.Text) == -1)
                {
                    calisan_pozisyonlari.Items.Add(pozisyonlar.Text);
                }
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
            if (calisan_pozisyonlari.Items.Count > 0)
                if (calisan_pozisyonlari.SelectedItems.Count > 0)
                    calisan_pozisyonlari.Items.Remove(calisan_pozisyonlari.Items[calisan_pozisyonlari.SelectedIndex]);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new calisan(), true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            calisan_resim.Image = Image.FromFile(filePath + @"\default_worker_image.jpg");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace Asteroid_Project_ManagerCEO
{
    public partial class kayit : Form
    {
        SqlCommand komut = new SqlCommand();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public kayit()
        {
            InitializeComponent();
            this.AcceptButton = kayit_ol;
        }

        private void kayit_Load(object sender, EventArgs e)
        {
            hata_label.Text = "";
            hata_label.ForeColor = Color.Red;
        }

        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            if (sifre.Text == sifre_tekrar.Text)
            {
                if (kullanici_adi.Text.Length > 0 && sifre_tekrar.Text.Length > 0 && sirket_ismi.Text.Length > 0)
                {
                    if (sifre.Text == sifre_tekrar.Text)
                    {
                        if (SIFRE_KONTROL(sifre_tekrar.Text))
                        {
                            if (KULLANICIAD_KONTROL(kullanici_adi.Text))
                            {
                                Kaydet();
                            }
                            else hata_label.Text = "Kullanıcı adı geçersiz.";
                        }
                        else hata_label.Text = "Şifre, 6 karakterden uzun olmalı, en az bir rakam ve en az bir harf içermelidir.";
                    }
                    else hata_label.Text = "Şifreler eşleşmiyor!";
                }
                else
                {
                    hata_label.Text = "'Kullanıcı Adı' , 'Şifre' , 'Şirket İsmi' boş bırakılamaz!";
                }
            }
            else
            {
                hata_label.Text = "Şifreler birbiriyle uyuşmuyor.";
            }

        }
        string fileName;

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if(fileName != null)
            sirket_resmi.Image = Image.FromFile(fileName);
        }
        [Obsolete]
        void Kaydet()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources\\iconlar");
            System.Drawing.Image imag = Image.FromFile(filePath + @"\default_company_image.jpg");
            string resim = "Eklenmedi";
            if (fileName != null)
            {
                resim = "Eklendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("UPDATE PROGRAM SET yonetici_adi=@YONETICIAD,yonetici_password=@YONETICIPASSWORD,company_name=@COMPANYNAME,company_logo=@COMPANYLOGO,company_detail=@COMPANYDETAIL WHERE yonetici_id=1");
            komut.Parameters.AddWithValue("@YONETICIAD", kullanici_adi.Text);
            komut.Parameters.AddWithValue("@YONETICIPASSWORD", Scripts.Tools.CryptographyFunctions.Encrypt(sifre.Text));
            komut.Parameters.AddWithValue("@COMPANYNAME", sirket_ismi.Text);
            komut.Parameters.AddWithValue("@COMPANYDETAIL", sirket_detay.Text);
            if (imag == null)
            {
                komut.Parameters.Add("@COMPANYLOGO", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {

                komut.Parameters.Add("@COMPANYLOGO", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Program bilgisi güncellenemedi.", Color.Red, 4000);
            if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
            {
                a.SIRKETBILGI_GUNCELLE();
                Scripts.Form.FormManager.FORM_AC(new giris(), true);
                Scripts.Tools.LogTools.LOG_ENTER(0, $"Hoşgeldiniz.({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarından Şirket kaydı oluşturuldu.Kullanıcı Adı:{kullanici_adi.Text},Şirket İsim:{sirket_ismi.Text},Şirket Resmi:{resim}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                Scripts.Form.Status.STATUS_LABEL("Durum:Kayıt olundu.Giriş bekleniyor.", Color.White);
            }
        }
        bool SIFRE_KONTROL(string sifre)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z]{6,}$");
         
            if (r.IsMatch(sifre))
                return true;
            else return false;
          
        }
        bool KULLANICIAD_KONTROL(string sifre)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^([a-zA-Z])[a-zA-Z_-]*[\w_-]*[\S]$|^([a-zA-Z])[0-9_-]*[\S]$|^[a-zA-Z]*[\S]$");
            if (r.IsMatch(sifre))
                return true;
            else return false;
        }
        private void sifre_enter(object sender, EventArgs e)
        {
            if (sifre.Text.Length > 0)
            {
                sifre_tekrar.Enabled = true;
            }
            else sifre_tekrar.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            sirket_resmi.Image = Image.FromFile(filePath + @"\default_company_image.jpg");
        }
    }
}

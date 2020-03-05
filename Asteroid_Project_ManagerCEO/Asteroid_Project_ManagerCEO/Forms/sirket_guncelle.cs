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
    public partial class sirket_guncelle : Form
    {
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public sirket_guncelle()
        {
            InitializeComponent();
            this.AcceptButton = departman_guncelle;
        }
        string fileName;
        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();
        }
       
        void Kaydet()
        {

            System.Drawing.Image imag = sirket_resmi.Image;
            string resim = "Güncellenmedi";
            if (fileName != null)
            {
                resim = "Güncellendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("UPDATE PROGRAM SET company_name=@dp_name,company_detail=@dp_details,company_logo=@dp_logo WHERE yonetici_id=1"); 
                    komut.Parameters.AddWithValue("@dp_name", sirket_adi.Text);
                    komut.Parameters.AddWithValue("@dp_details", sirket_detayi.Text);
                    komut.Parameters.Add("@dp_logo", SqlDbType.Image, 0).Value = F.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (sirket_adi.Text.Length > 0)
            {
                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Şirket bilgileri güncellenemedi.", Color.Red, 4000))
                {
                    F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından şirket bilgileri güncellendi.Şirket İsmi:{sirket_adi.Text},Şirket Resmi:{resim}", F.GET_SERVER_DATE());
                    F.FORM_AC(new sirket(), true);
                    a.SIRKETBILGI_GUNCELLE();
                }
            }
          

        }
        private void button2_Click(object sender, EventArgs e)
        {

            fileName = F.OPENFILEIMAGE();
            if(fileName != null)
            sirket_resmi.Image = Image.FromFile(fileName);
        }

        private void sirket_guncelle_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT company_logo,company_name,company_detail FROM PROGRAM WHERE yonetici_id=1");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Şirket bilgileri alınamadı.", Color.Red, 4000);
            if(table.Rows.Count !=0)
            {
                sirket_resmi.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["company_logo"]);
                sirket_adi.Text = (table.Rows[0]["company_name"]).ToString();
                sirket_detayi.Text = (table.Rows[0]["company_detail"]).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new sirket(), true);
        }

        private void sirket_resmi_kaldir_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            sirket_resmi.Image = Image.FromFile(filePath + @"\default_company_image.jpg");
        }
    }
}

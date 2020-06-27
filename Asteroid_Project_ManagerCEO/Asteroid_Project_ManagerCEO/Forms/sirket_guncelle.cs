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
                    komut.Parameters.Add("@dp_logo", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (sirket_adi.Text.Length > 0)
            {
                Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Şirket bilgileri güncellenemedi.", Color.Red, 4000);
                if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                {
                    Scripts.Tools.LogTools.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından şirket bilgileri güncellendi.Şirket İsmi:{sirket_adi.Text},Şirket Resmi:{resim}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                    Scripts.Form.FormManager.FORM_AC(new sirket(), true);
                    a.SIRKETBILGI_GUNCELLE();
                }
            }
          

        }
        private void button2_Click(object sender, EventArgs e)
        {

            fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if(fileName != null)
            sirket_resmi.Image = Image.FromFile(fileName);
        }

        private void sirket_guncelle_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT company_logo,company_name,company_detail FROM PROGRAM WHERE yonetici_id=1");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Şirket bilgileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if(table.Rows.Count !=0)
            {
                sirket_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["company_logo"]);
                sirket_adi.Text = (table.Rows[0]["company_name"]).ToString();
                sirket_detayi.Text = (table.Rows[0]["company_detail"]).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new sirket(), true);
        }

        private void sirket_resmi_kaldir_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            sirket_resmi.Image = Image.FromFile(filePath + @"\default_company_image.jpg");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class departman_ekle : Form
    {
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        public departman_ekle()
        {
            InitializeComponent();
        }
        string fileName;
        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fileName = F.OPENFILEIMAGE();
            if(fileName != null)
            departman_resim.Image = Image.FromFile(fileName);
        }
        void Kaydet()
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            System.Drawing.Image imag = Image.FromFile(filePath + @"\default_department_image.jpg");
            string resim = "Eklenmedi";
            if (fileName != null)
            {
                resim = "Eklendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("INSERT INTO DEPARTMENTS(department_name,department_details,department_logo) VALUES(@dp_name,@dp_details,@dp_logo)");
            komut.Parameters.AddWithValue("@dp_name", departman_ismi.Text);
            komut.Parameters.AddWithValue("@dp_details", departman_detay.Text);
            komut.Parameters.Add("@dp_logo", SqlDbType.Image, 0).Value = F.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (departman_ismi.Text.Length > 0)
            {
                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Departman eklenemedi.", Color.Red, 4000))
                {
                    F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından {departman_ismi.Text} adlı departman eklendi.Departman İsmi:{departman_ismi.Text},Departman Resmi:{resim}",F.GET_SERVER_DATE());
                    F.FORM_AC(new departman(), true);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new departman(), true);
        }

        private void departman_resmi_kaldir_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            departman_resim.Image = Image.FromFile(filePath + @"\default_department_image.jpg");
        }
    }
}

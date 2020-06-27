using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class departman_guncelle : Form
    {
        public int department_ID { get; set; }
        SqlCommand komut = new SqlCommand();
        
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public departman_guncelle()
        {
            InitializeComponent();
            this.AcceptButton = departman_guncelle_buton;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Kaydet();
        }
        string fileName;
        void Kaydet()
        {
            System.Drawing.Image imag = departman_resim.Image;
            string resim = "Güncellenmedi";
            if (fileName != null)
            {
                resim = "Güncellendi";
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("UPDATE DEPARTMENTS SET department_name=@dp_name,department_details=@dp_details,department_logo=@dp_logo WHERE department_id=@dp_id");
            komut.Parameters.AddWithValue("@dp_name", departman_ismi.Text);
            komut.Parameters.AddWithValue("@dp_id", departman_id.Text);
            komut.Parameters.AddWithValue("@dp_details", departman_aciklamasi.Text);
            komut.Parameters.Add("@dp_logo", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            if (departman_ismi.Text.Length > 0)
            {
                Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgileri güncellenemedi.", Color.Red, 3000);
                if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                { Scripts.Form.FormManager.FORM_AC(new departman(), true);  Scripts.Tools.LogTools.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından (ID-{department_ID}) {departman_ismi.Text} adlı departmanın bilgileri güncellendi.Departman İsmi:{departman_ismi.Text},Departman Resmi:{resim}", Scripts.SQL.SqlQueries.GET_SERVER_DATE()); }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if(fileName != null)
            departman_resim.Image = Image.FromFile(fileName);
        }
        private void departman_guncelle_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT department_logo,department_name,department_details FROM DEPARTMENTS WHERE department_id=@id");
            komut.Parameters.AddWithValue("@id", department_ID);
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table.Rows.Count != 0)
            {
                departman_id.Text = department_ID.ToString();
                departman_ismi.Text = (table.Rows[0]["department_name"]).ToString();
                departman_aciklamasi.Text = (table.Rows[0]["department_details"]).ToString();
                departman_resim.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["department_logo"]);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new departman(), true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources\\iconlar");
            departman_resim.Image = Image.FromFile(filePath + @"\default_department_image.jpg");
        }
    }
}


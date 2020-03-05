using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    public partial class AnaPanel : Form
    {
        public AnaPanel()
        {
            InitializeComponent();
        }
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        FUNCTIONS F = new FUNCTIONS();
        private void AnaPanel_Load(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlCommand komut = new System.Data.SqlClient.SqlCommand("SELECT worker_name FROM WORKERS WHERE worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            DataTable table = F.SQL_SELECT_DATATABLE(komut, "Hata:Bilgiler veritabanında alınamadı.", Color.Red, 4000);
            if (table != null)
            {
                kullanici_ismi.Text = table.Rows[0]["worker_name"].ToString();
            }
        }
    }
}
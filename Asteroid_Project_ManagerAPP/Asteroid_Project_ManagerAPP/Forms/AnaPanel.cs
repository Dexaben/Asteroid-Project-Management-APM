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
        private void AnaPanel_Load(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlCommand komut = new System.Data.SqlClient.SqlCommand("SELECT worker_name FROM WORKERS WHERE worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            komut.Connection = Scripts.SQL.SqlConnections.sqlConnection;
            Scripts.SQL.SQL_COMMAND sqlCommand =new Scripts.SQL.SQL_COMMAND(komut, "Hata:Bilgiler veritabanında alınamadı.", Color.Red, 4000);
            DataTable table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table != null)
            {
                kullanici_ismi.Text = table.Rows[0]["worker_name"].ToString();
            }
            DuyurulariListele();
        }
        private void DuyurulariListele()
        {

        }
    }
}
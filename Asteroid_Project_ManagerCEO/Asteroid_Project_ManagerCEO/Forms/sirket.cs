using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;


namespace Asteroid_Project_ManagerCEO
{
    public partial class sirket : Form
    {
        SqlCommand komut = new SqlCommand();
        
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public sirket()
        {
            InitializeComponent();
            this.AcceptButton = sirket_bilgileri_guncelle;
        }

        private void sirket_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT department_id,department_logo,department_name,department_details FROM DEPARTMENTS");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman verileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Departman ID", typeof(int));
            dataTable.Columns.Add("Departman Logo",typeof(Image));
            dataTable.Columns.Add("Departman İsmi", typeof(string));
            dataTable.Columns.Add("Departman Detayları", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["department_id"]), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["department_logo"]), table.Rows[i]["department_name"].ToString(), table.Rows[i]["department_details"].ToString());
                }
            }
            departmanlar_datagrid.DataSource = dataTable;
            for (int i = 0; i < departmanlar_datagrid.Columns.Count; i++)
                if (departmanlar_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)departmanlar_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
            komut = new SqlCommand("SELECT company_logo,company_name,company_detail FROM PROGRAM WHERE yonetici_id=1");
             SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Şirket bilgisi alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table.Rows.Count != 0)
            {
                sirket_image.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["company_logo"]);
                sirket_adi.Text = (table.Rows[0]["company_name"]).ToString();
                sirket_detayi.Text = (table.Rows[0]["company_detail"]).ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Forms.sirket_guncelle sirket_Guncelle = new Forms.sirket_guncelle();
            Scripts.Form.FormManager.FORM_AC(sirket_Guncelle, true);
        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            Forms.departman dep = new Forms.departman();
            Scripts.Form.FormManager.FORM_AC(dep, true);
        }
    }
}

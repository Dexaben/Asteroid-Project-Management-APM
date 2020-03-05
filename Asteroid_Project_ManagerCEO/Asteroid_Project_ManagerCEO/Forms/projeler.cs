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

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class projeler : Form
    { 
        public projeler()
        {
            InitializeComponent();
        }
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        private void projeler_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT project_id,project_name,project_image,project_start_date,project_finish_date,(SELECT DEPARTMENTS.department_name FROM DEPARTMENTS WHERE DEPARTMENTS.department_id = PROJECTS.department_id)AS department_name FROM PROJECTS");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Proje bilgileri alınamadı.", Color.Red, 2000);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Proje ID", typeof(int));
            dataTable.Columns.Add("Proje İsmi", typeof(string));
            dataTable.Columns.Add("Proje Resmi", typeof(Image));
            dataTable.Columns.Add("Proje Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Proje Bitiş Tarihi", typeof(string));
            dataTable.Columns.Add("Departman", typeof(string));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["project_image"]), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_start_date"].ToString()), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_finish_date"].ToString()), table.Rows[i]["department_name"].ToString());
            }
            projeler_datagrid.DataSource = dataTable;
            projeler_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < projeler_datagrid.Columns.Count; i++)
                if (projeler_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)projeler_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        private void dataGridView1_DoubleClick_1(object sender, EventArgs e)
        {
            if (projeler_datagrid.SelectedCells.Count > 0)
            {
                proje_detay pd = new proje_detay();
                pd.project_id = Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value);
                pd.ShowDialog();
            }
        }
    }
      
    }

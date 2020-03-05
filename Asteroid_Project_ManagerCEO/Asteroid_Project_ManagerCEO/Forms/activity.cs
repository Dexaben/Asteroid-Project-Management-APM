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
    public partial class activity : Form
    {
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        public activity()
        {
            InitializeComponent();
        }
        private void activity_Load(object sender, EventArgs e)
        {
            listeleme_secim.SelectedItem = listeleme_secim.Items[0];
        }
        void ACTIVITELERI_LISTELE(string command)
        {
            komut = new SqlCommand(command);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Aktiviteler veritabanından alınırken sorun oluştu.", Color.Red, 3000);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Log ID", typeof(int));
            dataTable.Columns.Add("Aktivite", typeof(string));
            dataTable.Columns.Add("Aktivite Tarihi", typeof(string));
            dataTable.Columns.Add("Aktivite Sahibi ID", typeof(string));
            dataTable.Columns.Add("Aktivite Sahibi", typeof(Image));
            /*
             * [0] log_id
             * [1] activity
             * [2] activity_date
             * [3] worker_id
             * [4] worker_name
             * [5] project_id
             * [6] project_name
             * [7] task_id
             * [8] task_name
             */
            if (table!=null)
            {
                for (int i = table.Rows.Count-1; i >= 0; i--)
                {
                    string worker_id = (table.Rows[i]["worker_id"] != DBNull.Value) ?  table.Rows[i]["worker_id"].ToString(): "Kayıt Silinmiş";
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["log_id"]), table.Rows[i]["activity"].ToString(), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["activity_date"].ToString()), worker_id, F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]));
                }
            }
            activity_datagrid.DataSource = dataTable;
            activity_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            for (int i = 0; i < activity_datagrid.Columns.Count; i++)
                if (activity_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)activity_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        /*
         0-Tüm Kayıtlar
         1-Çalışan Kayıtları
         2-Yönetici Kayıtları
         3-Şuanda Aktif Olan Çalışanların Kayıtları
         */
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           switch(((ComboBox)sender).SelectedIndex)
            {
                case 0:
                    ACTIVITELERI_LISTELE("SELECT L.log_id, L.activity, L.activity_date,L.worker_id, (SELECT WORKERS.worker_image FROM WORKERS WHERE(WORKERS.worker_id = L.worker_id)) as worker_image FROM LOGS L");
                    break;
                case 1:
                    ACTIVITELERI_LISTELE("SELECT L.log_id, L.activity, L.activity_date,L.worker_id, (SELECT WORKERS.worker_image FROM WORKERS WHERE(WORKERS.worker_id = L.worker_id)) as worker_image FROM LOGS L WHERE worker_id <> 0");
                    break;
                case 2:
                    ACTIVITELERI_LISTELE("SELECT L.log_id, L.activity, L.activity_date,L.worker_id, (SELECT WORKERS.worker_image FROM WORKERS WHERE(WORKERS.worker_id = L.worker_id)) as worker_image FROM LOGS L WHERE worker_id = 0");
                    break;
                case 3:
                    ACTIVITELERI_LISTELE("SELECT L.log_id, L.activity, L.activity_date,L.worker_id, (SELECT WORKERS.worker_image FROM WORKERS WHERE(WORKERS.worker_id = L.worker_id)) as worker_image FROM LOGS L WHERE worker_id IN (SELECT WORKERS.worker_id FROM WORKERS)");
                    break;
                default:
                    ACTIVITELERI_LISTELE("SELECT L.log_id, L.activity, L.activity_date,L.worker_id, (SELECT WORKERS.worker_image FROM WORKERS WHERE(WORKERS.worker_id = L.worker_id)) as worker_image FROM LOGS L");
                    break;
             }
        }
    }
}

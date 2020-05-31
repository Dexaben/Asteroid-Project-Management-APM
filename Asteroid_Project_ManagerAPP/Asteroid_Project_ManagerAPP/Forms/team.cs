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

namespace Asteroid_Project_ManagerAPP
{
    public partial class team : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
     
        public team()
        {
            InitializeComponent();
        }

        private void team_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT worker_image,WORKERS.worker_id,worker_name FROM WORKERS INNER JOIN PROJECT_WORKERS ON PROJECT_WORKERS.worker_id=WORKERS.worker_id WHERE worker_onay = 1 AND PROJECT_WORKERS.project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Gorevleri_Listele(komut);
            Renk_Duzenleme(proje_takimi);
        }
        void Renk_Duzenleme(Button btn)
        {
            proje_takimi.FlatAppearance.BorderSize = 0;
            sirket_takimi.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.BorderSize = 3;
            Color color = btn.BackColor;
            calisan_liste_datagrid.RowsDefaultCellStyle.BackColor = color;
            calisan_liste_datagrid.RowsDefaultCellStyle.SelectionBackColor = DarkerColor(color);
            calisan_liste_datagrid.AlternatingRowsDefaultCellStyle.BackColor = color;
            calisan_liste_datagrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = DarkerColor(color);
        }
        private Color DarkerColor(Color color, float correctionfactory = 50f)
        {
            const float hundredpercent = 100f;
            return Color.FromArgb((int)(((float)color.R / hundredpercent) * correctionfactory),
                (int)(((float)color.G / hundredpercent) * correctionfactory), (int)(((float)color.B / hundredpercent) * correctionfactory));
        }
        void Gorevleri_Listele(SqlCommand sql)
        {
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(sql, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 3000);
            DataTable table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan ismi", typeof(string));
            for (int i = 0; i < table.Rows.Count; i++)
            {
                dataTable.Rows.Add(Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString());
            }
            calisan_liste_datagrid.DataSource = dataTable;
            for (int i = 0; i < calisan_liste_datagrid.Columns.Count; i++)
                if (calisan_liste_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)calisan_liste_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        private void button2_Click(object sender, EventArgs e) //sirket kadro
        {
            komut = new SqlCommand("SELECT worker_image,worker_id,worker_name FROM WORKERS WHERE worker_onay = 1");
            Gorevleri_Listele(komut);
            Renk_Duzenleme(sirket_takimi);
        }

        private void button1_Click(object sender, EventArgs e) //proje kadro
        {
            komut = new SqlCommand("SELECT worker_image,WORKERS.worker_id,worker_name FROM WORKERS INNER JOIN PROJECT_WORKERS ON PROJECT_WORKERS.worker_id=WORKERS.worker_id WHERE worker_onay = 1 AND PROJECT_WORKERS.project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Gorevleri_Listele(komut);
            Renk_Duzenleme(proje_takimi);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView1_CellMouseEnter((DataGridView)sender, e);
        }

        private void dataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count > 0 && e.RowIndex == dataGridView.SelectedRows[0].Index)
            {
                string[] positions = Scripts.SQL.SqlQueries.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[1].Value));
                string P = dataGridView.SelectedRows[0].Cells[2].Value.ToString() + "'nin pozisyonları \n";
                for (int i = 0; i < positions.Length; i++)
                {
                    P += positions[i] + "\n";
                }
                System.Drawing.Rectangle cellRect = dataGridView.GetCellDisplayRectangle(2, e.RowIndex, false);
                toolTip1.Show(P,
                              this,
                              dataGridView.Location.X + cellRect.X ,
                              dataGridView.Location.Y + cellRect.Y + cellRect.Size.Height );    // Duration: 5 seconds.
            }
        }

        private void dataGridView1_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            toolTip1.Hide(this);
        }
    }
}

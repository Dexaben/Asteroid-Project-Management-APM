using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Asteroid_Project_ManagerAPP
{
    public partial class gorev_liste : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];

        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        public int gorev_listeleme_turu = 0;
        public gorev_liste()
        {
            InitializeComponent();
        }
        /// <summary>
        ///0 ise kendi görevlerini listeler,1 ise projedeki görevleri listeler,2 ise şirketteki görevleri listeler
        /// </summary>
        public void gorev_liste_Load(object sender, EventArgs e)
        {
            switch (gorev_listeleme_turu)
            {
                case 0:
                    button1_Click(this.uzerimdeki_gorevler, null);
                    break;
                case 1:
                    button2_Click(this.projedeki_gorevler, null);
                    break;
                case 2:
                    button3_Click(this.butun_gorevler, null);
                    break;
                default: button1_Click(this.uzerimdeki_gorevler, null); break;
            }
            komut = new SqlCommand("SELECT WORKERS.worker_image,(SELECT PROJECTS.project_image FROM PROJECTS WHERE PROJECTS.project_id = @PROJECT_ID)AS project_image,(SELECT PROGRAM.company_logo FROM PROGRAM)AS company_logo FROM WORKERS WHERE worker_id = @WORKER_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan bilgileri alınırken hata oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table.Rows.Count != 0)
            {
                uzerimdeki_gorev_resim.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0][0]);
                projedeki_gorevler_resim.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0][1]);
                butun_gorevler_resim.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0][2]);
            }

        }
        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if(gorev_liste_datagrid.Rows.Count>0 && gorev_liste_datagrid.SelectedRows.Count>0)
            {
                gorev gorev_Form = new gorev();
                gorev_Form.gorev_id = Convert.ToInt32(gorev_liste_datagrid.SelectedRows[0].Cells[0].Value);
                gorev_Form.ShowDialog();
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT TASKS.task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_details, task_urgency, task_start_date, task_finish_date, task_status FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = @WORKER_ID WHERE(project_id = @PROJECT_ID AND TASK_WORKERS.task_id = TASKS.task_id)");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            Gorevleri_Listele(komut);
            Renk_Duzenleme(uzerimdeki_gorevler);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_details, task_urgency, task_start_date, task_finish_date, task_status FROM TASKS WHERE project_id = @PROJECT_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Gorevleri_Listele(komut);
            Renk_Duzenleme(projedeki_gorevler);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_details, task_urgency, task_start_date, task_finish_date, task_status FROM TASKS");
            Gorevleri_Listele(komut);
            Renk_Duzenleme(butun_gorevler);
        }
        void Renk_Duzenleme(Button btn)
        {
            uzerimdeki_gorevler.FlatAppearance.BorderSize = 0;
            projedeki_gorevler.FlatAppearance.BorderSize = 0;
            butun_gorevler.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.BorderSize = 3;
            Color color = btn.BackColor;
            gorev_liste_datagrid.RowsDefaultCellStyle.BackColor = color;
            gorev_liste_datagrid.RowsDefaultCellStyle.SelectionBackColor = Scripts.Tools.ColorTools.DARKER_COLOR(color);
            gorev_liste_datagrid.AlternatingRowsDefaultCellStyle.BackColor = color;
            gorev_liste_datagrid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Scripts.Tools.ColorTools.DARKER_COLOR(color);
        }
       
        void Gorevleri_Listele(SqlCommand sql)
        {
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(sql, "Hata:Veritabanına bağlanırken hata oluştu.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Görev id", typeof(int));
            dataTable.Columns.Add("Proje ismi", typeof(string));
            dataTable.Columns.Add("Görev", typeof(string));
            dataTable.Columns.Add("Görev Aciliyeti", typeof(string));
            dataTable.Columns.Add("Görev Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Görev Bitiş Tarihi", typeof(string));
            dataTable.Columns.Add("Görev Durumu", typeof(string));
            if (table !=null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_name"], table.Rows[i]["task_urgency"], Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_start_date"].ToString()), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString()), table.Rows[i]["task_status"].ToString());
                }
            }
            gorev_liste_datagrid.DataSource = dataTable;
        }
    }
}

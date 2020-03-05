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

namespace Asteroid_Project_ManagerAPP.Forms
{
   
    public partial class deadline : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        public int gorev_listeleme_turu = 0;
        /// <summary>
        ///0 ise kendi görevlerini listeler,1 ise projedeki görevleri listeler,2 ise şirketteki görevleri listeler
        /// </summary>
        public deadline()
        {
            InitializeComponent();
        }

        public void deadline_Load(object sender, EventArgs e)
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
            SqlCommand komut = new SqlCommand("SELECT WORKERS.worker_image,(SELECT PROJECTS.project_image FROM PROJECTS WHERE PROJECTS.project_id = @PROJECT_ID)AS project_image,(SELECT PROGRAM.company_logo FROM PROGRAM)AS company_logo FROM WORKERS WHERE worker_id = @WORKER_ID");
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri alınırken hata oluştu.", Color.Red, 3000);
            if (table.Rows.Count != 0)
            {
                uzerimdeki_gorevler_resim.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0][0]);
                projedeki_gorevler_resim.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0][1]);
                butun_gorevler_resim.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0][2]);
            }
        }

        private void button1_Click(object sender, EventArgs e) //ÇALIŞAN
        {
            SqlCommand komut_Planlanan = new SqlCommand("SELECT TASKS.task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_urgency, task_start_date, task_finish_date, task_status FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = @WORKER_ID WHERE(project_id = @PROJECT_ID AND TASK_WORKERS.task_id = TASKS.task_id AND TASKS.task_status='Görev Çalışana Verildi.')");
            komut_Planlanan.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut_Planlanan.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            SqlCommand komut_DevamEden = new SqlCommand("SELECT TASKS.task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_urgency, task_finish_date, task_status FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = @WORKER_ID WHERE(project_id = @PROJECT_ID AND TASK_WORKERS.task_id = TASKS.task_id AND TASKS.task_status<>'Görev Çalışana Verildi.' AND TASKS.task_status<>'Görev Tamamlandı')");
            komut_DevamEden.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut_DevamEden.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            SqlCommand komut_Tamamlanan = new SqlCommand("SELECT TASKS.task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_status FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = @WORKER_ID WHERE(project_id = @PROJECT_ID AND TASK_WORKERS.task_id = TASKS.task_id AND TASKS.task_status='Görev Tamamlandı')");
            komut_Tamamlanan.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            komut_Tamamlanan.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            Gorevleri_Listele(komut_Planlanan, komut_DevamEden, komut_Tamamlanan);
            Renk_Duzenleme(uzerimdeki_gorevler);
        }

        private void button2_Click(object sender, EventArgs e) //PROJE
        {
            SqlCommand komut_Planlanan = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_urgency, task_start_date, task_finish_date, task_status FROM TASKS WHERE(project_id = @PROJECT_ID AND TASKS.task_status='Görev Çalışana Verildi.')");
            komut_Planlanan.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            SqlCommand komut_DevamEden = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name,  task_urgency,  task_finish_date, task_status FROM TASKS WHERE(project_id = @PROJECT_ID AND TASKS.task_status<>'Görev Çalışana Verildi.' AND TASKS.task_status<>'Görev Tamamlandı')");
            komut_DevamEden.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            SqlCommand komut_Tamamlanan = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name, task_status FROM TASKS WHERE(project_id = @PROJECT_ID AND TASKS.task_status='Görev Tamamlandı')");
            komut_Tamamlanan.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Gorevleri_Listele(komut_Planlanan, komut_DevamEden, komut_Tamamlanan);
            Renk_Duzenleme(projedeki_gorevler);
        }

        private void button3_Click(object sender, EventArgs e) //ŞİRKET
        {
            SqlCommand komut_Planlanan = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name,  task_urgency, task_start_date, task_finish_date, task_status FROM TASKS WHERE(TASKS.task_status='Görev Çalışana Verildi.')");

            SqlCommand komut_DevamEden = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name,  task_urgency,task_finish_date, task_status FROM TASKS WHERE(TASKS.task_status<>'Görev Çalışana Verildi.' AND TASKS.task_status<>'Görev Tamamlandı')");

            SqlCommand komut_Tamamlanan = new SqlCommand("SELECT task_id, (SELECT project_name FROM PROJECTS WHERE(TASKS.project_id = PROJECTS.project_id))AS project_name, task_name,  task_status FROM TASKS WHERE(TASKS.task_status='Görev Tamamlandı')");

            Gorevleri_Listele(komut_Planlanan, komut_DevamEden, komut_Tamamlanan);
            Renk_Duzenleme(butun_gorevler);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (((DataGridView)sender).Rows.Count > 0 && ((DataGridView)sender).SelectedRows.Count > 0)
            {
                gorev gorev_Form = new gorev();
                gorev_Form.gorev_id = Convert.ToInt32(((DataGridView)sender).SelectedRows[0].Cells[0].Value);
                gorev_Form.ShowDialog();
            }
        }
        void Renk_Duzenleme(Button btn)
        {
            uzerimdeki_gorevler.FlatAppearance.BorderSize = 0;
            projedeki_gorevler.FlatAppearance.BorderSize = 0;
            butun_gorevler.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.BorderSize = 3;
            Color color = btn.BackColor;
            groupBox1.BackColor = F.DARKER_COLOR(color,70);
            groupBox2.BackColor = color;
            groupBox3.BackColor = F.LIGHTER_COLOR(color);
            planlanan_gorev.RowsDefaultCellStyle.BackColor = groupBox1.BackColor;
            planlanan_gorev.RowsDefaultCellStyle.SelectionBackColor = F.DARKER_COLOR(groupBox1.BackColor,90);
            planlanan_gorev.AlternatingRowsDefaultCellStyle.BackColor = groupBox1.BackColor;
            planlanan_gorev.AlternatingRowsDefaultCellStyle.SelectionBackColor = F.DARKER_COLOR(groupBox1.BackColor,90);

            devam_eden_gorev.RowsDefaultCellStyle.BackColor = groupBox2.BackColor;
            devam_eden_gorev.RowsDefaultCellStyle.SelectionBackColor = F.DARKER_COLOR(groupBox2.BackColor,90);
            devam_eden_gorev.AlternatingRowsDefaultCellStyle.BackColor = groupBox2.BackColor;
            devam_eden_gorev.AlternatingRowsDefaultCellStyle.SelectionBackColor = F.DARKER_COLOR(groupBox2.BackColor,90);

            tamamlanan_gorev.RowsDefaultCellStyle.BackColor = groupBox3.BackColor;
            tamamlanan_gorev.RowsDefaultCellStyle.SelectionBackColor = F.DARKER_COLOR(groupBox3.BackColor,90);
            tamamlanan_gorev.AlternatingRowsDefaultCellStyle.BackColor = groupBox3.BackColor;
            tamamlanan_gorev.AlternatingRowsDefaultCellStyle.SelectionBackColor = F.DARKER_COLOR(groupBox3.BackColor,90);
        }
        void Gorevleri_Listele(SqlCommand sql_Planlanan,SqlCommand sql_DevamEden,SqlCommand sql_Tamamlanan)
        {
            //PLANLANAN
            table = F.SQL_SELECT_DATATABLE(sql_Planlanan, "Hata:Veritabanına bağlanırken hata oluştu.", Color.Red, 3000);
            DataTable dataTable_PLANLANAN = new DataTable();
            dataTable_PLANLANAN.Columns.Add("Görev id", typeof(int));
            dataTable_PLANLANAN.Columns.Add("Proje ismi", typeof(string));
            dataTable_PLANLANAN.Columns.Add("Görev", typeof(string));
            dataTable_PLANLANAN.Columns.Add("Görev Aciliyeti", typeof(string));
            dataTable_PLANLANAN.Columns.Add("Görev Başlangıç Tarihi", typeof(string));
            dataTable_PLANLANAN.Columns.Add("Görev Bitiş Tarihi", typeof(string));
            dataTable_PLANLANAN.Columns.Add("Görev Durumu", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable_PLANLANAN.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_name"], table.Rows[i]["task_urgency"], F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_start_date"].ToString()), F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString()), table.Rows[i]["task_status"].ToString());
                }
            }
            planlanan_gorev.DataSource = dataTable_PLANLANAN;
            //DEVAM EDEN
            table = F.SQL_SELECT_DATATABLE(sql_DevamEden, "Hata:Veritabanına bağlanırken hata oluştu.", Color.Red, 3000);
            DataTable dataTable_DEVAMEDEN = new DataTable();
            dataTable_DEVAMEDEN.Columns.Add("Görev id", typeof(int));
            dataTable_DEVAMEDEN.Columns.Add("Proje ismi", typeof(string));
            dataTable_DEVAMEDEN.Columns.Add("Görev", typeof(string));
            dataTable_DEVAMEDEN.Columns.Add("Görev Aciliyeti", typeof(string));
            dataTable_DEVAMEDEN.Columns.Add("Görev Bitiş Tarihi", typeof(string));
            dataTable_DEVAMEDEN.Columns.Add("Görev Durumu", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable_DEVAMEDEN.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_name"], table.Rows[i]["task_urgency"], F.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["task_finish_date"].ToString()), table.Rows[i]["task_status"].ToString());
                }
            }
            devam_eden_gorev.DataSource = dataTable_DEVAMEDEN;
            //TAMAMLANAN
            table = F.SQL_SELECT_DATATABLE(sql_Tamamlanan, "Hata:Veritabanına bağlanırken hata oluştu.", Color.Red, 3000);
            DataTable dataTable_TAMAMLANAN = new DataTable();
            dataTable_TAMAMLANAN.Columns.Add("Görev id", typeof(int));
            dataTable_TAMAMLANAN.Columns.Add("Proje ismi", typeof(string));
            dataTable_TAMAMLANAN.Columns.Add("Görev", typeof(string));
            dataTable_TAMAMLANAN.Columns.Add("Görev Durumu", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable_TAMAMLANAN.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_name"],  table.Rows[i]["task_status"].ToString());
                }
            }
            tamamlanan_gorev.DataSource = dataTable_TAMAMLANAN;
        }
    }
}

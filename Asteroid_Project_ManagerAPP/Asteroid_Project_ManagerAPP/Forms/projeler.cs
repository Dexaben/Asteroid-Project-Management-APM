
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    public partial class projeler : Form
    {
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        DataTable table = new DataTable();
        public projeler()
        {
            InitializeComponent();
            this.AcceptButton = proje_gir;
        }

        private void projeler_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT project_id,project_name,project_image,project_start_date,project_finish_date,(SELECT worker_name FROM WORKERS WHERE worker_id= project_manager)AS project_manager,(SELECT department_name FROM DEPARTMENTS WHERE PROJECTS.department_id=DEPARTMENTS.department_id)AS project_department FROM PROJECTS");
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Projelerin bilgileri alınırken hata oluştu.", Color.Red, 3000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Proje ID", typeof(int));
            dataTable.Columns.Add("Proje İsmi", typeof(string));
            dataTable.Columns.Add("Proje Resmi", typeof(Image));
            dataTable.Columns.Add("Proje Başlangıç Tarihi", typeof(string));
            dataTable.Columns.Add("Proje Bitiş Tarihi", typeof(string));
            dataTable.Columns.Add("Proje Yöneticisi", typeof(string));
            dataTable.Columns.Add("Yüklenici Departman", typeof(string));
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["project_id"]), table.Rows[i]["project_name"].ToString(), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["project_image"]), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_start_date"].ToString()), Scripts.Tools.DateFunctions.Tarih_Converter_DAY_HOUR_MINUTE(table.Rows[i]["project_finish_date"].ToString()), table.Rows[i]["project_manager"].ToString(), table.Rows[i]["project_department"].ToString());
                }
            }
            projeler_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            projeler_datagrid.DataSource = dataTable;
            for (int i = 0; i < projeler_datagrid.Columns.Count; i++)
                if (projeler_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)projeler_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (Scripts.SQL.SqlQueries.WORKER_HAS_ROLE_BY_WORKER_ID_AND_WORKER_ROLE(a.worker_id, "Proje Yöneticisi"))
            {
                Scripts.Form.FormManager.FORM_AC(new yonetimForms.projeEkleme(), true);
            }
            else Scripts.Form.Status.STATUS_LABEL("Proje açmak için 'Proje Yöneticisi' yetkisi gerekli.", Color.White, 2000);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (projeler_datagrid.SelectedRows.Count == 1)
            {
                komut = new SqlCommand("SELECT project_manager FROM PROJECTS WHERE project_id = @PROJECT_ID");
                komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value));
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje çalışanlarının bilgilerine ulaşılamadı.", Color.Red, 3000);

                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if (table != null)
                { 
                    if(Convert.ToInt32(table.Rows[0]["project_manager"]) == a.worker_id)
                    {
                        a.project_id = Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value);
                        a.ANAFORM_BILGILER_GUNCELLE();
                        Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, $"'Proje Yöneticisi' {Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {Scripts.SQL.SqlQueries.PROJECT_NAME_BY_PROJECT_ID(Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value))} projesine giriş yaptı.Proje ID:{Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value)}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                        Scripts.Form.FormManager.FORM_AC(new projem(), true);
                        return;
                    }
                }
                komut = new SqlCommand("SELECT (SELECT PROJECTS.project_finish_date FROM PROJECTS WHERE PROJECTS.project_id=@PROJECT_ID)AS project_finish_date,(SELECT PROJECTS.project_start_date FROM PROJECTS WHERE PROJECTS.project_id=@PROJECT_ID)AS project_start_date FROM PROJECT_WORKERS WHERE worker_id = @WORKER_ID AND PROJECT_WORKERS.project_id = @PROJECT_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                komut.Parameters.AddWithValue("@PROJECT_ID", Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value));
               sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje çalışanlarının bilgilerine ulaşılamadı.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
                if(table != null)
                {
                        if (table.Rows.Count != 0)
                        {
                            DateTime dtf = Scripts.Tools.DateFunctions.DATETIME_CONVERTER(table.Rows[0][0].ToString());
                            DateTime dts = Scripts.Tools.DateFunctions.DATETIME_CONVERTER(table.Rows[0][1].ToString());
                            if (dtf >= Scripts.SQL.SqlQueries.GET_SERVER_DATE() && dts < Scripts.SQL.SqlQueries.GET_SERVER_DATE())
                            {
                                a.project_id = Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value);
                                a.ANAFORM_BILGILER_GUNCELLE();
                            Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, $"{Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {Scripts.SQL.SqlQueries.PROJECT_NAME_BY_PROJECT_ID(Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value))} projesine giriş yaptı.Proje ID:{Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value)}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                                Scripts.Form.FormManager.FORM_AC(new projem(), true);
                            }
                            else MessageBox.Show("Projenin zamanı dolmuş veya Proje başlamış durumda değil.");
                        }
                        else Scripts.Form.Status.STATUS_LABEL("Bu projenin katılımcısı değilsiniz.", Color.White, 4000);
                }
                else Scripts.Form.Status.STATUS_LABEL("Bu projenin katılımcısı değilsiniz.", Color.White, 4000);
            }
            else Scripts.Form.Status.STATUS_LABEL("Girilebilecek bir proje seçmediniz.", Color.White, 3000);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (projeler_datagrid.SelectedRows.Count == 1)
            {
                if (Scripts.SQL.SqlQueries.PROJE_YONETICISI_KONTROL(a.worker_id,Convert.ToInt32(projeler_datagrid.SelectedRows[0].Cells[0].Value)))
                {
                    SqlCommand komut1 = new SqlCommand("DELETE W FROM TASK_WORKERS W RIGHT JOIN TASKS T ON T.task_id = W.task_id WHERE project_id = @project_id");
                    komut1.Parameters.AddWithValue("@project_id", projeler_datagrid.SelectedRows[0].Cells[0].Value);
                    SqlCommand komut2 = new SqlCommand("DELETE T FROM TASK_WORKERS W RIGHT JOIN TASKS T ON T.task_id = W.task_id WHERE project_id = @project_id");
                    komut2.Parameters.AddWithValue("@project_id", projeler_datagrid.SelectedRows[0].Cells[0].Value);
                    SqlCommand komut3 = new SqlCommand("DELETE FROM PROJECTS WHERE project_id=@project_id");
                    komut3.Parameters.AddWithValue("@project_id", projeler_datagrid.SelectedRows[0].Cells[0].Value);
                    SqlCommand komut4 = new SqlCommand("DELETE FROM PROJECT_WORKERS WHERE project_id=@project_id");
                    komut4.Parameters.AddWithValue("@project_id", projeler_datagrid.SelectedRows[0].Cells[0].Value);
                    Scripts.SQL.SQL_COMMAND sqlCommand1 = new Scripts.SQL.SQL_COMMAND(komut1, "Hata:Görev çalışanlarının bilgileri silinirken hata oluştu.", Color.Red, 2000);
                    Scripts.SQL.SQL_COMMAND sqlCommand2 = new Scripts.SQL.SQL_COMMAND(komut2, "Hata:Görevler silinemedi.", Color.Red, 2000);
                    Scripts.SQL.SQL_COMMAND sqlCommand3 = new Scripts.SQL.SQL_COMMAND(komut3, "Hata:Proje bilgileri silinemedi.", Color.Red, 2000);
                    Scripts.SQL.SQL_COMMAND sqlCommand4 = new Scripts.SQL.SQL_COMMAND(komut4, "Hata:Proje bilgileri silinemedi.", Color.Red, 2000);

                    if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand1) && Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand2) && Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand3) && Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand4))
                    {
                        Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id) + " adlı çalışan " +projeler_datagrid.SelectedRows[0].Cells[1].Value.ToString()+ " projesini sildi.", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                        projeler_Load(this, null);
                    }
                }
                else Scripts.Form.Status.STATUS_LABEL("Proje silmek için yeterli yetkiye sahip değilsiniz.", Color.White, 2000);
            }
        }
    }
}

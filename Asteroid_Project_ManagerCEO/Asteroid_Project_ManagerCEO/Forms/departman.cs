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
    public partial class departman : Form
    {
        SqlCommand komut = new SqlCommand();
        
        DataTable table = new DataTable();
        public departman()
        {
            InitializeComponent();
            this.AcceptButton = departman_duzenle;
        }
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        private void button1_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new departman_ekle(), true);
        }
        private void departman_Load(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT department_id,department_logo,department_name,department_details FROM DEPARTMENTS");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman verileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Departman ID", typeof(int));
            dataTable.Columns.Add("Departman Logo", typeof(Image));
            dataTable.Columns.Add("Departman İsmi", typeof(string));
            dataTable.Columns.Add("Departman Detayları", typeof(string));
            if (table.Rows.Count !=0)
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
            row_selected(this, null);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (departmanlar_datagrid.Rows.Count > 0)
            {
                int i = Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value);
                departman_guncelle departman_Guncelle = new departman_guncelle();
                departman_Guncelle.department_ID = i;
                Scripts.Form.FormManager.FORM_AC(departman_Guncelle, true);
            }
            else Scripts.Form.Status.STATUS_LABEL("Hata: Departman verisi bulunamadı.", Color.Purple, 2000);
        }
        int DEPARTMENT_PROJECTS_COUNT(int department_id) //DEPARTMAN PROJELERİ
        {
            komut = new SqlCommand("SELECT COUNT(project_id)AS project_id FROM PROJECTS WHERE department_id = @DEPARTMENT_ID");
            komut.Parameters.AddWithValue("@DEPARTMENT_ID", department_id);
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table != null)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }
        int PROJECT_WORKERS_COUNT(int department_id) //PROJELERDE ÇALIŞAN KİŞİ
        {
            komut = new SqlCommand("SELECT COUNT(project_id)AS project_id FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID)");
            komut.Parameters.AddWithValue("@DEPARTMENT_ID", department_id);
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table != null)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }
        int PROJECT_TASKS_COUNT(int department_id) //PROJELERDE BULUNAN GÖREV SAYISI
        {
            komut = new SqlCommand("SELECT COUNT(task_id)AS task_id FROM TASKS WHERE TASKS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID)");
            komut.Parameters.AddWithValue("@DEPARTMENT_ID", department_id);
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table != null)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (departmanlar_datagrid.Rows.Count > 0)
            {
                string message = "Bu departmanı gerçekten SİLMEK istiyormusunuz?";
                if (DEPARTMENT_PROJECTS_COUNT(Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)) > 0)
                {
                    message += " Bu departmanın üstlendiği " + DEPARTMENT_PROJECTS_COUNT(Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)) + " adet proje kaydı bulunuyor.";
                    if (PROJECT_WORKERS_COUNT(Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)) > 0)
                    {
                        message += " Bu projelerde " + PROJECT_WORKERS_COUNT(Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)).ToString() + " adet çalışan görev yapmakta.";
                        if (PROJECT_TASKS_COUNT(Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)) > 0)
                        {
                            message += " Bu projelerde " + PROJECT_TASKS_COUNT(Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)).ToString() + " adet görev bulunuyor.";
                        }
                    }
                }
                
                DialogResult dialog = MessageBox.Show(message, "ÇIKIŞ", MessageBoxButtons.YesNo);
                if (dialog == DialogResult.Yes)
                {
                    komut = new SqlCommand("DELETE FROM TASK_IMAGES WHERE TASK_IMAGES.task_id IN (SELECT TASKS.task_id FROM TASKS WHERE TASKS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID))");
                    komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                    Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi silinemedi.", Color.Red, 3000);
                    if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                    {
                        komut = new SqlCommand("DELETE FROM TASK_WORKERS WHERE TASK_WORKERS.task_id IN (SELECT TASKS.task_id FROM TASKS WHERE TASKS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID))");
                        komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                       SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi silinemedi.", Color.Red, 3000);
                        if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                        {
                            komut = new SqlCommand("DELETE FROM TASKS WHERE TASKS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID)");
                            komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                            SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi silinemedi.", Color.Red, 3000);
                            if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                            {
                                komut = new SqlCommand("DELETE FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.project_id IN (SELECT PROJECTS.project_id FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID)");
                                komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                                 SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi silinemedi.", Color.Red, 3000);
                                if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                                {
                                    komut = new SqlCommand("DELETE FROM PROJECTS WHERE PROJECTS.department_id = @DEPARTMENT_ID");
                                    komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                                    SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi silinemedi.", Color.Red, 3000);
                                    if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                                    {
                                        komut = new SqlCommand("DELETE FROM DEPARTMENTS WHERE department_id=@DEPARTMENT_ID");
                                        komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                                        SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgisi silinemedi.", Color.Red, 3000);
                                        if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(SqlCommand))
                                        {  Scripts.Tools.LogTools.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından (ID-{Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value)}) {departmanlar_datagrid.SelectedRows[0].Cells[2].Value} adlı departman SİLİNDİ.Bu silme işlemi ile departman içerisindeki projeler proje çalışanları görevler ve onlara ait verilerde SİLİNDİ.",Scripts.SQL.SqlQueries.GET_SERVER_DATE()); departman_Load(this, null); }
                                        if (departmanlar_datagrid.Rows.Count <= 0) groupBox1.Visible = false;
                                        else departmanlar_datagrid.Rows[0].Selected = true;
                                    }
                                }
                            }
                        }
                    }
                     
                }
             
            }
            else Scripts.Form.Status.STATUS_LABEL("Hata: Departman verisi bulunamadı.", Color.Purple, 2000);
        }
        private void row_selected(object sender, DataGridViewCellEventArgs e)
        {
            if (departmanlar_datagrid.Rows.Count > 0)
            {
                groupBox1.Visible = true;
                komut = new SqlCommand("SELECT department_name,department_logo,department_details FROM DEPARTMENTS WHERE department_id=@wrk_id");
                komut.Parameters.AddWithValue("@wrk_id", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman verisi alınamadı.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
                if(table !=null)
                {
                    departman_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["department_logo"]);
                    departman_ismi.Text = table.Rows[0]["department_name"].ToString();
                    groupBox1.Text = "Departman Bilgileri (ID-"+ Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value) + ")";
                    string department_details = table.Rows[0]["department_details"].ToString()+Environment.NewLine+Environment.NewLine+"Departman Projeleri"+Environment.NewLine;
                    komut = new SqlCommand("SELECT project_id,project_name FROM PROJECTS WHERE department_id=@DEPARTMENT_ID");
                    komut.Parameters.AddWithValue("@DEPARTMENT_ID", Convert.ToInt32(departmanlar_datagrid.SelectedRows[0].Cells[0].Value));
                  SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman verisi alınamadı.", Color.Red, 3000);
                    table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
                    if (table != null)
                    {

                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            department_details += $"(ID-{table.Rows[i]["project_id"].ToString()}) {table.Rows[i]["project_name"].ToString()}"+Environment.NewLine;
                        }
                    }
                    departman_detay.Text = department_details;
                    departman_id.Text = "Departman Bilgiler (" + departmanlar_datagrid.SelectedRows[0].Cells[0].Value + ")";
                }
            }
            else
            {
                Scripts.Form.Status.STATUS_LABEL("Hata:Departman verisi bulunamadı.", Color.Purple, 2000);
                groupBox1.Visible = false;
            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(departman_resmi.Image);
        }
    }
}

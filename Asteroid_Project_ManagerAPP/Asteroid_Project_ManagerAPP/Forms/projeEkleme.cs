using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Data.SqlTypes;

namespace Asteroid_Project_ManagerAPP.yonetimForms
{
    public partial class projeEkleme : Form
    {
        public projeEkleme()
        {
            InitializeComponent();
            this.AcceptButton = proje_olustur;
        }
        DataTable table = new DataTable();
        SqlCommand komut = new SqlCommand();
        string fileName = null;
        Dictionary<string, int> departmentDict;
        Dictionary<string, int> managerDict;
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        void kaydet()
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources\\iconlar");
            Image imag = Image.FromFile(filePath + @"\default_project_image.jpg");
            if (fileName != null)
            {
                imag = Image.FromFile(fileName);
            }
            komut = new SqlCommand("INSERT INTO PROJECTS(project_name,project_detail,project_start_date,project_finish_date,project_image,project_status,project_manager,department_id) VALUES(@project_name,@project_detail,@project_start_date,@project_finish_date,@project_image,@project_status,@project_manager,@department_id)");
            komut.Parameters.AddWithValue("@project_name", proje_adi.Text);
            komut.Parameters.AddWithValue("@project_detail", proje_detayi.Text);
            SqlDateTime time_Start = proje_baslangic_tarihi.Value;
            SqlDateTime time_Finish = proje_bitis_tarihi.Value;
            komut.Parameters.AddWithValue("@project_start_date", time_Start);
            komut.Parameters.AddWithValue("@project_finish_date", time_Finish);
            komut.Parameters.AddWithValue("@project_status", "Planlanma Aşamasında");
            komut.Parameters.AddWithValue("@project_manager", managerDict[proje_yoneticileri.Text]);
            komut.Parameters.AddWithValue("@department_id", departmentDict[departmanlar.Text]);
            komut.Parameters.Add("@project_image", SqlDbType.Image, 0).Value = Scripts.Tools.ImageTools.CONVERT_IMAGE_TO_BYTE_ARRAY(imag, System.Drawing.Imaging.ImageFormat.Jpeg);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje veritabanına eklenemedi.", Color.Red, 3000);
            if (Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
            {
                string workers = "";
                for (int i = 0; i < projeye_eklenenler_datagrid.Rows.Count; i++)
                {
                    workers += $" {Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(Convert.ToInt32(projeye_eklenenler_datagrid.Rows[i].Cells[0].Value))} ,";
                    komut = new SqlCommand("INSERT INTO PROJECT_WORKERS(worker_id,project_id) VALUES(@WORKER_ID,(SELECT TOP 1 project_id FROM PROJECTS WHERE project_name = @PROJECT_NAME))");
                    komut.Parameters.AddWithValue("@WORKER_ID", Convert.ToInt32(projeye_eklenenler_datagrid.Rows[i].Cells[0].Value));
                    komut.Parameters.AddWithValue("@PROJECT_NAME", proje_adi.Text);
                    sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Proje çalışan bilgisi eklenemedi.", Color.Red, 3000);

                    if (!Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand))
                    { return; }
                }
                Scripts.Tools.LogTools.LOG_ENTER(a.worker_id, $"{Scripts.SQL.SqlQueries.WORKER_NAME_BY_WORKER_ID(a.worker_id)} adlı çalışan {proje_adi.Text} projesini oluşturdu.Proje Yöneticisi:{proje_yoneticileri.Text},Proje Başlangıç Tarihi:{time_Start},Proje Bitiş Tarihi:{time_Finish},Proje Departmanı:{departmanlar.Text},Proje Çalışanları:{workers}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
                Scripts.Form.FormManager.FORM_AC(new projeler(), true);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if(fileName != null)
            proje_resmi.Image = Image.FromFile(fileName);
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (proje_adi.Text.Length > 0 && proje_detayi.Text.Length > 0)
            {
                kaydet();
            }
            else label6.Text = "Proje ismi ve Proje detayı boş bırakılamaz";
      
        }

        private void projeEkleme_Load(object sender, EventArgs e)
        {
            label6.Text = "";
            label6.ForeColor = Color.Red;
            proje_baslangic_tarihi.MinDate = Scripts.SQL.SqlQueries.GET_SERVER_DATE();
            proje_bitis_tarihi.MinDate = Scripts.SQL.SqlQueries.GET_SERVER_DATE().AddDays(1);
            proje_baslangic_tarihi.Value = proje_baslangic_tarihi.MinDate;
            proje_bitis_tarihi.Value = proje_bitis_tarihi.MinDate;
            komut = new SqlCommand("SELECT worker_id,worker_name,worker_image, (SELECT  COUNT(DISTINCT project_id) FROM PROJECT_WORKERS WHERE PROJECT_WORKERS.worker_id = WORKERS.worker_id) as ProjeSayisi,(SELECT COUNT(TASKS.task_id)FROM TASKS INNER JOIN TASK_WORKERS ON TASK_WORKERS.worker_id = WORKERS.worker_id WHERE TASK_WORKERS.task_id = TASKS.task_id AND TASKS.task_status = 'Görev Tamamlandı') AS TamamlanmisGorevSay  FROM WORKERS where worker_onay = 1");
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan bilgileri alınırken hata oluştu.", Color.Red, 3000);

            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan İsmi", typeof(string));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable.Columns.Add("Çalışan Şuanda Kaç Projede?", typeof(int));
            dataTable.Columns.Add("Çalışanın Tamamlanmamış Görev Sayısı", typeof(int));
            if (table != null)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString(), Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), Convert.ToInt32(table.Rows[i]["ProjeSayisi"]), Convert.ToInt32(table.Rows[i]["TamamlanmisGorevSay"]));
                }
            }
            //datagridview2 columns add
            DataGridViewImageColumn dataGridViewImageColumn = new DataGridViewImageColumn();
            dataGridViewImageColumn.HeaderText = "Çalışan Resmi";
            dataGridViewImageColumn.Name = "worker_image";
            projeye_eklenenler_datagrid.Columns.Add("worker_id","Çalışan ID");
            projeye_eklenenler_datagrid.Columns.Add("worker_name", "Çalışan İsmi");
            projeye_eklenenler_datagrid.Columns.Add(dataGridViewImageColumn);
            projeye_eklenenler_datagrid.Columns[0].ValueType = typeof(int);
            projeye_eklenenler_datagrid.Columns[1].ValueType = typeof(string);
            projeye_eklenenler_datagrid.Columns[2].ValueType = typeof(Image);
            ((DataGridViewImageColumn)projeye_eklenenler_datagrid.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            projeye_eklenenler_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            calisanlar_datagrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            calisanlar_datagrid.DataSource = dataTable;
            for (int i = 0; i < calisanlar_datagrid.Columns.Count; i++)
                if (calisanlar_datagrid.Columns[i] is DataGridViewImageColumn)
                {
                    ((DataGridViewImageColumn)calisanlar_datagrid.Columns[i]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                    break;
                }
            komut = new SqlCommand("SELECT worker_id,worker_name FROM WORKERS");
 sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            managerDict = new Dictionary<string, int>();
            if (table.Rows.Count != 0)
            {
                for(int i =0;i<table.Rows.Count;i++)
                {
                    if (Scripts.SQL.SqlQueries.WORKER_HAS_ROLE_BY_WORKER_ID_AND_WORKER_ROLE(Convert.ToInt32(table.Rows[i]["worker_id"]), "Proje Yöneticisi"))
                    {
                        managerDict.Add(table.Rows[i]["worker_name"].ToString(), Convert.ToInt32(table.Rows[i]["worker_id"]));
                      
                    }
                    proje_yoneticileri.Items.Clear();
                    foreach (string Deger in managerDict.Keys)
                    {
                        proje_yoneticileri.Items.Add(Deger);
                    }
                    if (proje_yoneticileri.Items.Count > 0)
                    {
                        proje_yoneticileri.Text = proje_yoneticileri.Items[0].ToString();
                    }
                    else
                    {
                        Scripts.Form.Status.STATUS_LABEL("Proje ekleyebilmeniz için bir'Proje Yöneticisi'ne ihtiyacınız var", Color.PaleVioletRed, 4000);
                       
                    }
                }
            }
            if (proje_yoneticileri.Items.Count > 0)
            {
                proje_yoneticileri.Text = proje_yoneticileri.Items[0].ToString();
            }
            komut = new SqlCommand("SELECT department_id,department_name FROM DEPARTMENTS");
           sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Departman bilgileri alınırken hata oluştu.", Color.Red, 2000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            departmentDict = new Dictionary<string, int>();
            if(table.Rows.Count !=0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    departmentDict.Add(table.Rows[i]["department_name"].ToString(), Convert.ToInt32(table.Rows[i]["department_id"]));
                }
                foreach (string Deger in departmentDict.Keys)
                {
                    departmanlar.Items.Add(Deger);
                }
            }
            if (departmanlar.Items.Count > 0)
            {
                departmanlar.Text = departmanlar.Items[0].ToString();
            }
            else
            {
                Scripts.Form.Status.STATUS_LABEL("Proje Ekleyebilmeniz için en az bir DEPARTMAN'a ihtiyacınız var", Color.PaleVioletRed, 4000);
                Scripts.Form.FormManager.FORM_AC(new AnaPanel(), true);
            }
        }
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if(proje_bitis_tarihi.Value <= proje_baslangic_tarihi.Value)
            {
                Scripts.Form.Status.STATUS_LABEL("Hata: Proje bitiş tarihi, başlangıç tarihinin gerisinde olamaz.", Color.White, 2000);
                proje_bitis_tarihi.Value = proje_baslangic_tarihi.Value.AddDays(1);
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (proje_bitis_tarihi.Value <= proje_baslangic_tarihi.Value)
            {
               Scripts.Form.Status.STATUS_LABEL("Hata: Proje bitiş tarihi, başlangıç tarihinin gerisinde olamaz.", Color.White, 2000);
                proje_bitis_tarihi.Value = proje_baslangic_tarihi.Value.AddDays(1);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           Scripts.Form.FormManager.FORM_AC(new projeler(), true);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            for (int i = 0; i < projeye_eklenenler_datagrid.Rows.Count; i++)
            {
                if (Convert.ToInt32(projeye_eklenenler_datagrid.Rows[i].Cells[0].Value) == Convert.ToInt32(calisanlar_datagrid.SelectedRows[0].Cells[0].Value))
                {
                    Scripts.Form.Status.STATUS_LABEL("Bu çalışan zaten ekli.", Color.BlueViolet, 2000);
                    return;

                }
            }
            if(Convert.ToInt32(calisanlar_datagrid.SelectedRows[0].Cells[3].Value) > 0)
            {
                if (MessageBox.Show(calisanlar_datagrid.SelectedRows[0].Cells[1].Value+" şuanda "+calisanlar_datagrid.SelectedRows[0].Cells[3].Value+" projede yer alıyor. Bu çalışanı yeni bir projeye eklemek istiyormusunuz?","Uyarı", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    projeye_eklenenler_datagrid.Rows.Add(calisanlar_datagrid.SelectedRows[0].Cells[0].Value, calisanlar_datagrid.SelectedRows[0].Cells[1].Value.ToString(), calisanlar_datagrid.SelectedRows[0].Cells[2].Value);
                    return;
                }
            }
            else projeye_eklenenler_datagrid.Rows.Add(calisanlar_datagrid.SelectedRows[0].Cells[0].Value, calisanlar_datagrid.SelectedRows[0].Cells[1].Value.ToString(), calisanlar_datagrid.SelectedRows[0].Cells[2].Value);
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            if(projeye_eklenenler_datagrid.Rows.Count>0 && projeye_eklenenler_datagrid.SelectedRows.Count>0)
            projeye_eklenenler_datagrid.Rows.Remove(projeye_eklenenler_datagrid.SelectedRows[0]);
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            Scripts.Tools.ImageTools.OpenImage(proje_resmi.Image);
        }

        private void cell_click(object sender, DataGridViewCellEventArgs e)
        {
            this.cell_mouse_enter((DataGridView)sender, e);
        }

        private void cell_mouse_enter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;
            if (dataGridView.Rows.Count > 0 && dataGridView.SelectedRows.Count > 0 && e.RowIndex == dataGridView.SelectedRows[0].Index)
            {
                string[] positions = Scripts.SQL.SqlQueries.WORKER_ROLE_CALL_BY_ID(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                string P = dataGridView.SelectedRows[0].Cells[1].Value.ToString() + "'nin pozisyonları \n";
                for (int i = 0; i < positions.Length; i++)
                {
                    P += positions[i] + "\n";
                }
                Rectangle cellRect = dataGridView.GetCellDisplayRectangle(1, e.RowIndex, false);
                toolTip1.Show(P,
                              this,
                              dataGridView.Location.X + cellRect.X,
                              dataGridView.Location.Y + cellRect.Y + cellRect.Size.Height);    // Duration: 5 seconds.
            }

        }

        private void cell_mouse_leave(object sender, DataGridViewCellEventArgs e)
        {
            toolTip1.Hide(this);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var projectPath = System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources\\iconlar");
            proje_resmi.Image = Image.FromFile(filePath + @"\default_project_image.jpg");
        }
    }
}

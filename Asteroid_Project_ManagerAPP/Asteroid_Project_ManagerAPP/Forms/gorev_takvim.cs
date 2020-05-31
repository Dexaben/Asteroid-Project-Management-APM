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
    public partial class gorev_takvim : Form
    {
        SqlCommand komut = new SqlCommand();
  
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        DataTable table = new DataTable();
        class group_box
        {
            public GroupBox groupBox { get; set; }
            public ListView listView { get; set; }
        }
        class task_item
        {
            public DateTime start_tarih { get; set;}
            public DateTime finish_tarih { get; set; }
            public ListViewItem listViewItem { get; set;}
        }
        List<task_item> tasks = new List<task_item>();
        public gorev_takvim()
        {
            InitializeComponent();
        }
        public void gorev_takvim_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                for (int z = 0; z < tableLayoutPanel1.ColumnCount;z++)
                {
                    group_box group_Box = new group_box();
                    group_Box.groupBox = (GroupBox)this.tableLayoutPanel1.GetControlFromPosition(z, i);
                    foreach (var listview in this.tableLayoutPanel1.GetControlFromPosition(z, i).Controls.OfType<ListView>())
                    {
                        group_Box.listView = listview;
                    }
                    groupBoxes.Add(group_Box);
                }
            }
            komut = new SqlCommand("SELECT T.task_id,(SELECT P.project_name FROM PROJECTS P WHERE P.project_id=T.project_id)AS project_name,(SELECT P.project_start_date FROM PROJECTS P WHERE P.project_id=T.project_id)AS project_start_date,(SELECT P.project_finish_date FROM PROJECTS P WHERE P.project_id=T.project_id)AS project_finish_date,T.task_name,T.task_details,T.task_start_date,T.task_finish_date,T.task_status,T.task_urgency FROM TASKS T INNER JOIN TASK_WORKERS TW ON TW.task_id=T.task_id WHERE T.project_id=@PROJECT_ID AND T.task_status<>'Görev Tamamlandı'AND TW.worker_id=@WORKER_ID");
            komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
            komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table != null)
            {
                if (table.Rows.Count > 0)
                {
                    DateTime projectStart = (DateTime)table.Rows[0]["project_start_date"];
                    takvim_date_picker.MinDate = projectStart;
                    DateTime projectFinish = (DateTime)table.Rows[0]["project_finish_date"]; 
                    takvim_date_picker.MaxDate = projectFinish;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        task_item task = new task_item();
                        System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                        DateTime startTime = Scripts.Tools.DateFunctions.DATETIME_CONVERTER(table.Rows[i]["task_finish_date"].ToString());
                        task.finish_tarih = startTime;
                        DateTime finishTime = Scripts.Tools.DateFunctions.DATETIME_CONVERTER(table.Rows[i]["task_start_date"].ToString());
                        task.start_tarih = finishTime;
                        ListViewItem listviewItem = new ListViewItem();
                        listviewItem.Text = table.Rows[i]["task_name"].ToString();
                        Random rnd = new Random();
                        listviewItem.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                        if (tasks.Count > 0)
                        {
                            do
                            {
                                 rnd = new Random();
                                listviewItem.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                            } while (tasks[tasks.Count - 1].listViewItem.BackColor == listviewItem.BackColor);
                        }
                        if (table.Rows[i]["task_status"].ToString() == "Acil") listviewItem.BackColor = Color.Red;
                        task.listViewItem = listviewItem;
                        tasks.Add(task);
                    }
                }
            }
            bugun_olacaklar_listView.Items.Clear();
            dateTime = Scripts.SQL.SqlQueries.GET_SERVER_DATE();
           
            for (int i = 0; i < groupBoxes.Count; i++)
            {
                groupBoxes[i].groupBox.BackColor = Color.LightBlue;
            }
            takvim_date_picker.Value = dateTime;
            dateTimePicker1_ValueChanged(this, null);
        }
        void GUN_TIKLA(GroupBox groupBox)
        {
            TAKVIM_AYARLA();
            if (groupBox.Text.Length >0)
            {
                groupBox.BackColor = Scripts.Tools.ColorTools.DARKER_COLOR(groupBox.BackColor);
                bugun_olacaklar_listView.Items.Clear();
                events_groupbox.Visible = true;
                DateTime date = new DateTime(dateTime.Year, dateTime.Month, Convert.ToInt32(groupBox.Text));
                if(date >= takvim_date_picker.MinDate && date <= takvim_date_picker.MaxDate)
                takvim_date_picker.Value = date;
                events_groupbox.Text = date.ToString("dddd, dd MMMM yyyy");
                komut = new SqlCommand("SELECT T.task_id,(SELECT P.project_name FROM PROJECTS P WHERE P.project_id=T.project_id)AS project_name,(SELECT P.project_start_date FROM PROJECTS P WHERE P.project_id=T.project_id)AS project_start_date,(SELECT P.project_finish_date FROM PROJECTS P WHERE P.project_id=T.project_id)AS project_finish_date,T.task_name,T.task_details,T.task_start_date,T.task_finish_date,T.task_status,T.task_urgency FROM TASKS T INNER JOIN TASK_WORKERS TW ON TW.task_id=T.task_id WHERE T.project_id=@PROJECT_ID AND T.task_status<>'Görev Tamamlandı'AND TW.worker_id=@WORKER_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", a.worker_id);
                komut.Parameters.AddWithValue("@PROJECT_ID", a.project_id);
                Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Görev bilgileri alınamadı.", Color.Red, 3000);
                table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
               
                DataTable bugun_Bitecek_Gorevler = new DataTable();
                bugun_Bitecek_Gorevler.Columns.Add("Görev ID", typeof(int));
                bugun_Bitecek_Gorevler.Columns.Add("Görev", typeof(string));
                bugun_Bitecek_Gorevler.Columns.Add("Görev Projesi", typeof(string));
                bugun_Bitecek_Gorevler.Columns.Add("Görev Durumu", typeof(string));

                DataTable devam_Eden_Gorevler = new DataTable();
                devam_Eden_Gorevler.Columns.Add("Görev ID", typeof(int));
                devam_Eden_Gorevler.Columns.Add("Görev", typeof(string));
                devam_Eden_Gorevler.Columns.Add("Görev Projesi", typeof(string));
                devam_Eden_Gorevler.Columns.Add("Görev Durumu", typeof(string));
                if (table != null)
                {
                    if (table.Rows.Count > 0)
                    {
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                            DateTime task_finish_date =Scripts.Tools.DateFunctions.DATETIME_CONVERTER(table.Rows[i]["task_finish_date"].ToString());
                            DateTime task_start_date = Scripts.Tools.DateFunctions.DATETIME_CONVERTER(table.Rows[i]["task_start_date"].ToString());
                            if (task_start_date.Date <= date.Date)
                            {
                                if (task_finish_date.Date == date.Date)
                                {
                                    bugun_Bitecek_Gorevler.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["task_name"].ToString(), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_status"].ToString());
                                }
                                else if (task_finish_date.Date >= date.Date)
                                {
                                    devam_Eden_Gorevler.Rows.Add(Convert.ToInt32(table.Rows[i]["task_id"]), table.Rows[i]["task_name"].ToString(), table.Rows[i]["project_name"].ToString(), table.Rows[i]["task_status"].ToString());
                                }
                            }

                        }
                        bugun_suresi_dolacak.DataSource = bugun_Bitecek_Gorevler;
                        devam_eden.DataSource = devam_Eden_Gorevler;
                    }
                }
                foreach (ListView listview in groupBox.Controls.OfType<ListView>())
                {
                    for (int i = 0; i < listview.Items.Count; i++)
                    {
                        bugun_olacaklar_listView.Items.Add((ListViewItem)listview.Items[i].Clone());
                    }
                }
            }
        }
        private void gun_Click(object sender, EventArgs e)
        {
            if(sender is GroupBox)
            {
                GroupBox groupBox = (GroupBox)sender;
                GUN_TIKLA(groupBox);
            }
        }
        DateTime dateTime = new DateTime();
        List<group_box> groupBoxes = new List<group_box>();
        void TAKVIM_AYARLA()
        {
            for(int i = 0;i<groupBoxes.Count;i++)
            {
                groupBoxes[i].groupBox.Text = "";
                groupBoxes[i].listView.Items.Clear();
            }
            takvim_date_picker.Value = dateTime;
            switch(Convert.ToInt32(dateTime.Month))
            {
                case 1: ay_left.Text = "Ocak"; break;
                case 2: ay_left.Text = "Şubat"; break;
                case 3: ay_left.Text = "Mart"; break;
                case 4: ay_left.Text = "Nisan"; break;
                case 5: ay_left.Text = "Mayıs"; break;
                case 6: ay_left.Text = "Haziran"; break;
                case 7: ay_left.Text = "Temmuz"; break;
                case 8: ay_left.Text = "Ağustos"; break;
                case 9: ay_left.Text = "Eylül"; break;
                case 10: ay_left.Text = "Ekim"; break;
                case 11: ay_left.Text = "Kasım"; break;
                case 12: ay_left.Text = "Aralık"; break;
                default: ay_left.Text = ""; break;
            }
            yil_right.Text = dateTime.Year.ToString();
            DateTime date = new DateTime(dateTime.Year, dateTime.Month, 1);
            int baslangic = Convert.ToInt32(date.DayOfWeek);
            switch(baslangic)
            {
                case 0: baslangic = 6; break;
                case 1: baslangic = 0; break;
                case 2: baslangic = 1; break;
                case 3: baslangic = 2; break;
                case 4: baslangic = 3; break;
                case 5: baslangic = 4; break;
                case 6: baslangic = 5; break;
            }
            for (int i = baslangic,y=0;y<DateTime.DaysInMonth(dateTime.Year, dateTime.Month); i++,y++)
            {
                date = new DateTime(dateTime.Year, dateTime.Month, y+1);
                groupBoxes[i].groupBox.Text = (y+1).ToString();
                groupBoxes[i].groupBox.BackColor = Scripts.Tools.ColorTools.DARKER_COLOR(Color.LightBlue, 80);
                for (int z = 0;z<tasks.Count;z++)
                {
                    if(date >= tasks[z].start_tarih.Date && date <= tasks[z].finish_tarih.Date )
                    {
                        groupBoxes[i].listView.Items.Add((ListViewItem)tasks[z].listViewItem.Clone());
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < groupBoxes.Count; i++)
            {
                groupBoxes[i].groupBox.BackColor = Color.LightBlue;
            }
            dateTime = takvim_date_picker.Value;
            DateTime date = new DateTime(dateTime.Year, dateTime.Month, 1);
            int baslangic = Convert.ToInt32(date.DayOfWeek);
            switch (baslangic)
            {
                case 0: baslangic = 6; break;
                case 1: baslangic = 0; break;
                case 2: baslangic = 1; break;
                case 3: baslangic = 2; break;
                case 4: baslangic = 3; break;
                case 5: baslangic = 4; break;
                case 6: baslangic = 5; break;
            }
            GUN_TIKLA(groupBoxes[baslangic + Convert.ToInt32(dateTime.Day)-1].groupBox);
        }

        private void bugun_suresi_dolacak_DoubleClick(object sender, EventArgs e)
        {
            if(bugun_suresi_dolacak.Rows.Count >0)
            {
                if(bugun_suresi_dolacak.SelectedRows.Count >0)
                {
                    gorev gorev_ = new gorev();
                    gorev_.gorev_id = Convert.ToInt32(bugun_suresi_dolacak.SelectedRows[0].Cells[0].Value);
                    gorev_.ShowDialog();
                }
            }
        }

        private void devam_eden_DoubleClick(object sender, EventArgs e)
        {
            if (devam_eden.Rows.Count > 0)
            {
                if (devam_eden.SelectedRows.Count > 0)
                {
                    gorev gorev_ = new gorev();
                    gorev_.gorev_id = Convert.ToInt32(devam_eden.SelectedRows[0].Cells[0].Value);
                    gorev_.ShowDialog();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (takvim_date_picker.Value.AddMonths(1) <= takvim_date_picker.MaxDate) takvim_date_picker.Value = takvim_date_picker.Value.AddMonths(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(takvim_date_picker.Value.AddMonths(-1) >= takvim_date_picker.MinDate) takvim_date_picker.Value = takvim_date_picker.Value.AddMonths(-1);
        }
    }
}

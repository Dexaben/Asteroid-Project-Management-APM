using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Asteroid_Project_ManagerAPP
{
    public partial class calisan_giris : Form
    {
        FUNCTIONS F = new FUNCTIONS();
        SqlCommand komut = new SqlCommand();
        System.Data.DataTable table = new System.Data.DataTable();
        public calisan_giris()
        {
            InitializeComponent();
            this.AcceptButton = giris_button;
        }
        private void calisan_giris_Load(object sender, EventArgs e)
        {
            hata_label.Text = "";
            hata_label.ForeColor = Color.Red;
            komut = new SqlCommand("SELECT worker_id,worker_image,worker_name,worker_onay FROM WORKERS");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri alınamadı.", Color.Red, 4000);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Çalışan ID", typeof(int));
            dataTable.Columns.Add("Çalışan Resmi", typeof(Image));
            dataTable.Columns.Add("Çalışan İsim", typeof(string));
            dataTable.Columns.Add("Çalışan Onay", typeof(bool));
            for(int i = 0;i<table.Rows.Count;i++)
            {
                dataTable.Rows.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[i]["worker_image"]), table.Rows[i]["worker_name"].ToString(), (bool)table.Rows[i]["worker_onay"]);
            }
            calisan_datagridview.DataSource = dataTable;
            calisan_datagridview.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            ((DataGridViewImageColumn)calisan_datagridview.Columns[1]).ImageLayout = DataGridViewImageCellLayout.Zoom;
            if (Properties.Settings.Default.UserName != string.Empty || Properties.Settings.Default.Password != string.Empty)
            {
                kullanici_adi_textBox.Text = Properties.Settings.Default.UserName;
                sifre_textBox.Text = Properties.Settings.Default.Password;
                beni_Hatirla_checkBox.Checked = true;
                this.BeginInvoke(new MethodInvoker(dataselect)); //DATAGRIDVIEW DEKİ KULLANICI İSMİ PROPERTIESDE KAYITLI KULLANICI ADIYLA AYNI OLANI SEÇIYOR.
            }
            else
            {
                kullanici_adi_textBox.Text = "";
                sifre_textBox.Text = "";
                beni_Hatirla_checkBox.Checked = false;
            }
        }
        void dataselect()
        {
            foreach (DataGridViewRow row in calisan_datagridview.Rows)
            {
                if (row.Cells[2].Value.ToString().Equals(Properties.Settings.Default.UserName))
                {
                    calisan_datagridview.Rows[row.Index].Selected = true;
                    return;
                }
            }
            hata_label.Text = "Böyle bir çalışan kaydı veritabanında bulunamadı!";
            hata_label.ForeColor = Color.Red;
            Properties.Settings.Default.UserName = "";
            Properties.Settings.Default.Password = "";
            Properties.Settings.Default.Save();
            kullanici_adi_textBox.Text = "";
            sifre_textBox.Text = "";
            beni_Hatirla_checkBox.Checked = false;
        }
        private void button1_Click_1(object sender, EventArgs e) //GİRİŞ YAP
        {
                komut = new SqlCommand("SELECT worker_name,worker_password,worker_onay,worker_id FROM WORKERS WHERE worker_name=@wrk_name");
                komut.Parameters.AddWithValue("@wrk_name", kullanici_adi_textBox.Text);
            table = F.SQL_SELECT_DATATABLE(komut, "Kullanıcı Adı veya Şifre Yanlış!", Color.Red, 4000);
            if(table.Rows.Count !=0)
            {
                if (table.Rows[0]["worker_name"].ToString() == kullanici_adi_textBox.Text && F.Decrypt(table.Rows[0]["worker_password"].ToString()) == sifre_textBox.Text)
                {
                    if ((bool)table.Rows[0]["worker_onay"] == true)
                    {
                        if (kullanici_adi_textBox.Text.Length > 0 && sifre_textBox.Text.Length > 0)
                        {
                            Application.OpenForms["AnaForm"].MainMenuStrip.Visible = true;
                            AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
                            if (a.toolStrip1 != null)
                            {
                                F.DURUM_LABEL("Durum: Giriş yapıldı. Son giriş zamanı:" + F.GET_SERVER_DATE().ToString(), Color.Green);
                                F.LOG_ENTER(Convert.ToInt32(table.Rows[0]["worker_id"]), F.WORKER_NAME_BY_WORKER_ID(Convert.ToInt32(table.Rows[0]["worker_id"])) + " adlı çalışan uygulamaya giriş yaptı.", F.GET_SERVER_DATE());
                            }
                            if (beni_Hatirla_checkBox.Checked)
                            {
                                Properties.Settings.Default.UserName = kullanici_adi_textBox.Text;
                                Properties.Settings.Default.Password = sifre_textBox.Text;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.UserName = "";
                                Properties.Settings.Default.Password = "";
                                Properties.Settings.Default.Save();
                            }
                            a.worker_id = Convert.ToInt32(table.Rows[0]["worker_id"]);
                            a.ANAFORM_BILGILER_GUNCELLE();
                            F.FORM_AC(new AnaPanel(), true);
                        }
                        else
                        {
                            hata_label.Text = "Kullanıcı Adı veya Şifre boş bırakılamaz!";
                            hata_label.ForeColor = Color.Red;
                        }
                    }
                    else
                    {
                        hata_label.Text = "Kullanıcı yönetici tarafından 'ONAYLI' değildir.";
                        hata_label.ForeColor = Color.Red;
                    }
                }
                else
                {
                    hata_label.Text = "Kullanıcı Adı veya Şifre Yanlış!";
                    hata_label.ForeColor = Color.Red;
                }
            }
            else
            {
                hata_label.Text = "Böyle bir çalışan kaydı veritabanında bulunamadı!";
                hata_label.ForeColor = Color.Red;
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
                Properties.Settings.Default.Save();
                kullanici_adi_textBox.Text = "";
                sifre_textBox.Text = "";
                beni_Hatirla_checkBox.Checked = false;

            }
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            F.FORM_AC(new calisan_kayit(), true);
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (calisan_datagridview.SelectedRows.Count > 0)
            {
                kullanici_adi_textBox.Text = calisan_datagridview.SelectedRows[0].Cells[2].Value.ToString();
                sifre_textBox.Text = "";
                beni_Hatirla_checkBox.Checked = false;
                Properties.Settings.Default.UserName = "";
                Properties.Settings.Default.Password = "";
            }
        }
    }
}

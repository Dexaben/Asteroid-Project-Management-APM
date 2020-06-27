using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerCEO
{
    public partial class AnaForm : Form
    {
        SqlCommand komut = new SqlCommand();
        
        DataTable table = new DataTable();
        DateTime sonGirisZamani;
        public AnaForm()
        {
            InitializeComponent();
        }
        [Obsolete]
        public void StartAnaForm()
        {
            Scripts.Tools.LogTools.LOG_ENTER(0, $"({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarı tarafından yönetici programına giriş yapıldı.", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
            sonGirisZamani = Scripts.SQL.SqlQueries.GET_SERVER_DATE();
            menuStrip.Visible = false;
            groupBox1.Visible = false;
            komut = new SqlCommand("SELECT yonetici_adi,program_serial FROM PROGRAM WHERE yonetici_id=1");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Bilgiler veritabanından alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table.Rows.Count != 0)
            {
                if (table.Rows[0]["yonetici_adi"] != DBNull.Value)
                {
                    Scripts.Form.FormManager.FORM_AC(new giris(), true);
                    Scripts.Form.Status.STATUS_LABEL("Durum:Kayıt olundu.Giriş bekleniyor.", Color.White);
                }
                else
                {
                    Properties.Settings.Default.AdminUserName = "";
                    Properties.Settings.Default.AdminPassword = "";
                    Scripts.Form.FormManager.FORM_AC(new kayit(), true);
                    Scripts.Form.Status.STATUS_LABEL("Durum:Serial girildi.Yönetici kaydı bekleniyor.", Color.Yellow);
                }
            }
            else
            {
                Properties.Settings.Default.AdminUserName = "";
                Properties.Settings.Default.AdminPassword = "";
                Scripts.Form.Status.STATUS_LABEL("Durum:Serial girişi bekleniyor.", Color.White);
                Scripts.Form.FormManager.FORM_AC(new serial_gir(), true);
            }
            SIRKETBILGI_GUNCELLE();
        }
     
        private void AnaForm_Load(object sender, EventArgs e)
        {
            Forms.connectAPM connectAPM = new Forms.connectAPM();
            connectAPM.Show(this);
          
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White;
           
        }
        public void SIRKETBILGI_GUNCELLE()
        {
            komut = new SqlCommand("SELECT worker_id,worker_name FROM WORKERS WHERE worker_onay=0");
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Çalışan onay durumları alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            duyurular.Items.Clear();
            if (table != null && table.Rows.Count >0)
            {
                groupBox2.Visible = true;
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    ListViewItem listBoxItem = new ListViewItem();
                    listBoxItem.Text = $"{table.Rows[i]["worker_name"]} adlı çalışan onay istiyor.";
                    listBoxItem.ForeColor = Color.ForestGreen;
                    listBoxItem.Font = new Font("Bahnschrift", 9, FontStyle.Bold);
                    duyurular.Items.Add(listBoxItem);
                }
            }
            else groupBox2.Visible = false;
            komut = new SqlCommand("SELECT company_logo,company_name,company_detail,program_serial FROM PROGRAM WHERE yonetici_id=1");
          SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Bilgiler veritabanından alınamadı.", Color.Red, 3000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if (table.Rows.Count != 0)
            {
                sirket_resmi.Image = Scripts.Tools.ImageTools.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["company_logo"]);
                sirket_ismi.Text = (table.Rows[0]["company_name"]).ToString();
                if (table.Rows[0]["company_name"] != DBNull.Value)
                {
                    groupBox1.Visible = true;
                }
                else
                {
                    menuStrip.Visible = false;
                    groupBox1.Visible = false;
                }
                sirket_aciklamasi.Text = (table.Rows[0]["company_detail"]).ToString();
            }
            else
            {
                Scripts.Form.Status.STATUS_LABEL("Durum:Serial girişi bekleniyor.", Color.White);
                menuStrip.Visible = false;
                groupBox1.Visible = false;
            }
        }
        private void çalışanOnayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.calisan(), true);
        }

        private void departmanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.departman(), true);
        }

        private void şirketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new sirket(), true);
        }
        private void projelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.projeler(), true);
        }

        private void panelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.AnaPanel(), true);
        }

        private void pozisyonlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.pozisyonlar(), true);
        }

        private void aktiviteDökümüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Scripts.Form.FormManager.FORM_AC(new Forms.activity(), true);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string fileName = Scripts.Tools.ImageTools.OPENFILEIMAGE();
            if (fileName != null)
                sirket_resmi.Image = Image.FromFile(fileName);

        }
        [Obsolete]
        private void AnaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Scripts.Tools.LogTools.LOG_ENTER(0, $"({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarı tarafından yönetici programından çıkış yapıldı.Programın kullanım süresi:{Scripts.SQL.SqlQueries.GET_SERVER_DATE() - sonGirisZamani}", Scripts.SQL.SqlQueries.GET_SERVER_DATE());
        }

        private void toplantıBaşlatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

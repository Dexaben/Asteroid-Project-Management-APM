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
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        DateTime sonGirisZamani;
        public AnaForm()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void AnaForm_Load(object sender, EventArgs e)
        {
            F.LOG_ENTER(0, $"({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarı tarafından yönetici programına giriş yapıldı.", F.GET_SERVER_DATE());
            sonGirisZamani = F.GET_SERVER_DATE();
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.White;
            menuStrip.Visible = false;
            groupBox1.Visible = false;
            komut = new SqlCommand("SELECT yonetici_adi,program_serial FROM PROGRAM WHERE yonetici_id=1");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Bilgiler veritabanından alınamadı.", Color.Red, 3000);
            if (table.Rows.Count != 0)
            {
                if (table.Rows[0]["yonetici_adi"] != DBNull.Value)
                {
                    F.FORM_AC(new giris(), true);
                    F.DURUM_LABEL("Durum:Kayıt olundu.Giriş bekleniyor.", Color.White);
                }
                else
                {
                    Properties.Settings.Default.AdminUserName = "";
                    Properties.Settings.Default.AdminPassword = "";
                    F.FORM_AC(new kayit(), true);
                    F.DURUM_LABEL("Durum:Serial girildi.Yönetici kaydı bekleniyor.", Color.Yellow);
                }
            }
            else
            {
                Properties.Settings.Default.AdminUserName = "";
                Properties.Settings.Default.AdminPassword = "";
                F.DURUM_LABEL("Durum:Serial girişi bekleniyor.", Color.White);
                F.FORM_AC(new serial_gir(), true);
            }
            SIRKETBILGI_GUNCELLE();
        }
        public void SIRKETBILGI_GUNCELLE()
        {
            komut = new SqlCommand("SELECT worker_id,worker_name FROM WORKERS WHERE worker_onay=0");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan onay durumları alınamadı.", Color.Red, 4000);
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
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Bilgiler veritabanından alınamadı.", Color.Red, 3000);
            if (table.Rows.Count != 0)
            {
                sirket_resmi.Image = F.CONVERT_BYTE_ARRAY_TO_IMAGE(table.Rows[0]["company_logo"]);
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
                F.DURUM_LABEL("Durum:Serial girişi bekleniyor.", Color.White);
                menuStrip.Visible = false;
                groupBox1.Visible = false;
            }
        }
        private void çalışanOnayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new Forms.calisan(), true);
        }

        private void departmanlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new Forms.departman(), true);
        }

        private void şirketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new sirket(), true);
        }
        private void projelerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new Forms.projeler(), true);
        }

        private void panelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new Forms.AnaPanel(), true);
        }

        private void pozisyonlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new Forms.pozisyonlar(), true);
        }

        private void aktiviteDökümüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            F.FORM_AC(new Forms.activity(), true);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string fileName = F.OPENFILEIMAGE();
            if (fileName != null)
                sirket_resmi.Image = Image.FromFile(fileName);

        }
        [Obsolete]
        private void AnaForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            F.LOG_ENTER(0, $"({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarı tarafından yönetici programından çıkış yapıldı.Programın kullanım süresi:{F.GET_SERVER_DATE() - sonGirisZamani}", F.GET_SERVER_DATE());
        }
    }
}

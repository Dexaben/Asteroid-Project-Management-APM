using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerCEO
{
    public partial class giris : Form
    {
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public giris()
        {
            InitializeComponent();
            this.AcceptButton = giris_button;
        }
        [Obsolete]
        private void button1_Click(object sender, EventArgs e)
        {
            komut = new SqlCommand("SELECT yonetici_adi,yonetici_password FROM PROGRAM WHERE yonetici_id=1");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Program bilgileri alınamadı.", Color.Red, 4000);
            if(table.Rows.Count !=0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i][0].ToString() == kullanici_adi.Text && F.Decrypt(table.Rows[i][1].ToString()) == sifre.Text)
                    {
                        if (kullanici_adi.Text.Length > 0 && sifre.Text.Length > 0)
                        {
                            a.MainMenuStrip.Visible = true;
                            if (a.toolStrip1 != null)
                            {
                                F.DURUM_LABEL("Durum: Giriş yapıldı. Son giriş zamanı:" + F.GET_SERVER_DATE().ToString(), Color.Green);
                            }
                            if (beni_hatirla.Checked)
                            {
                                Properties.Settings.Default.AdminUserName = kullanici_adi.Text;
                                Properties.Settings.Default.AdminPassword = sifre.Text;
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.AdminUserName = "";
                                Properties.Settings.Default.AdminPassword = "";
                                Properties.Settings.Default.Save();
                            }
                            F.FORM_AC(new Forms.AnaPanel(), true);
                            F.LOG_ENTER(0, $"({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarından yönetici programına giriş yapıldı.Kullanıcı Adı:{Properties.Settings.Default.AdminUserName}",F.GET_SERVER_DATE());
                        }
                        else
                        {
                            label1.Text = "Kullanıcı Adı veya Şifre boş bırakılamaz!";
                            label1.ForeColor = Color.Red;
                        }

                    }
                    else
                    {
                        label1.Text = "Kullanıcı Adı veya Şifre Yanlış!";
                        label1.ForeColor = Color.Red;
                    }
                }
            }
        }
        private void giris_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AdminUserName != string.Empty || Properties.Settings.Default.AdminPassword != string.Empty)
            {
                kullanici_adi.Text = Properties.Settings.Default.AdminUserName;
                sifre.Text = Properties.Settings.Default.AdminPassword;
                beni_hatirla.Checked = true;
            }
            else
            {
                kullanici_adi.Text = "";
                sifre.Text = "";
                beni_hatirla.Checked = false;
            }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

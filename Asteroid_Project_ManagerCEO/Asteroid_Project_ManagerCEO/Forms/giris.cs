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
            Scripts.SQL.SQL_COMMAND SqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:Program bilgileri alınamadı.", Color.Red, 4000);
            table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(SqlCommand);
            if(table.Rows.Count !=0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (table.Rows[i][0].ToString() == kullanici_adi.Text && Scripts.Tools.CryptographyFunctions.Decrypt(table.Rows[i][1].ToString()) == sifre.Text)
                    {
                        if (kullanici_adi.Text.Length > 0 && sifre.Text.Length > 0)
                        {
                            a.MainMenuStrip.Visible = true;
                            if (a.toolStrip1 != null)
                            {
                                Scripts.Form.Status.STATUS_LABEL("Durum: Giriş yapıldı. Son giriş zamanı:" + Scripts.SQL.SqlQueries.GET_SERVER_DATE().ToString(), Color.Green);
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
                            Scripts.Form.FormManager.FORM_AC(new Forms.AnaPanel(), true);
                            Scripts.Tools.LogTools.LOG_ENTER(0, $"({System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].MapToIPv4()}) {Environment.MachineName} bilgisayarından yönetici programına giriş yapıldı.Kullanıcı Adı:{Properties.Settings.Default.AdminUserName}",Scripts.SQL.SqlQueries.GET_SERVER_DATE());
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

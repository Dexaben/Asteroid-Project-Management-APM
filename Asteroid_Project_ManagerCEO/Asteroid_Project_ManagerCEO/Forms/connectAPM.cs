using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class connectAPM : Form
    {
        AnaForm anaform;
        public connectAPM()
        {
            InitializeComponent();
            label1.Text = "Bağlanılıyor...";
            try
            {
                anaform = (AnaForm)Application.OpenForms["AnaForm"];
                anaform.Hide();
            }
            catch
            {
                MessageBox.Show("AnaForm bulunamadı.");
                Application.Exit();
            }
        }
        private void connectAPM_Load(object sender, EventArgs e)
        {
            CheckConnection();
        }
        private void CheckConnection()
        {
            listBox1.Items.Add("Bağlantı dizesi alınıyor... ");
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection conn = config.ConnectionStrings;
                if (conn.ConnectionStrings["ConnectionStringAPM"].ConnectionString == "")
                    throw new ConnectionStringNotFindException("Bağlantı dizesi bulunamadı.");
                else Scripts.SQL.SqlConnections.SQL_SERVER_CONFIGURATION();
            }
            catch (ConnectionStringNotFindException)
            {
                listBox1.Items.Add("Bağlantı dizesi bulunamadı ");
                DialogResult dialogResult = MessageBox.Show("Bağlantı dizesi bulunamadı. Eklemek ister misiniz?", "Bağlantı dizesi bulunamadı.", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    //Data Source=25.3.143.231;Initial Catalog=APM_DATABASE;Persist Security Info=True;User ID=admin;Password=aylinalp1234
                    ConnectionStringForm();
                    return;
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                    return;
                }
            }
            ServerConnect();
        }
        private void ServerConnect()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection conn = config.ConnectionStrings;

            string tempConnString = conn.ConnectionStrings["ConnectionStringAPM"].ConnectionString;

            try
            {
                Scripts.SQL.SqlConnections.SQL_SERVER_CONFIGURATION();
                listBox1.Items.Add($"Bağlantı dizesi alındı. ({tempConnString.Substring(0, 35)}...)");
            try
            {

                Scripts.SQL.SqlConnections.SQL_SERVER_CONNECT();
                    listBox1.Items.Add($"Veritabanına bağlanıldı.");

        }
            catch (Exception ex)
            {
                listBox1.Items.Add($"Veritabanına bağlanılamadı.{ex}");
                    return;
            }
}
            catch
            {
                listBox1.Items.Add("Veritabanına bağlantı sağlanamadı.");
                return;
            }
            isConnected = true;
        }
        private void ConnectionStringForm()
        {
            Forms.ConnectionStringEnter form = new ConnectionStringEnter();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                ConnectionStringsSection conn = config.ConnectionStrings;
                conn.ConnectionStrings["ConnectionStringAPM"].ConnectionString = form.conStringText;
                config.Save(ConfigurationSaveMode.Modified);
            }
            else Application.Exit();
            form.Dispose();
            ServerConnect();
        }
        bool isConnected = false;[Obsolete]
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isConnected)
                label1.Text = "Bağlandı";
            else label1.Text = "Bağlanılıyor...";
            if (progressBar1.Value == 100 && isConnected)
            {
                timer1.Stop(); this.Close();
                anaform.Show();
               anaform.StartAnaForm();
               
            }
            else if (progressBar1.Value == 100 && !isConnected)
            {
                progressBar1.Value = 0;
            }
            else progressBar1.Increment(2);
        }
    }
}

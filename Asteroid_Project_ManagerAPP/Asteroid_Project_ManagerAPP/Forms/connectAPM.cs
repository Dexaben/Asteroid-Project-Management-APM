using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP.Forms
{
    public partial class connectAPM : Form
    {
        public connectAPM()
        {
            InitializeComponent();
            label1.Text = "Bağlanılıyor...";
        }

        private void connectAPM_Load(object sender, EventArgs e)
        {
            Scripts.SQL.SqlConnections.SQL_SERVER_CONFIGURATION();
            Scripts.SQL.SqlConnections.SQL_SERVER_CONNECT();
            label1.Text = "Bağlanılıyor... "+ Scripts.SQL.SqlConnections.CONNECTION_STATUS().ToString();
            if(Scripts.SQL.SqlConnections.CONNECTION_STATUS() == ConnectionState.Open)
            {
                Scripts.Form.FormManager.FORM_AC(new AnaForm(), true);
            }
        }
    }
}

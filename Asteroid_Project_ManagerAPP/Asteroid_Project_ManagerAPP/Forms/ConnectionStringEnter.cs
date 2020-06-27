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
    public partial class ConnectionStringEnter : Form
    {
        public ConnectionStringEnter()
        {
            InitializeComponent();
        }
        public string conStringText ="";
        private void button1_Click(object sender, EventArgs e)
        {
            conStringText = textBox1.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

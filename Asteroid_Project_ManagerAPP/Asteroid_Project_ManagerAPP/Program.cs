using System;
using System.ServiceModel;
using System.Windows.Forms;

namespace Asteroid_Project_ManagerAPP
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AnaForm anaForm = (AnaForm)Scripts.Form.FormManager.AnaFormSet();
            Application.Run(anaForm);
        }
    }
}

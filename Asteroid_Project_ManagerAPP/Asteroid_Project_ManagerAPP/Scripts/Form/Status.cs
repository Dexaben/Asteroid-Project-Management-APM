using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_Project_ManagerAPP.Scripts.Form
{
    public static class Status
    {
        /// <summary>
        /// ana form durum labelini suresiz olarak degistirir.
        /// </summary>
        public static AnaForm anaForm;
        public static void STATUS_LABEL(string durum_Text, System.Drawing.Color durum_Renk)
        {
            anaForm = (AnaForm)System.Windows.Forms.Application.OpenForms["AnaForm"];
            anaForm.toolStripLabel1.Text = durum_Text;
            anaForm.toolStripLabel1.ForeColor = durum_Renk;
        }
        static bool durum_panel = true;
        /// <summary>
        /// ana form durum labelini sureli olarak degistirir.
        /// </summary>
        /// <param name="durum_Gosterim_Suresi">milisaniye</param>
        public static void STATUS_LABEL(string durum_Text, System.Drawing.Color durum_Renk, float durum_Gosterim_Suresi)
        {
            anaForm = (AnaForm)System.Windows.Forms.Application.OpenForms["AnaForm"];
            try
            {
                if (durum_panel)
                {
                    durum_panel = false;
                    string temp_text = anaForm.toolStripLabel1.Text;
                    System.Drawing.Color temp_color = anaForm.toolStripLabel1.ForeColor;
                    anaForm.toolStripLabel1.Text = durum_Text;
                    anaForm.toolStripLabel1.ForeColor = durum_Renk;
                    WaitSomeTime(durum_Gosterim_Suresi, temp_text, temp_color);
                }
            }
            catch { }
        }
        public static async void WaitSomeTime(float delay, string tmptxt, System.Drawing.Color tmpclr)
        {
            await Task.Delay((int)delay);
            durum_panel = true;
            anaForm.toolStripLabel1.Text = tmptxt;
            anaForm.toolStripLabel1.ForeColor = tmpclr;
        }
    }
}

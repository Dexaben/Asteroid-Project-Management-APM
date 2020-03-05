using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Asteroid_Project_ManagerCEO
{
    public partial class serial_gir : Form
    {
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        public serial_gir()
        {
            InitializeComponent();
            this.AcceptButton = serial_onayla;
        }
        int yanlisSerial = 0;
        private void button1_Click(object sender, EventArgs e)
        {

            if (serial_textBox.Text == "FAD3-23FS-42BX-342F-DFSD")
            {
                F.DURUM_LABEL("Durum:Serial girildi.Yönetici kaydı bekleniyor.", Color.Yellow);

                komut = new SqlCommand("SELECT COUNT(yonetici_id)AS yonetici_id FROM PROGRAM");
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Program kaydı alınamadı.", Color.Red, 4000);
                if (table != null)
                {
                    if (Convert.ToInt32(table.Rows[0]["yonetici_id"]) > 0)
                    {
  
                            komut = new SqlCommand("DELETE FROM PROGRAM");
                            F.SQL_EXECUTENONQUERY(komut, "Hata:Program bilgileri silinemedi.", Color.Red, 4000);
            
                    }
                }
                        komut = new SqlCommand("INSERT INTO PROGRAM(program_serial,yonetici_id) VALUES(@SERIAL,1)");
                komut.Parameters.AddWithValue("@SERIAL", F.Encrypt(serial_textBox.Text));
                F.SQL_EXECUTENONQUERY(komut, "Hata:Serial kaydedilemedi.", Color.Red, 4000);


                komut = new SqlCommand("SELECT COUNT(worker_jobs)AS worker_jobs FROM JOBS WHERE worker_jobs='Proje Yöneticisi'");
                table = F.SQL_SELECT_DATATABLE(komut, "Hata:Pozisyon kaydı alınamadı.", Color.Red, 4000);
                if(table != null)
                {
                    if(Convert.ToInt32(table.Rows[0]["worker_jobs"]) > 0)
                    {
                        for (int i = 0;i< Convert.ToInt32(table.Rows[0]["worker_jobs"]);i++)
                        {
                            komut = new SqlCommand("DELETE FROM JOBS WHERE worker_jobs='Proje Yöneticisi'");
                            F.SQL_EXECUTENONQUERY(komut, "Hata:'Proje Yöneticisi' rolü eklenemedi.", Color.Red, 4000);
                        }
                    }
                }
                    komut = new SqlCommand("INSERT INTO JOBS(worker_jobs) VALUES('Proje Yöneticisi')");
                    F.SQL_EXECUTENONQUERY(komut, "Hata:'Proje Yöneticisi' rolü eklenemedi.", Color.Red, 4000);
                F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından serial girişi yapıldı.Kayıt Bekleniyor.", F.GET_SERVER_DATE());
                F.FORM_AC(new kayit(), true);
            }
            else
            {
                F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından serial girişi denemesi yapıldı.{yanlisSerial}. Deneme:{serial_textBox.Text}", F.GET_SERVER_DATE());
                yanlisSerial++;
                if (yanlisSerial > 3 && yanlisSerial < 10)
                {
                    if (yanlisSerial == 4)
                    {
                        MessageBox.Show("Asteroid Project Manager yönetici programı için serial bilgilerinizi bilmiyor veya öğrenmek istiyorsanız. asteroid.gamestdio@gmail.com adresiyle iletişime geçin.");
                    }
                    F.DURUM_LABEL("Hata:Seriali bilmiyorsanız:asteroid.gamestdio@gmail.com", Color.Red, 2000);
                }
                else
                {
                    F.DURUM_LABEL("Hata: Serial Yanlış veya Eksik girildi.", Color.Red, 2000);
                    if (yanlisSerial >= 10)
                    {
                        Application.Exit();
                    }
                }
            }
        }
    }
}


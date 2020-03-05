using Microsoft.VisualBasic;
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

namespace Asteroid_Project_ManagerCEO.Forms
{
    public partial class pozisyonlar : Form
    {
        public pozisyonlar()
        {
            InitializeComponent();
            this.AcceptButton = pozisyon_ekle;
        }
        AnaForm a = (AnaForm)Application.OpenForms["AnaForm"];
        SqlCommand komut = new SqlCommand();
        FUNCTIONS F = new FUNCTIONS();
        DataTable table = new DataTable();
        private void pozisyonlar_Load(object sender, EventArgs e)
        {
            pozisyon_listview.Items.Clear();
            komut = new SqlCommand("SELECT worker_jobs FROM JOBS");
            table = F.SQL_SELECT_DATATABLE(komut, "Hata:Çalışan pozisyonları veritabanından alınamadı.", Color.Red, 3000);
            if (table.Rows.Count != 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    pozisyon_listview.Items.Add(table.Rows[i][0].ToString());
                }
                if(pozisyon_listview.Items.IndexOf("Proje Yöneticisi") == -1)
                {
                    komut = new SqlCommand("INSERT INTO JOBS(worker_jobs) VALUES(Proje Yöneticisi)");
                    F.SQL_EXECUTENONQUERY(komut, "Hata:'Proje Yöneticisi' rolü eklenemedi.", Color.Red, 4000);
                }
            }
            
        }
        private void button1_Click(object sender, EventArgs e) //pozisyon ekle
        {
            komut = new SqlCommand("INSERT JOBS(worker_jobs) VALUES(@JOB)");
            string t = Interaction.InputBox("Pozisyon ismi 2 ile 50 karakter uzunluğunda olmalıdır ve zaten varolan bir pozisyon olmamalıdır.", "Pozisyon Ekle", null, -1, -1);
            if (UYGUNLUK_KONTROL(t) && pozisyon_listview.Items.IndexOf(t) == -1)
            {
                komut.Parameters.AddWithValue("@JOB", t);
                if (F.SQL_EXECUTENONQUERY(komut, "Hata:Pozisyon veritabanına eklenemedi", Color.Red, 3000))
                { pozisyonlar_Load(this, null); F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından pozisyon kaydı yapıldı.Pozisyon:{t}", F.GET_SERVER_DATE()); }
            }
            else
            {
                F.DURUM_LABEL("Hata: Pozisyon ismi uygun değil veya zaten varolan bir pozisyon.", Color.Red, 3000);
                return;
            }
        }
   
        private void button2_Click(object sender, EventArgs e) // pozisyonu duzenle
        {
            if (pozisyon_listview.SelectedItems.Count !=0)
            {
                if (pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString() != "Proje Yöneticisi")
                {
                    SqlCommand komut2 = new SqlCommand("UPDATE WORKER_POSITIONS SET worker_job=@JOB WHERE worker_job=@SELECTJOB");
                    komut = new SqlCommand("UPDATE JOBS SET worker_jobs = @JOB WHERE worker_jobs=@SELECTJOB");
                    komut.Parameters.AddWithValue("@SELECTJOB", pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString());
                    komut2.Parameters.AddWithValue("@SELECTJOB", pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString());
                    string t = Interaction.InputBox(pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString() + " pozisyonunun yeni ismini girin. Pozisyon ismi 2 ile 50 karakter uzunluğunda olmalıdır ve zaten varolan bir pozisyon olmamalıdır.", "Pozisyon Ekle", null, -1, -1);
                    if (UYGUNLUK_KONTROL(t) && pozisyon_listview.Items.IndexOf(t) == -1)
                    {
                        komut2.Parameters.AddWithValue("@JOB", t);
                        komut.Parameters.AddWithValue("@JOB", t);
                        if (F.SQL_EXECUTENONQUERY(komut, "Hata:Pozisyon veritabanına eklenemedi", Color.Red, 3000) && F.SQL_EXECUTENONQUERY(komut2, "Hata:Pozisyon veritabanına eklenemedi", Color.Red, 3000))
                        { F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından pozisyon güncellendi.({pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString()} => {t})", F.GET_SERVER_DATE()); pozisyonlar_Load(this, null);  }
                    }
                    else
                    {
                        F.DURUM_LABEL("Hata: Pozisyon ismi uygun değil veya zaten varolan bir pozisyon.", Color.Red, 3000);
                        return;
                    }
                }
                else F.DURUM_LABEL("'Proje Yöneticisi' rolü değiştirilemez.", Color.PaleVioletRed, 3000);
            }
            else F.DURUM_LABEL("Herhangibir pozisyon seçilmemiş.", Color.White, 3000);
        }
        bool UYGUNLUK_KONTROL(string sifre)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9 ğüşıöçĞÜŞİÖÇ]{2,50}$");
            if (r.IsMatch(sifre)) return true;
            else return false;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (pozisyon_listview.SelectedItems.Count != 0)
            {
                if(pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString() != "Proje Yöneticisi")
                {
                    DialogResult dialog = MessageBox.Show("Bu pozisyonu gerçekten SİLMEK istiyormusunuz? (Bu pozisyona sahip " + F.WORKER_POSITION_COUNT_BY_POSITIONNAME(pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString()) + " çalışan bulunuyor.)", "ÇIKIŞ", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        komut = new SqlCommand("DELETE FROM JOBS WHERE worker_jobs = @WORKER_JOB");
                        komut.Parameters.AddWithValue("@WORKER_JOB", pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString());
                        if (F.SQL_EXECUTENONQUERY(komut, "Hata:Pozisyon verisi silinemedi.", Color.Red, 4000))
                        {
                            komut = new SqlCommand("DELETE FROM WORKER_POSITIONS WHERE worker_job = @WORKER_JOB");
                            komut.Parameters.AddWithValue("@WORKER_JOB", pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString());
                            if (F.SQL_EXECUTENONQUERY(komut, "Hata:Pozisyon verisi çalışandan silinemedi.", Color.Red, 4000))
                            {
                                F.LOG_ENTER(0, $"{Environment.MachineName} bilgisayarından {pozisyon_listview.Items[pozisyon_listview.SelectedIndex].ToString()} pozisyon kaydı SİLİNDİ.", F.GET_SERVER_DATE());
                                pozisyonlar_Load(this, null);
                            }
                        }
                    }
                }
                else F.DURUM_LABEL("'Proje Yöneticisi' rolü silinemez.", Color.PaleVioletRed, 3000);
            }
            else F.DURUM_LABEL("Herhangibir pozisyon seçilmemiş.", Color.White, 3000);
        }
    }
}

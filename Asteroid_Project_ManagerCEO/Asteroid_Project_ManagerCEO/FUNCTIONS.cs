using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace Asteroid_Project_ManagerCEO
{
    public partial class FUNCTIONS //SQL FUNCTIONS
    {
        AnaForm a;
        SqlConnection sqlBaglan = new SqlConnection();
        public System.Data.DataTable SQL_SELECT_DATATABLE(SqlCommand sqlCommand, string hata_mesaji, System.Drawing.Color hata_color, int hata_gosterim_sure)
        {
            System.Data.DataTable DT = new System.Data.DataTable();
            try
            {
                sqlBaglan = SQL_BAGLAN(sqlBaglan);
                sqlBaglan.Open();
                sqlCommand.Connection = sqlBaglan;
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(DT);
            }
            catch { DURUM_LABEL(hata_mesaji, hata_color, hata_gosterim_sure); return null; }
            finally { if (sqlBaglan != null) sqlBaglan.Close(); }
            return DT;
        }
        public bool SQL_EXECUTENONQUERY(SqlCommand sqlCommand, string hata_mesaji, System.Drawing.Color hata_color, int hata_gosterim_sure)
        {
            try
            {
                sqlBaglan = SQL_BAGLAN(sqlBaglan);
                sqlBaglan.Open();
                sqlCommand.Connection = sqlBaglan;
                sqlCommand.ExecuteNonQuery();
            }
            catch { DURUM_LABEL(hata_mesaji, hata_color, hata_gosterim_sure); return false; }
            finally { if (sqlBaglan != null) sqlBaglan.Close(); }
            return true;
        }
        /// <summary>
        /// SQLCONNECTION string'i hangi bilgisayar için alacağını belirleyen fonksiyon.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public SqlConnection SQL_BAGLAN(SqlConnection sql)
        {
            sql = new SqlConnection("Data Source=25.3.143.231;Initial Catalog=APM_DATABASE;Persist Security Info=True;User ID=admin;Password=1234");
            return sql;
        }
        /// <summary>
        /// WORKER_ID'ye göre bir WORKER'ın calistigi pozisyonlari dizi halinde donduren fonksiyon.
        /// </summary>
        public string[] WORKER_ROLE_CALL_BY_ID(int worker_id)
        {
            try
            {
                sqlBaglan = SQL_BAGLAN(sqlBaglan); sqlBaglan.Open();
                SqlCommand komut = new SqlCommand("SELECT worker_job FROM WORKER_POSITIONS WHERE worker_id=@WORKER_ID", sqlBaglan);
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                SqlDataReader veri = komut.ExecuteReader();
                List<string> WORKER_ROLES = new List<string>();
                while (veri.Read())
                {
                    WORKER_ROLES.Add(veri[0].ToString());
                }
                veri.Close();
                return WORKER_ROLES.ToArray();
            }
            catch { DURUM_LABEL("Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                if (sqlBaglan != null)
                    sqlBaglan.Close();
            }
            return null;
        }
        /// <summary>
        /// Tum calisanlarin WORKER_ROLE'lerini alip istedigimiz rolden kac tane oldugunu donduren fonksiyon.
        /// </summary>
        public int WORKER_POSITION_COUNT_BY_POSITIONNAME(string worker_role)
        {
            try
            {
                sqlBaglan = SQL_BAGLAN(sqlBaglan);
                sqlBaglan.Open();
                SqlCommand komut = new SqlCommand("SELECT COUNT(worker_job)AS worker_job_count FROM WORKER_POSITIONS WHERE worker_job=@WORKER_JOB", sqlBaglan);
                komut.Parameters.AddWithValue("@WORKER_JOB", worker_role);
                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri veritabanından alınamadı.", System.Drawing.Color.Red, 3000);
                if (table != null)
                {
                    return Convert.ToInt32(table.Rows[0]["worker_job_count"]);
                }
                else DURUM_LABEL("Hata: Veritabanında kayıtlı pozisyon bulunamadı.", System.Drawing.Color.Yellow, 2000);
            }
            catch { DURUM_LABEL("Hata: Veritabanından rol sayısı alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                if (sqlBaglan != null)
                    sqlBaglan.Close();
            }
            return 0;
        }
        /// <summary>
        /// Calisanlarin arasinda GONDERILEN role sahip calisanlarin id'sini ve ismini dict(int,string) olarak donduren fonksiyon.
        /// </summary>
        public Dictionary<int, string> WORKER_NAME_AND_WORKER_ID_BY_WORKER_ROLE(string worker_role)
        {
            try
            {
                sqlBaglan = SQL_BAGLAN(sqlBaglan);
                sqlBaglan.Open();
                SqlCommand komut = new SqlCommand("SELECT W.worker_id,W.worker_name,WP.worker_job FROM WORKERS W INNER JOIN WORKER_POSITIONS WP ON W.worker_id=WP.worker_id WHERE WP.worker_job=@WORKER_JOB", sqlBaglan);
                komut.Parameters.AddWithValue("@WORKER_JOB", worker_role);
                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(komut, "Hata:Çalışan bilgileri veritabanından alınamadı.", System.Drawing.Color.Red, 3000);
                Dictionary<int, string> worker_id_name = new Dictionary<int, string>(table.Rows.Count);
                if (table != null)
                {
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["worker_job"].ToString() == worker_role)
                        {
                            worker_id_name.Add(Convert.ToInt32(table.Rows[i]["worker_id"]), table.Rows[i]["worker_name"].ToString());
                        }
                    }
                }
                else return null;
                return worker_id_name;
            }
            catch { DURUM_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                if (sqlBaglan != null)
                    sqlBaglan.Close();
            }
            return null;
        }
        /// <summary>
        /// Calisan id ye gore o calisanin o rolde olup olmadigibi donduren.
        /// </summary>
        public bool WORKER_HAS_ROLE_BY_WORKER_ID_AND_WORKER_ROLE(int worker_id, string worker_role)
        {
            try
            {
                sqlBaglan = SQL_BAGLAN(sqlBaglan);
                sqlBaglan.Open();
                SqlCommand komut = new SqlCommand("SELECT worker_job FROM WORKER_POSITIONS WHERE worker_id=@WORKER_ID", sqlBaglan);
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                SqlDataReader veri = komut.ExecuteReader();
                List<string> WORKER_ROLES = new List<string>();
                while (veri.Read())
                {
                    WORKER_ROLES.Add(veri[0].ToString());
                }
                for (int i = 0; i < WORKER_ROLES.Count; i++)
                {
                    if (WORKER_ROLES[i] == worker_role)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { DURUM_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                if (sqlBaglan != null)
                    sqlBaglan.Close();
            }
            return false;
        }
    }
    public partial class FUNCTIONS
    {
        public DateTime DATETIME_CONVERTER(string DT)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.ParseExact(DT, "dd.MM.yyyy HH:mm:ss", provider);
            }
            catch
            {
                dt = DateTime.ParseExact(DT, "d.MM.yyyy HH:mm:ss", provider);
            }
            return dt;
        }
        public string Tarih_Converter_DAY_HOUR_MINUTE(string DT)
        {
            System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
            DateTime dt = new DateTime();
            try
            {
                dt = DateTime.ParseExact(DT, "dd.MM.yyyy HH:mm:ss", provider);
            }
            catch
            {
                dt = DateTime.ParseExact(DT, "d.MM.yyyy HH:mm:ss", provider);
            }
            DateTime now_date = GET_SERVER_DATE();
            string tarih = "";
            if (dt < now_date)
            {
                if ((now_date - dt).Days == 0) tarih = (now_date - dt).Hours + " saat önce";
                else tarih = (now_date - dt).Days + " gün önce";
            }

            if (dt.Year == now_date.Year && dt.Month == now_date.Month && dt.Day == now_date.Day)
            {
                if (dt.Hour >= now_date.Hour)
                {
                    if ((dt - now_date).Hours > 0) tarih = "Bugün " + (dt - now_date).Hours + " saat " + ((dt - now_date).Minutes + " dakika sonra");
                    else
                    {
                        if ((dt - now_date).Minutes == 0) tarih = "Az önce";
                        else tarih = "Bugün " + ((now_date - dt).Minutes + " dakika önce");
                    }

                }
                else
                {
                    if ((now_date - dt).Hours > 0) tarih = "Bugün " + (now_date - dt).Hours + " saat " + ((now_date - dt).Minutes + " dakika önce");
                    else tarih = "Bugün " + ((now_date - dt).Minutes + " dakika önce");

                }
            }
            if (dt > now_date)
            {
                if ((dt - now_date).Days == 0) tarih = (dt - now_date).Hours + " saat sonra";
                else tarih = (dt - now_date).Days + " gün sonra";

            }

            if ((dt - now_date).Days == 1) tarih = "Yarın " + ((dt - now_date).Hours + " saat sonra");
            if ((now_date - dt).Days == 1) tarih = "Dün " + (now_date - dt).Hours + " saat önce";
            return tarih;
        }
    }
    public partial class FUNCTIONS //EXTRAS
    {
        /// <summary>
        /// Gönderilen resmi gosteriri.
        /// </summary>
        public void OpenImage(System.Drawing.Image image)
        {
            string myPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string myPath = System.IO.Path.Combine(myPictures, "testImage.jpg");
            image.Save(myPath, System.Drawing.Imaging.ImageFormat.Jpeg);
            System.Diagnostics.Process photoViewer = new System.Diagnostics.Process();
            photoViewer.StartInfo.FileName = myPath;
            photoViewer.Start();
        }

        /// <summary>
        /// Gönderilen resmin çözünürlüğünü düşürür.
        /// Kullanım : { Bitmap temp_image =(Bitmap)gönderilen_resim;    
        ///                 Bitmap bp = ResizeImage((Bitmap)gönderilen_resim, new Size((int)(0.5f * mg.Width), (int)(0.5f * mg.Height)));
        ///                         gönderilen_resim = (Image)bp.SetResolution(temp_image.HorizontalResolution, temp_image.VerticalResolution);
        /// </summary>
        public System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap mg, System.Drawing.Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            int x = 0;
            int y = 0;
            System.Drawing.Bitmap bp;
            if ((mg.Width / Convert.ToDouble(newSize.Width)) > (mg.Height /
            Convert.ToDouble(newSize.Height)))
                ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(newSize.Width);
            else
                ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(newSize.Height);
            myThumbHeight = Math.Ceiling(mg.Height / ratio);
            myThumbWidth = Math.Ceiling(mg.Width / ratio);
            System.Drawing.Size thumbSize = new System.Drawing.Size((int)myThumbWidth, (int)myThumbHeight);
            bp = new System.Drawing.Bitmap(newSize.Width, newSize.Height);
            x = (newSize.Width - thumbSize.Width) / 2;
            y = (newSize.Height - thumbSize.Height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(x, y, thumbSize.Width, thumbSize.Height);
            g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, System.Drawing.GraphicsUnit.Pixel);
            return bp;
        }
        /// <summary>
        /// Gönderilen resmi byte dizesine cevirir.
        /// </summary>
        /// <param name="imageToConvert">donusturulecek resim</param>
        /// <param name="formatOfImage">donusturulecek format</param>
        public byte[] CONVERT_IMAGE_TO_BYTE_ARRAY(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret = null;
            try
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch { DURUM_LABEL("Hata:Resim dönüştürülemedi.", System.Drawing.Color.Orange, 3500); }
            return Ret;
        }
        /// <summary>
        /// Gönderilen byte dizesini resme çevirir.
        /// </summary>
        /// <param name="veri">donusturulecek byte verisi</param>
        public System.Drawing.Image CONVERT_BYTE_ARRAY_TO_IMAGE(object veri)
        {
            try
            {
                Byte[] byteBLOBData = new Byte[0];
                byteBLOBData = (Byte[])(veri);
                System.IO.MemoryStream stmBLOBData = new System.IO.MemoryStream(byteBLOBData);
                return System.Drawing.Image.FromStream(stmBLOBData);
            }
            catch
            {
                return null;
            }
        }
        public string OPENFILEIMAGE()
        {
            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog() { Filter = "JPEG|*.jpg", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return ofd.FileName;
                }
                return null;
            }
        }
        /// <summary>
        /// 0 ile 100 arasında değerlere göre kırmızıdan yeşile renk verir.
        /// </summary>
        public System.Drawing.Color RED_YELLOW_GREEN_100(double percentage) //red green yellow color picker
        {
            if (percentage >= 0 && percentage <= 255)
            {
                var red = (percentage > 50 ? 1 - 2 * (percentage - 50) / 100.0 : 1.0) * 255;
                var green = (percentage > 50 ? 1.0 : 2 * percentage / 100.0) * 255;
                var blue = 0.0;
                System.Drawing.Color result = System.Drawing.Color.FromArgb((int)red, (int)green, (int)blue);
                return result;
            }
            return System.Drawing.Color.White;
        }
    }
    public partial class FUNCTIONS //ŞİFRELEME İŞLEMLERİ
    {
        public string Encrypt(string clearText)
        {
            string EncryptionKey = "ASTRAPMCEO999ALPAYLIN";
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "ASTRAPMCEO999ALPAYLIN";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = System.Text.Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
    public partial class FUNCTIONS
    {
        public void LOG_ENTER(int worker_id, string activity, DateTime activity_date)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO LOGS(worker_id,activity,activity_date) VALUES(@WORKER_ID,@ACTIVITY,@ACTIVITY_DATE)");
            try
            {
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
            }
            catch { komut.Parameters.AddWithValue("@WORKER_ID", null); }
            try
            {
                komut.Parameters.AddWithValue("@ACTIVITY", activity);
            }
            catch { komut.Parameters.AddWithValue("@ACTIVITY", null); }
            try
            {
                komut.Parameters.AddWithValue("@ACTIVITY_DATE", activity_date);
            }
            catch { komut.Parameters.AddWithValue("@ACTIVITY_DATE", GET_SERVER_DATE()); }
            SQL_EXECUTENONQUERY(komut, "Hata:LOG kaydı eklenemedi.", System.Drawing.Color.BlueViolet, 4000);
        }
        public DateTime GET_SERVER_DATE()
        {
            DateTime dt = new DateTime();
            SqlCommand komut = new SqlCommand("SELECT GETDATE()AS server_tarih");
            System.Data.DataTable table = SQL_SELECT_DATATABLE(komut, "Hata:Sunucu zamanı alınamadı.", System.Drawing.Color.Red, 4000);
            if (table != null)
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                dt = DateTime.ParseExact(table.Rows[0]["server_tarih"].ToString(), "dd.MM.yyyy HH:mm:ss", provider);
                return dt;
            }
            return dt;
        }
        /// <summary>
        /// ana form durum labelini suresiz olarak degistirir.
        /// </summary>
        public void DURUM_LABEL(string durum_Text, System.Drawing.Color durum_Renk)
        {
            a = (AnaForm)System.Windows.Forms.Application.OpenForms["AnaForm"];
            a.toolStripLabel1.Text = durum_Text;
            a.toolStripLabel1.ForeColor = durum_Renk;
        }
        bool durum_panel = true;
        /// <summary>
        /// ana form durum labelini sureli olarak degistirir.
        /// </summary>
        /// <param name="durum_Gosterim_Suresi">milisaniye</param>
        public void DURUM_LABEL(string durum_Text, System.Drawing.Color durum_Renk, int durum_Gosterim_Suresi)
        {
            a = (AnaForm)System.Windows.Forms.Application.OpenForms["AnaForm"];
            if (durum_panel)
            {
                durum_panel = false;
                string temp_text = a.toolStripLabel1.Text;
                System.Drawing.Color temp_color = a.toolStripLabel1.ForeColor;
                a.toolStripLabel1.Text = durum_Text;
                a.toolStripLabel1.ForeColor = durum_Renk;
                WaitSomeTime(durum_Gosterim_Suresi, temp_text, temp_color);
            }
        }
        public async void WaitSomeTime(int delay, string tmptxt, System.Drawing.Color tmpclr)
        {
            await System.Threading.Tasks.Task.Delay(delay);
            durum_panel = true;
            a.toolStripLabel1.Text = tmptxt;
            a.toolStripLabel1.ForeColor = tmpclr;
        }
        string sonGirilenForm;
        /// <summary>
        /// acilacak formu ANAFROM MDI'sında acar formlar_Kapatilsinmi true ise acık olan tüm formları kapatır.
        /// </summary>
        public void FORM_AC(System.Windows.Forms.Form acilacak_Form, bool formlar_Kapatilsinmi)
        {
            a = (AnaForm)System.Windows.Forms.Application.OpenForms["AnaForm"];
            if (sonGirilenForm == acilacak_Form.Name && System.Windows.Forms.Application.OpenForms[acilacak_Form.Name] != null)
            {
                DURUM_LABEL("Durum:Zaten açmak istediğiniz penceredesiniz.", System.Drawing.Color.Cyan, 1000);
            }
            else
            {
                if (formlar_Kapatilsinmi)
                {
                    for (int i = 0; i < a.MdiChildren.Length; i++)
                    {
                        a.BeginInvoke(new System.Windows.Forms.MethodInvoker(a.MdiChildren[i].Close));
                    }
                }
                acilacak_Form.MdiParent = System.Windows.Forms.Application.OpenForms["AnaForm"];
                acilacak_Form.Show();
                if (acilacak_Form.Name == new Forms.calisan().Name || acilacak_Form.Name == new Forms.AnaPanel().Name || acilacak_Form.Name == new Forms.departman().Name || acilacak_Form.Name == new sirket().Name || acilacak_Form.Name == new Forms.pozisyonlar().Name || acilacak_Form.Name == new Forms.activity().Name)
                {

                    sonGirilenForm = acilacak_Form.Name;
                }

            }

        }

    }
}

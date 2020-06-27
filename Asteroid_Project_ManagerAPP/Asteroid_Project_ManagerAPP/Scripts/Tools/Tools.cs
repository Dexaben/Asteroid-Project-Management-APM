using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_Project_ManagerAPP.Scripts.Tools
{
    public class LogTools
    {
        public static void LOG_ENTER(int worker_id, string activity, DateTime activity_date)
        {
            System.Data.SqlClient.SqlCommand komut = new System.Data.SqlClient.SqlCommand("INSERT INTO LOGS(worker_id,activity,activity_date) VALUES(@WORKER_ID,@ACTIVITY,@ACTIVITY_DATE)");
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
            catch { komut.Parameters.AddWithValue("@ACTIVITY_DATE", Scripts.SQL.SqlQueries.GET_SERVER_DATE()); }
            Scripts.SQL.SQL_COMMAND sqlCommand = new Scripts.SQL.SQL_COMMAND(komut, "Hata:LOG kaydı eklenemedi.", System.Drawing.Color.BlueViolet, 4000);
            Scripts.SQL.SqlSetQueries.SQL_EXECUTENONQUERY(sqlCommand);
        }
    }
    public class ImageTools
    {
        /// <summary>
        /// Gönderilen resmi gosteriri.
        /// </summary>
        public static void OpenImage(System.Drawing.Image image)
        {
            string myPictures = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Random rnd = new Random();
            int x = rnd.Next(1, 9999999);
            string myPath = System.IO.Path.Combine(myPictures, x + ".jpg");
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
        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap mg, System.Drawing.Size newSize)
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
        public static byte[] CONVERT_IMAGE_TO_BYTE_ARRAY(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
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
            catch { Scripts.Form.Status.STATUS_LABEL("Hata:Resim dönüştürülemedi.", System.Drawing.Color.Orange, 3500); }
            return Ret;
        }
        /// <summary>
        /// Gönderilen byte dizesini resme çevirir.
        /// </summary>
        /// <param name="veri">donusturulecek byte verisi</param>
        public static System.Drawing.Image CONVERT_BYTE_ARRAY_TO_IMAGE(object veri)
        {
            Byte[] byteBLOBData = new Byte[0];
            byteBLOBData = (Byte[])(veri);
            System.IO.MemoryStream stmBLOBData = new System.IO.MemoryStream(byteBLOBData);
            return System.Drawing.Image.FromStream(stmBLOBData);
        }
        public static string OPENFILEIMAGE()
        {
            using (System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog() { Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png", ValidateNames = true, Multiselect = false })
            {
                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return ofd.FileName;
                }
                return null;
            }
        } 
    }
    public class ColorTools
    {
        /// <summary>
        /// 0 ile 100 arasında değerlere göre kırmızıdan yeşile renk verir.
        /// </summary>
        public static System.Drawing.Color RED_YELLOW_GREEN_100(double percentage) //red green yellow color picker
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
        public static System.Drawing.Color LIGHTER_COLOR(System.Drawing.Color color, float correctionfactory = 50f)
        {
            correctionfactory = correctionfactory / 100f;
            const float rgb255 = 255f;
            return System.Drawing.Color.FromArgb((int)((float)color.R + ((rgb255 - (float)color.R) * correctionfactory)), (int)((float)color.G + ((rgb255 - (float)color.G) * correctionfactory)), (int)((float)color.B + ((rgb255 - (float)color.B) * correctionfactory))
                );
        }
        public static System.Drawing.Color DARKER_COLOR(System.Drawing.Color color, float correctionfactory = 50f)
        {
            const float hundredpercent = 100f;
            return System.Drawing.Color.FromArgb((int)(((float)color.R / hundredpercent) * correctionfactory),
                (int)(((float)color.G / hundredpercent) * correctionfactory), (int)(((float)color.B / hundredpercent) * correctionfactory));
        }
        public static System.Drawing.Color RANDOM_COLOR()
        {
            Random rnd = new Random();
            System.Drawing.Color randomColor = System.Drawing.Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
            return randomColor;
        }
    }
}

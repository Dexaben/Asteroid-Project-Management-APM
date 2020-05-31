using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_Project_ManagerAPP.Scripts.Tools
{
    class DateFunctions
    {
        public static DateTime DATETIME_CONVERTER(string DT)
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
        public static string Tarih_Converter_DAY_HOUR_MINUTE(string DT)
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
            DateTime now_date = Scripts.SQL.SqlQueries.GET_SERVER_DATE();
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
}

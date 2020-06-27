using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroid_Project_ManagerCEO.Scripts.SQL
{
    class SqlQueries
    {
        public static System.Data.DataTable SQL_SELECT_DATATABLE(SQL_COMMAND sqlCommand)
        {
            sqlCommand.SqlCommand.Connection = Scripts.SQL.SqlConnections.sqlConnection;

            System.Data.DataTable DT = new System.Data.DataTable();
            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand.SqlCommand);
                dataAdapter.Fill(DT);
            }
            catch { Form.Status.STATUS_LABEL(sqlCommand.errorText, sqlCommand.errorColor, sqlCommand.errorOnScreenTime); return null; }
            finally { SQL.SqlConnections.SQL_SERVER_DISCONNECT(); }
            return DT;
        }


        /// <summary>
        /// WORKER_ID'ye göre bir WORKER'ın calistigi pozisyonlari dizi halinde donduren fonksiyon.
        /// </summary>
        public static string[] WORKER_ROLE_CALL_BY_ID(int worker_id)
        {

            try
            {
                SqlCommand komut = new SqlCommand("SELECT worker_job FROM WORKER_POSITIONS WHERE worker_id=@WORKER_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                komut.Connection = SQL.SqlConnections.sqlConnection;
                SQL.SqlConnections.SQL_SERVER_CONNECT();
                SQL.SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);
                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(sqlCommand);
                List<string> WORKER_ROLES = new List<string>();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    WORKER_ROLES.Add(table.Rows[i]["worker_job"].ToString());
                }

                return WORKER_ROLES.ToArray();
            }
            catch
            {
                Form.Status.STATUS_LABEL("Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);

            }
            finally
            {
                SQL.SqlConnections.SQL_SERVER_DISCONNECT();
            }
            return null;
        }
        /// <summary>
        /// Tum calisanlarin WORKER_ROLE'lerini alip istedigimiz rolden kac tane oldugunu donduren fonksiyon.
        /// </summary>
        public static int WORKER_POSITION_COUNT_BY_POSITIONNAME(string worker_role)
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT COUNT(worker_job)AS worker_job_count FROM WORKER_POSITIONS WHERE worker_job=@WORKER_JOB");
                komut.Parameters.AddWithValue("@WORKER_JOB", worker_role);
                komut.Connection = SQL.SqlConnections.sqlConnection;
                SQL.SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);
                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(sqlCommand);
                if (table != null)
                {
                    return Convert.ToInt32(table.Rows[0]["worker_job_count"]);
                }
                else Form.Status.STATUS_LABEL("Hata: Veritabanında kayıtlı pozisyon bulunamadı.", System.Drawing.Color.Yellow, 2000);
            }
            catch { Form.Status.STATUS_LABEL("Hata: Veritabanından rol sayısı alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                SQL.SqlConnections.SQL_SERVER_DISCONNECT();
            }
            return 0;
        }
        public static string WORKER_NAME_BY_WORKER_ID(int worker_id)
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT worker_name FROM WORKERS WHERE worker_id=@WORKER_ID");
                komut.Parameters.AddWithValue("@WORKER_ID", worker_id);
                komut.Connection = SQL.SqlConnections.sqlConnection;
                SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);

                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(sqlCommand);
                if (table != null)
                {
                    return table.Rows[0]["worker_name"].ToString();
                }
                else Form.Status.STATUS_LABEL("Hata: Veritabanında kayıtlı çalışan bulunamadı.", System.Drawing.Color.Yellow, 2000);
            }
            catch { Form.Status.STATUS_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                SqlConnections.SQL_SERVER_DISCONNECT();
            }
            return null;
        }
        public static string PROJECT_NAME_BY_PROJECT_ID(int project_id)
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT project_name FROM PROJECTS WHERE project_id=@PROJECT_ID");
                komut.Parameters.AddWithValue("@PROJECT_ID", project_id);
                komut.Connection = SQL.SqlConnections.sqlConnection;
                SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);

                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(sqlCommand);
                if (table != null)
                {
                    return table.Rows[0]["project_name"].ToString();
                }
                else Form.Status.STATUS_LABEL("Hata: Veritabanında kayıtlı proje bulunamadı.", System.Drawing.Color.Yellow, 2000);
            }
            catch { Form.Status.STATUS_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                SqlConnections.SQL_SERVER_DISCONNECT();
            }
            return null;
        }
        /// <summary>
        /// Calisanlarin arasinda GONDERILEN role sahip calisanlarin id'sini ve ismini dict(int,string) olarak donduren fonksiyon.
        /// </summary>
        public static Dictionary<int, string> WORKER_NAME_AND_WORKER_ID_BY_WORKER_ROLE(string worker_role)
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT W.worker_id,W.worker_name,WP.worker_job FROM WORKERS W INNER JOIN WORKER_POSITIONS WP ON W.worker_id=WP.worker_id WHERE WP.worker_job=@WORKER_JOB");
                komut.Parameters.AddWithValue("@WORKER_JOB", worker_role);
                komut.Connection = SQL.SqlConnections.sqlConnection;
                SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);

                System.Data.DataTable table = new System.Data.DataTable();
                table = SQL_SELECT_DATATABLE(sqlCommand);
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
            catch { Form.Status.STATUS_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                SqlConnections.SQL_SERVER_DISCONNECT();
            }
            return null;
        }
        /// <summary>
        /// Calisan id ye gore o calisanin o rolde olup olmadigibi donduren.
        /// </summary>
        public static bool WORKER_HAS_ROLE_BY_WORKER_ID_AND_WORKER_ROLE(int worker_id, string worker_role)
        {
            try
            {

                string[] WORKER_ROLES = WORKER_ROLE_CALL_BY_ID(worker_id);

                for (int i = 0; i < WORKER_ROLES.Length; i++)
                {
                    if (WORKER_ROLES[i] == worker_role)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch { Form.Status.STATUS_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                SqlConnections.SQL_SERVER_DISCONNECT();

            }
            return false;
        }
        public static bool PROJE_YONETICISI_KONTROL(int worker_id, int project_id)
        {
            try
            {
                SqlCommand komut = new SqlCommand("SELECT P.project_manager FROM PROJECTS P WHERE P.project_id=@PROJECT_ID");
                komut.Parameters.AddWithValue("@PROJECT_ID", project_id);
                komut.Connection = SqlConnections.sqlConnection;
                SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata: Veritabanından roller alınırken hata oluştu.", System.Drawing.Color.Red, 2000);
                SqlConnections.SQL_SERVER_CONNECT();
                SqlDataReader veri = komut.ExecuteReader();
                if (veri.Read())
                {
                    if (Convert.ToInt32(veri[0]) == worker_id)
                    {
                        return true;
                    }
                }
                else return false;
            }
            catch { Form.Status.STATUS_LABEL("Hata: Veritabanından bilgi alınırken hata oluştu.", System.Drawing.Color.Red, 2000); }
            finally
            {
                SqlConnections.SQL_SERVER_DISCONNECT();

            }
            return false;
        }
        public static DateTime GET_SERVER_DATE()
        {
            DateTime dt = new DateTime();
            SqlCommand komut = new SqlCommand("SELECT GETDATE()AS server_tarih");
            komut.Connection = SQL.SqlConnections.sqlConnection;
            SQL_COMMAND sqlCommand = new SQL_COMMAND(komut, "Hata:Sunucu zamanı alınamadı.", System.Drawing.Color.Red, 4000);
            System.Data.DataTable table = Scripts.SQL.SqlQueries.SQL_SELECT_DATATABLE(sqlCommand);
            if (table != null)
            {
                System.Globalization.CultureInfo provider = System.Globalization.CultureInfo.InvariantCulture;
                try
                {
                    dt = DateTime.ParseExact(table.Rows[0]["server_tarih"].ToString(), "dd.MM.yyyy HH:mm:ss", provider);
                }
                catch
                {
                    dt = DateTime.ParseExact(table.Rows[0]["server_tarih"].ToString(), "d.MM.yyyy HH:mm:ss", provider);

                }
                return dt;
            }
            return dt;
        }
    }
}

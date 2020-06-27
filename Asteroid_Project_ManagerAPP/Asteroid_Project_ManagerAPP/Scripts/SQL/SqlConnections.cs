using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class ConnectionStringNotFindException : Exception
{
    public ConnectionStringNotFindException(string message) : base(message)
    {

    }
}
namespace Asteroid_Project_ManagerAPP.Scripts.SQL
{
   
    public class SQL_COMMAND
    {
        public SqlCommand SqlCommand;
        public string errorText;
        public System.Drawing.Color errorColor;
        public float errorOnScreenTime;
        public SQL_COMMAND(SqlCommand SqlCommand,string errorText, System.Drawing.Color errorColor, float errorOnScreenTime)
        {
            this.errorText = errorText;
            this.SqlCommand = SqlCommand;
            this.errorColor = errorColor;
            this.errorOnScreenTime = errorOnScreenTime;
        }
    }
    public static class SqlConnections
    {
        public static SqlConnection sqlConnection;
        public static void SQL_SERVER_CONFIGURATION()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConnectionStringsSection conn = config.ConnectionStrings;
            sqlConnection = new SqlConnection(conn.ConnectionStrings["ConnectionStringAPM"].ConnectionString);
            System.Diagnostics.Debug.WriteLine($"System Configuration [ConnectionString:{sqlConnection.ConnectionString}]"); //TEST
        }
        public static void SQL_SERVER_CONNECT()
        {
            if (CONNECTION_STATUS() != System.Data.ConnectionState.Open)
            {
                sqlConnection.Open();
                return;
            }
        }
        public static void SQL_SERVER_DISCONNECT()
        {
            if (CONNECTION_STATUS() != System.Data.ConnectionState.Closed)
            {
                sqlConnection.Close();
                System.Diagnostics.Debug.WriteLine($"Server Close [ConnectionString:{sqlConnection.ConnectionString}]"); //TEST
                return;
            }
        }
        public static System.Data.ConnectionState CONNECTION_STATUS()
        {
            if (sqlConnection != null)
                return sqlConnection.State;
            else return System.Data.ConnectionState.Closed;
        }
    }
}

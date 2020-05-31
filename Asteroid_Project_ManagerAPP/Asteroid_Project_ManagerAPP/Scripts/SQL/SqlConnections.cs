using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private static SqlConnection sqlConnection;
        public static SqlConnection GET_SQLCONNECTION()
        {
            SQL_SERVER_CONFIGURATION();
            if(CONNECTION_STATUS() == System.Data.ConnectionState.Open && !string.IsNullOrEmpty(sqlConnection.ConnectionString))
            return sqlConnection;
            else
            {
                SQL_SERVER_CONNECT();
                return sqlConnection;
            }
        }
        public static void SQL_SERVER_CONFIGURATION()
        {
            sqlConnection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringAPM"].ConnectionString);
            System.Diagnostics.Debug.WriteLine($"System Configuration [ConnectionString:{sqlConnection.ConnectionString}]"); //TEST
        }
        public static void SQL_SERVER_CONNECT()
        {
            if (CONNECTION_STATUS() != System.Data.ConnectionState.Open)
            {
                if(string.IsNullOrEmpty(sqlConnection.ConnectionString))
                SQL_SERVER_CONFIGURATION();
                sqlConnection.Open();
                System.Diagnostics.Debug.WriteLine($"Server Open [ConnectionString:{sqlConnection.ConnectionString}]"); //TEST
                return;
            }
            else SQL_SERVER_DISCONNECT();
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
            return sqlConnection.State;
        }
    }
}


namespace Asteroid_Project_ManagerAPP.Scripts.SQL
{
    class SqlSetQueries
    {
        public static bool SQL_EXECUTENONQUERY(SQL_COMMAND sqlCommand)
        {
            try
            {
                sqlCommand.SqlCommand.Connection = SqlConnections.GET_SQLCONNECTION();
                sqlCommand.SqlCommand.ExecuteNonQuery();
            }
            catch { Form.Status.STATUS_LABEL(sqlCommand.errorText, sqlCommand.errorColor, sqlCommand.errorOnScreenTime); return false; }
            finally { SqlConnections.SQL_SERVER_DISCONNECT(); }
            return true;
        }
    }
}

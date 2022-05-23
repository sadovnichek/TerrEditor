using MySql.Data.MySqlClient;

namespace TerrEditor.Domain.DataBase
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "f0677041.xsph.ru";
            int port = 3306;
            string database = "f0677041_terr";
            string username = "f0677041_terr";
            string password = "1TheBestT3rrEditor1"; // �������� � Env vars

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DBTerr
{
    public class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "localhost";
            int port = 3306;
            string database = "terrAssets";
            string username = "root";
            string password = "1TheBestT3rrEditor1"; //Засунуть в Env vars

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}
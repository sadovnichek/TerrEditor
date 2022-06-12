using MySql.Data.MySqlClient;

namespace TerrEditor.Domain.DataBase
{
    public class DBUtils
    {
        static string host;
        static int port;
        static string database;
        static string username;
        static string password ;

        public DBUtils()
        {
            var path = Directory.GetCurrentDirectory() + "\\data.txt";
            using StreamReader file = new StreamReader(path);
            host = file.ReadLine();
            port = Int32.Parse(file.ReadLine());
            database = file.ReadLine();
            username = file.ReadLine();
            password = file.ReadLine();
            file.Close();
        }
        public MySqlConnection GetDBConnection()
        {
            
            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}
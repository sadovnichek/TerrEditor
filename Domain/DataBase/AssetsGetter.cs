using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;
using MySQLDB;

namespace MySQLDB
{
    public class AssetsGetter
    {
        public static void GetAsset()
        {   
            // Получить соединение к базе данных.
            MySqlConnection connection = DBUtils.GetDBConnection();
            connection.Open();

            try
            {

                string sql = "SELECT assetName FROM TerrAssets WHERE id = 1";

                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                string name = cmd.ExecuteScalar().ToString();

                Console.WriteLine("Row Count affected = " + name);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                connection = null;
            }


            Console.Read();

        }
    }

}
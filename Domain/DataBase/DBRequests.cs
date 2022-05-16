using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using ImagesInteraction;

namespace DBTerr
{
    public class DBReqs
    {
        private readonly string name;
        private readonly long size;
        private Dictionary<string, Bitmap> parsedDBInfo = new();
        private MySqlConnection dBConn = DBUtils.GetDBConnection();

        public Dictionary<string, Bitmap> ParsedDBInfo { get { return this.parsedDBInfo; } private set { this.parsedDBInfo = value; } }

        public DBReqs(string name)
        {
            dBConn.Open(); // ƒолжен ли быть где-то close?
            this.name = name;
            this.size = getDBSize();
            ParseInfoFromDB();
            dBConn.Close();
        }

        private long getDBSize()
        {
            var request = $"SELECT count(*) FROM {this.name}";
            var resp = new MySqlCommand(request, dBConn);
            return (long)resp.ExecuteScalar();
        }

        private void ParseInfoFromDB()
        {
            for (var id = 1; id <= this.size; id++)
            {
                string request = $"SELECT image, name FROM {this.name} WHERE id = {id}";
                MySqlCommand resp = new MySqlCommand(request, dBConn);
                MySqlDataReader datareader = resp.ExecuteReader();
                while(datareader.Read())
                {
                    var name = datareader["name"].ToString();
                    var bImage = (byte[])datareader["image"];

                    var image = new Bitmap(Image.FromStream(new MemoryStream(bImage)));
                    image = (Bitmap)ImagesMethod.ResizeImage(image, new Size(100, 100)); //”Ѕ–ј“№

                    this.parsedDBInfo[name] = image;
                }
                datareader.Close();
            }
        } 
    }
}
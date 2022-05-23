using System.Drawing;
using MySql.Data.MySqlClient;

namespace TerrEditor.Domain.DataBase
{
    public class DBReqs
    {
        private readonly string _name;
        private readonly long _dbSize;
        private Dictionary<string, Bitmap> _parsedDbInfo = new();
        private readonly MySqlConnection _dBConn = DBUtils.GetDBConnection();
        private readonly Size _requiredSize;

        public Dictionary<string, Bitmap> ParsedDBInfo { get { return this._parsedDbInfo; } private set { this._parsedDbInfo = value; } }

        public DBReqs(string name)
        {
            _dBConn.Open();
            _name = name;
            _dbSize = getDBSize();
            ParseInfoFromDb();
            _dBConn.Close();
        }

        private long getDBSize()
        {
            var request = $"SELECT count(*) FROM {this._name}";
            var resp = new MySqlCommand(request, _dBConn);
            return (long)resp.ExecuteScalar();
        }

        private void ParseInfoFromDb()
        {
            for (var id = 1; id <= _dbSize; id++)
            {
                string request = $"SELECT image, name FROM {_name} WHERE id = {id}";
                MySqlCommand resp = new MySqlCommand(request, _dBConn);
                MySqlDataReader datareader = resp.ExecuteReader();
                while(datareader.Read())
                {
                    var name = datareader["name"].ToString();
                    var bImage = (byte[])datareader["image"];
                    var image = new Bitmap(Image.FromStream(new MemoryStream(bImage)));
                    //image = (Bitmap)ImagesMethod.ResizeImage(image, new Size(100, 100)); //ÓÁÐÀÒÜ
                    _parsedDbInfo[name] = image;
                }
                datareader.Close();
            }
        } 
    }
}
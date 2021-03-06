using System.Drawing;
using MySql.Data.MySqlClient;
using TerrEditor.Infrastructure.DataBase;

namespace TerrEditor.Infrastructure.DBRepo;

public class DatabaseCore
{
    private  long _dbSize;
    private readonly string _name;
    private readonly Dictionary<string, Bitmap> _parsedDbInfo = new();
    private MySqlConnection _dBConn;
    
    public DatabaseCore(string name)
    {
        _name = name;
    }
    private long getDBSize()
    {
        var request = $"SELECT count(*) FROM {_name}";
        var resp = new MySqlCommand(request, _dBConn);
        return (long)resp.ExecuteScalar();
    }
    public Dictionary<string, Bitmap> ParseInfoFromDB()
    {
        try
        {
            _dBConn = new DBUtils().GetDBConnection();
            _dBConn.Open();
            _dbSize = getDBSize();
        }
        catch(MySqlException e)
        {
            throw new Exception("Error occurs while connecting to database. Please check your internet connection");
        }
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
                _parsedDbInfo[name] = image;
            }
            datareader.Close();
        }
        _dBConn.Close();
        return _parsedDbInfo;
    }
}
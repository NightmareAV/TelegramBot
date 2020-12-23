using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;

namespace TelegramBot.Models
{
    public class MusicModel
    {
        private SQLiteConnection dbConnection;
        public void ConnectionToDb()
        {
            dbConnection = new SQLiteConnection("Data Source=C:\\Users\\prosu\\source\\repos\\TelegramBot\\TelegramBot\\App_Data\\telegrambot.db; Version=3");
        }
        public List<Music> musics { get; set; }
        public MusicModel()
        {
            ConnectionToDb();
            musics = new List<Music>();
        }
        public IEnumerable<Music> GetMusics()
        {
            dbConnection.Open();
            SQLiteCommand load = dbConnection.CreateCommand();
            load.CommandText = "SELECT * FROM Musics";
            SQLiteDataReader sql = load.ExecuteReader();
            while (sql.Read())
            {
                Music music = new Music();
                music.Id = Convert.ToInt32(sql["Id"]);
                music.Name = Convert.ToString(sql["Name"]);
                music.Performer = Convert.ToString(sql["Performer"]);
                music.Duration = Convert.ToInt32(sql["Duration"]);
                music.Url = Convert.ToString(sql["Url"]);
                musics.Add(music);
            }
            dbConnection.Close();
            return musics;
        }
    }
}
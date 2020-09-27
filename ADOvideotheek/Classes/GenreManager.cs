using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Data.Common;

namespace ADOvideotheek
{
    public class GenreManager
    {
        public List<Filmgenre> GetGenres()
        {
            List<Filmgenre> genres = new List<Filmgenre>();
            var manager = new VideoManager();

            using (var conVideo = manager.GetConnection())
            {
                using (var comGenres = conVideo.CreateCommand())
                {
                    comGenres.CommandType = CommandType.Text;
                    comGenres.CommandText = "SELECT * FROM Genres ORDER BY Genre";

                    conVideo.Open();
                    using (var rdrGenres = comGenres.ExecuteReader())
                    {
                        Int32 posGenreNr = rdrGenres.GetOrdinal("GenreNr");
                        Int32 posGenre = rdrGenres.GetOrdinal("Genre");

                        while (rdrGenres.Read())
                        {
                            genres.Add(new Filmgenre(
                                rdrGenres.GetInt32(posGenreNr),
                                rdrGenres.GetString(posGenre)));
                        }
                    }
                }
            }
            return genres;
        }
    }
    public class VideoManager
    {
        private static ConnectionStringSettings videoConSetting =
            ConfigurationManager.ConnectionStrings["video"];

        private static DbProviderFactory videoFactory =
            DbProviderFactories.GetFactory(videoConSetting.ProviderName);

        public DbConnection GetConnection()
        {
            DbConnection conVideo = videoFactory.CreateConnection();
            conVideo.ConnectionString = videoConSetting.ConnectionString;
            return conVideo;
        }
    }
}
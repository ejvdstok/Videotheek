using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using System.Data;

namespace ADOvideotheek
{
    public class FilmManager
    {
        public List<Film> GetFilms()
        {
            List<Film> films = new List<Film>();
            using (var conVideo = new VideoManager().GetConnection())
            {
                using (var comGetFilms = conVideo.CreateCommand())
                {
                    comGetFilms.CommandType = CommandType.Text;
                    comGetFilms.CommandText = "select BandNr, Titel, Films.GenreNr, Genres.Genre, InVoorraad, UitVoorraad, Prijs, TotaalVerhuurd " +
                        "from Films inner join Genres on Films.GenreNr=Genres.GenreNr order by Titel";
                    conVideo.Open();
                    using (var reader = comGetFilms.ExecuteReader())
                    {
                        var bandNrPos = reader.GetOrdinal("BandNr");
                        var titelPos = reader.GetOrdinal("Titel");
                        var genrenrPos = reader.GetOrdinal("GenreNr");
                        var genrePos = reader.GetOrdinal("Genre");
                        var inPos = reader.GetOrdinal("InVoorraad");
                        var uitPos = reader.GetOrdinal("UitVoorraad");
                        var prijsPos = reader.GetOrdinal("Prijs");
                        var totaalPos = reader.GetOrdinal("TotaalVerhuurd");
                        while (reader.Read())
                        {
                            films.Add(new Film(
                                reader.GetInt32(bandNrPos),
                                reader.GetString(titelPos),
                                new Filmgenre(
                                    reader.GetInt32(genrenrPos),
                                    reader.GetString(genrePos)),
                                reader.GetInt32(inPos),
                                reader.GetInt32(uitPos),
                                reader.GetDecimal(prijsPos),
                                reader.GetInt32(totaalPos)));
                        }
                    }
                }
            }
            return films;
        }

        public void VerwijderFilm(int bandnr)
        {
            using (var conVideo = new FilmDbManager().GetConnection())
            {
                using (var comDelete = conVideo.CreateCommand())
                {
                    comDelete.CommandType = CommandType.Text;
                    comDelete.CommandText = "delete from Films where BandNr=@bandnr";
                    var parBandNr = comDelete.CreateParameter();
                    parBandNr.ParameterName = "@bandnr";
                    parBandNr.Value = bandnr;
                    comDelete.Parameters.Add(parBandNr);
                    conVideo.Open();
                    if (comDelete.ExecuteNonQuery() == 0)
                        throw new System.Exception("Verwijderen mislukt");
                }
            }
        }

        public int ToevoegFilm(Film film)
        {
            using (var conVideo = new FilmDbManager().GetConnection())
            {
                using (var comInsert = conVideo.CreateCommand())
                {
                    comInsert.CommandType = CommandType.Text;
                    comInsert.CommandText = "insert into Films(Titel, GenreNr, InVoorraad, UitVoorraad, Prijs, TotaalVerhuurd) " +
                        "values (@titel, @genrenr, @in, @uit, @prijs, @totaal); select @@identity";

                    var parTitel = comInsert.CreateParameter();
                    parTitel.ParameterName = "@titel";
                    parTitel.Value = film.Titel;
                    comInsert.Parameters.Add(parTitel);

                    var parGen = comInsert.CreateParameter();
                    parGen.ParameterName = "@genrenr";
                    parGen.Value = film.Genre.GenreNr;
                    comInsert.Parameters.Add(parGen);

                    var parIn = comInsert.CreateParameter();
                    parIn.ParameterName = "@in";
                    parIn.Value = film.InVoorraad;
                    comInsert.Parameters.Add(parIn);

                    var parUit = comInsert.CreateParameter();
                    parUit.ParameterName = "@uit";
                    parUit.Value = film.UitVoorraad;
                    comInsert.Parameters.Add(parUit);

                    var parPrijs = comInsert.CreateParameter();
                    parPrijs.ParameterName = "@prijs";
                    parPrijs.Value = film.Prijs;
                    comInsert.Parameters.Add(parPrijs);

                    var parTotaal = comInsert.CreateParameter();
                    parTotaal.ParameterName = "@totaal";
                    parTotaal.Value = film.TotaalVerhuurd;
                    comInsert.Parameters.Add(parTotaal);

                    conVideo.Open();

                    var bandnr = comInsert.ExecuteScalar();
                    return Convert.ToInt32(bandnr);
                }

            }
        }
    }
}

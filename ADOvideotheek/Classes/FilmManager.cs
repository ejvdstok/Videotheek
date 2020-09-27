using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Collections.ObjectModel;

namespace ADOvideotheek
{
    public class FilmManager
    {
        private List<int> RemovedFilms = new List<int>();
        private ObservableCollection<Film> films;
        public Collection<Film> GetFilms()
        {
            if (films != null)
            {
                return films;
            }

            films = new ObservableCollection<Film>();
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
            RemovedFilms.Add(bandnr);
            using (var conVideo = new FilmDbManager().GetConnection())
            {
                using (var comDelete = conVideo.CreateCommand())
                {
                }
            }
        }

        public int MaakNieuweFilm()
        {
            Film film = new Film(
                -1, // This wont work to add it to the database, but good enough for now
                "", new Filmgenre(1, "bad"),
                0, 0, 0m, 0
          );
            film.IsNewItem = true;
            this.films.Add(film);
            return films.Count - 1;
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

        public void Synchronize()
        {
            using (var conn = new FilmDbManager().GetConnection())
            {
                conn.Open();
                using (var txn = conn.BeginTransaction())
                {
                    foreach (var film in films)
                    {
                        if (film.IsNewItem)
                        {
                            var cmd = conn.CreateCommand();
                            cmd.Transaction = txn;
                            cmd.CommandText = "INSERT INTO films (Titel,GenreNr,InVoorraad, UitVoorraad, Prijs, TotaalVerhuurd) OUTPUT INSERTED.BandNr VALUES (@Titel,@GenreNr,@InVoorraad,@UitVoorraad,@Prijs,@TotaalVerhuurd) ";

                            var parTitel = cmd.CreateParameter();
                            parTitel.ParameterName = "@Titel";
                            parTitel.Value = film.Titel;
                            cmd.Parameters.Add(parTitel);

                            var parGen = cmd.CreateParameter();
                            parGen.ParameterName = "@GenreNr";
                            parGen.Value = film.Genre.GenreNr;
                            cmd.Parameters.Add(parGen);

                            var parIn = cmd.CreateParameter();
                            parIn.ParameterName = "@InVoorraad";
                            parIn.Value = film.InVoorraad;
                            cmd.Parameters.Add(parIn);

                            var parUit = cmd.CreateParameter();
                            parUit.ParameterName = "@UitVoorraad";
                            parUit.Value = film.UitVoorraad;
                            cmd.Parameters.Add(parUit);

                            var parPrijs = cmd.CreateParameter();
                            parPrijs.ParameterName = "@Prijs";
                            parPrijs.Value = film.Prijs;
                            cmd.Parameters.Add(parPrijs);

                            var parTotaal = cmd.CreateParameter();
                            parTotaal.ParameterName = "@TotaalVerhuurd";
                            parTotaal.Value = film.TotaalVerhuurd;
                            cmd.Parameters.Add(parTotaal);

                            film.Changed = false;
                            film.IsNewItem = false;
                            film.BandNr = (int)cmd.ExecuteScalar();
                        }
                        else if (film.Changed)
                        {
                            var cmd = conn.CreateCommand();
                            cmd.Transaction = txn;
                            cmd.CommandText = "UPDATE films SET" +
                                " Titel = @Titel," +
                                " GenreNr = @GenreNr," +
                                " InVoorraad = @InVoorraad," +
                                " UitVoorraad = @UitVoorraad," +
                                " Prijs = @Prijs," +
                                " TotaalVerhuurd = @TotaalVerhuurd" +
                                " where BandNr=@BandNr";
                            var parTitel = cmd.CreateParameter();
                            parTitel.ParameterName = "@Titel";
                            parTitel.Value = film.Titel;
                            cmd.Parameters.Add(parTitel);

                            var parGen = cmd.CreateParameter();
                            parGen.ParameterName = "@GenreNr";
                            parGen.Value = film.Genre.GenreNr;
                            cmd.Parameters.Add(parGen);

                            var parIn = cmd.CreateParameter();
                            parIn.ParameterName = "@InVoorraad";
                            parIn.Value = film.InVoorraad;
                            cmd.Parameters.Add(parIn);

                            var parUit = cmd.CreateParameter();
                            parUit.ParameterName = "@UitVoorraad";
                            parUit.Value = film.UitVoorraad;
                            cmd.Parameters.Add(parUit);

                            var parPrijs = cmd.CreateParameter();
                            parPrijs.ParameterName = "@Prijs";
                            parPrijs.Value = film.Prijs;
                            cmd.Parameters.Add(parPrijs);

                            var parTotaal = cmd.CreateParameter();
                            parTotaal.ParameterName = "@TotaalVerhuurd";
                            parTotaal.Value = film.TotaalVerhuurd;
                            cmd.Parameters.Add(parTotaal);

                            var parBandNr = cmd.CreateParameter();
                            parBandNr.ParameterName = "@BandNr";
                            parBandNr.Value = film.BandNr;
                            cmd.Parameters.Add(parBandNr);

                            cmd.ExecuteNonQuery();
                            film.Changed = false;
                        }
                    }
                    foreach (var film in RemovedFilms)
                    {
                        var cmd = conn.CreateCommand();
                        cmd.Transaction = txn;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "delete from Films where BandNr=@bandnr";
                        var parBandNr = cmd.CreateParameter();
                        parBandNr.ParameterName = "@bandnr";
                        parBandNr.Value = film;
                        cmd.Parameters.Add(parBandNr);
                        if (cmd.ExecuteNonQuery() == 0)
                            throw new System.Exception("Verwijderen mislukt");

                    }
                    RemovedFilms.Clear();
                    txn.Commit();
                }
            }
        }
    }
}

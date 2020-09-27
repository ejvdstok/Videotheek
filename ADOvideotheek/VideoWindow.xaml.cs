
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ADOvideotheek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class VideoWindow : Window
    {
        private CollectionViewSource FilmViewSource;
        private FilmManager filmManager;
        public ObservableCollection<Film> Films;
        public List<Filmgenre> Genres;

        public VideoWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DbInladen();
        }

        private void DbInladen()
        {
            FilmViewSource =
                ((CollectionViewSource)(this.FindResource("filmViewSource")));
            filmManager = new FilmManager();
            var genreManager = new GenreManager();

            var Films = filmManager.GetFilms();
            FilmViewSource.Source = Films;
            //Films.CollectionChanged += this.OnCollectionChanged;

            Genres = genreManager.GetGenres();
            comboBoxGenres.ItemsSource = Genres;
            comboBoxGenres.DisplayMemberPath = "GenreNaam";
            comboBoxGenres.SelectedValuePath = "GenreNr";
            comboBoxGenres.IsEnabled = false;

            ExitEditingMode();
        }

        private void SetFieldWritability(bool writable)
        {
            comboBoxGenres.IsReadOnly = !writable;
            titelTextBox.IsReadOnly = !writable;
            prijsTextBox.IsReadOnly = !writable;
            inVooraadTextBox.IsReadOnly = !writable;

        }
        private void ExitEditingMode()
        {
            Film selectedMovie = (Film)FilmViewSource.View.CurrentItem;
            if (selectedMovie != null)
            {
                comboBoxGenres.SelectedValue = selectedMovie.Genre.GenreNr;
            }
            buttonVerwijderen.IsEnabled = true;
            buttonOpslaan.IsEnabled = true;
            buttonToevoegen.Content = "Toevoegen";
            buttonVerwijderen.Content = "Verwijderen";
            listBoxFilms.IsEnabled = true;
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var f = ((ListBox)sender).SelectedItem as Film;
            comboBoxGenres.SelectedItem = f.Genre;
        }

        private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int genNr = Convert.ToInt32(((ComboBox)sender).SelectedValue);
         
            Filmgenre gen = Genres.Where(g => g.GenreNr == genNr).First();
            Film f;
            if (gridFilmInfo.DataContext is Film)
                f = gridFilmInfo.DataContext as Film;
            else
                f = listBoxFilms.SelectedItem as Film;
            f.Genre = gen;
        }
//TOEVOEGEN EN BEVESTIGEN
        private void Toevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (buttonToevoegen.Content == "Toevoegen")
            {
                SetFieldWritability(true);
                listBoxFilms.SelectedIndex = filmManager.MaakNieuweFilm();
                buttonVerwijderen.IsEnabled = true;
                buttonOpslaan.IsEnabled = false;
                listBoxFilms.IsEnabled = false;
                comboBoxGenres.IsEnabled = true;
                comboBoxGenres.SelectedIndex = 0;
                buttonToevoegen.Content = "Bevestigen";
                buttonVerwijderen.Content = "Annuleren";
            }
            else if (buttonToevoegen.Content == "Bevestigen")
            {
                bool hasErrors = false;
                // voeg shit toe aan lijst

                hasErrors = hasErrors || CheckValidation(titelTextBox.GetBindingExpression(TextBox.TextProperty));
                hasErrors = hasErrors || CheckValidation(inVooraadTextBox.GetBindingExpression(TextBox.TextProperty));
                hasErrors = hasErrors || CheckValidation(prijsTextBox.GetBindingExpression(TextBox.TextProperty));

                if (hasErrors)
                {
                    MessageBox.Show("Ya fucked everything up, dumbass.");
                } else
                {
                    SetFieldWritability(false);
                    ExitEditingMode();
                }
            }
        } 

        private static bool CheckValidation(BindingExpression expr)
        {
            expr.UpdateSource();
            return expr.HasValidationError;
        }

//VERWIJDEREN EN ANNULEREN
        private void Verwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (buttonVerwijderen.Content == "Verwijderen")
            {
                if(MessageBox.Show("Weet u zeker dat u deze film wilt verwijderen?", "Verwijderen",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                {
                    return;
                }
                filmManager.VerwijderFilm((listBoxFilms.SelectedItem as Film).BandNr);
            }

            var films = filmManager.GetFilms();
            films.Remove(listBoxFilms.SelectedItem as Film);
            listBoxFilms.SelectedIndex = 0;
            ExitEditingMode();
        }
 //OPSLAAN

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            //try
           // {
                filmManager.Synchronize();
                ExitEditingMode();
            //}
            //catch (Exception ex)
           // {
           //     MessageBox.Show("Opslaan mislukt : " + ex.Message);
            //}
        }
 //VERHUUR
        private void Verhuur_Click(object sender, RoutedEventArgs e)
        {
            var films = filmManager.GetFilms();
            var film = (listBoxFilms.SelectedItem as Film);
            if (film.InVoorraad == 0)
            { MessageBox.Show("Alle films zijn verhuurd");
                return;
            }
            else
            {
                film.InVoorraad -= 1;
                film.UitVoorraad += 1;
                film.TotaalVerhuurd += 1;
            }
        }

        //VENSTER SLUITEN
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Wilt u alles wegschrijven naar de database ?", "Opslaan", MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                filmManager.Synchronize();
            }
        }
    }
}

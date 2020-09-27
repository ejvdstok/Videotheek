
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

            List<Film> Films = filmManager.GetFilms();
            FilmViewSource.Source = Films;
            //Films.CollectionChanged += this.OnCollectionChanged;

            Genres = genreManager.GetGenres();
            comboBoxGenres.ItemsSource = Genres;
            comboBoxGenres.DisplayMemberPath = "GenreNaam";
            comboBoxGenres.SelectedValuePath = "GenreNr";
            comboBoxGenres.IsEnabled = false;

            Update();
        }

        private void SetFieldWritability(bool writable)
        {
            comboBoxGenres.IsReadOnly = !writable;
            titelTextBox.IsReadOnly = !writable;
            prijsTextBox.IsReadOnly = !writable;
            inVooraadTextBox.IsReadOnly = !writable;
            totaalVerhuurdTextBox.IsReadOnly = !writable;
            uitgeleendTextBox.IsReadOnly = !writable;
        }
        private void Update()
        {
            Film selectedMovie = (Film)FilmViewSource.View.CurrentItem;
            if (selectedMovie != null)
            {
                comboBoxGenres.SelectedValue = selectedMovie.Genre.GenreNr;
            }
            buttonVerwijderen.IsEnabled = true;
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
                // voeg shit toe aan lijst
                Update();
            }
        } 
//VERWIJDEREN EN ANNULEREN
        private void Verwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (buttonToevoegen.Content == "Verwijderen")
            {
                if(MessageBox.Show("Weet u zeker dat u deze film wilt verwijderen?", "Verwijderen",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                { 
                }
            }

            else if (buttonToevoegen.Content == "Annuleren")
            {
                Update();
                var f = listBoxFilms.SelectedItem as Film;
                comboBoxGenres.SelectedItem = f.Genre;
            }
        }
 //OPSLAAN

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var f = listBoxFilms.SelectedItem as Film;
                new FilmManager().ToevoegFilm(f);

                f.Changed = false;
                Films.Add(f);
                FilmViewSource.View.SortDescriptions.Add(new SortDescription("Titel",
                    ListSortDirection.Ascending));
                FilmViewSource.View.Refresh();
                FilmViewSource.View.MoveCurrentTo(f);
                listBoxFilms.ScrollIntoView(listBoxFilms.SelectedItem);
                Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Opslaan mislukt : " + ex.Message);
            }
        }
 //VERHUUR
        private void Verhuur_Click(object sender, RoutedEventArgs e)
        {
            //if voorraad >0 -1 +1 +1
        }
//VENSTER SLUITEN
        /*private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) 
         * {     
         * if (MessageBox.Show("Wilt u alles wegschrijven naar de database ?", "Opslaan", MessageBoxButton.YesNo, 
         * MessageBoxImage.Question, MessageBoxResult.Yes) == MessageBoxResult.Yes)     
         * 
         * {         
         * leverancierDataGrid.CommitEdit(DataGridEditingUnit.Row, true);         
         * var manager = new FilmDbManager();         
         * List<Film> resultaatFilms = new List<Film>();         
         * StringBuilder nietgoed = new StringBuilder();         
         * StringBuilder welgoed = new StringBuilder(); 
 
        if (OudeFilms.Count > 0)         
        {             
        resultaatFilms = manager.SchrijfVerwijderingen(OudeFilms);             
        if (resultaatFilm.Count > 0)             
        {  
                foreach (var l in resultaatFilms)                 
        {                     
        nietgoed.Append("Niet verwijderd: " + l.LevNr + " : " + l.Naam + " niet\n");                
        }             
        }             
        welgoed.Append(OudeLeveranciers.Count - resultaatLevs.Count + " leverancier(s) verwijderd in de database\n");         
        } 
 
        resultaatFilms.Clear();         
        if (NieuweFilms.Count > 0)         
        {             
        resultaatLevs = manager.SchrijfToevoegingen(NieuweFilms);             
        if (resultaatLevs.Count > 0)             
        {                 
        foreach (var l in resultaatLevs)                 
        {                     
        nietgoed.Append("Niet toegevoegd: " + l.LevNr + " : " + l.Naam + " niet\n");                 
        }             
        }             
        welgoed.Append(NieuweLeveranciers.Count - resultaatLevs.Count + " leverancier(s) toegevoegd aan de database\n");        
        } 
 
        foreach (Leverancier l in leveranciersOb)         
        {             
        if ((l.Changed == true) && (l.LevNr != 0))             
        {                 
        GewijzigdeLeveranciers.Add(l);                 
        l.Changed = false;             
        }         
        } 
 
        resultaatFilms.Clear();         
        if (GewijzigdeLeveranciers.Count > 0)         
        {             
        resultaatFilms = manager.SchrijfWijzigingen(GewijzigdeFilms);             
        if (resultaatFilms.Count > 0)             
        {                 
        foreach (var l in resultaatLevs)                 
        {                    
        nietgoed.Append("Niet gewijzigd: " + l.BandNr + " : " + l.Naam +                     " niet\n");
        }             
        }             
        welgoed.Append(GewijzigdeLeveranciers.Count - resultaatLevs.Count +                  " film(s) gewijzigd in de database\n");         
        } 
 
        MessageBox.Show(nietgoed.ToString() + "\n\n" + welgoed.ToString(), "Info", MessageBoxButton.OK); 
 
        OudeFilms.Clear();         
        NieuweFilms.Clear();         
        GewijzigdeFilms.Clear(); 
 
        System.Windows.Data.CollectionViewSource leverancierViewSource = ((System.Windows.Data.CollectionViewSource)             
        (this.FindResource("leverancierViewSource")));         
        leveranciersOb = manager.GetLeveranciers();         
        leverancierViewSource.Source = leveranciersOb;     } */
    }
}

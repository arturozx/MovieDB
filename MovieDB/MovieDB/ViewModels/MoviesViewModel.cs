using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieDB.Models;
using MovieDB.Services;
using Xamarin.Forms;

namespace MovieDB.ViewModels
{
	public class MoviesViewModel : BindableObject
    {
        #region Vars and Properties
        private WebService _webService;
        #endregion

        #region Lifecycle
        public MoviesViewModel()
		{
            this._webService = new WebService();
            Movies = new ObservableCollection<MoviesByGenre>();

            FillMovies();
        }
        #endregion

        #region Methods
        private async void FillMovies()
        {
            var genres = await this._webService.GetGenres();
        
            foreach (var genre in genres)
            {
                var moviesByGenre = new MoviesByGenre();
                moviesByGenre.GenreName = genre.Name;
                moviesByGenre.MoviesList = await this._webService.GetMoviesByGenre(genre.Id);
                Movies.Add(moviesByGenre);
            }
        }

        private void OnSelectedMovie(Movie args)
        {

        }
        #endregion

        #region Binding Properties
        private ObservableCollection<MoviesByGenre> _movies;
        public ObservableCollection<MoviesByGenre> Movies
        {
            get => this._movies;
            set 
            {
                this._movies = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BindingCommands
        private ICommand _selectedMovieCommand;
        public ICommand SelectedMovieCommand => _selectedMovieCommand ??=
            new Command<Movie>(args => this.OnSelectedMovie(args));
        #endregion
    }
}


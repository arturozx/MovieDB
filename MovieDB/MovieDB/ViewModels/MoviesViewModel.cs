using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MovieDB.Models;
using MovieDB.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace MovieDB.ViewModels
{
	public class MoviesViewModel : BindableObject
    {
        #region Vars and Properties
        private WebService _webService;

        public Action<Movie> NavToMovieDetails; 
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
            //Get popular movies
            var moviesPopular = new MoviesByGenre();
            moviesPopular.GenreName = "Popular";
            moviesPopular.MoviesList = await this._webService.GetPopularMovies();
            Movies.Add(moviesPopular);

            //Get movies by genre
            var genres = await this._webService.GetGenres();
            foreach (var genre in genres)
            {
                var moviesByGenre = new MoviesByGenre();
                moviesByGenre.GenreName = genre.Name;
                moviesByGenre.MoviesList = await this._webService.GetMoviesByGenre(genre.Id);
                Movies.Add(moviesByGenre);
            }
        }

        private void OnSelectedMovie(Movie movie)
        {
            NavToMovieDetails?.Invoke(movie);
        }

        private void ExecuteRefreshCommand()
        {
            if (IsRefreshing)
                return;

            IsRefreshing = true;
            Movies.Clear();

            FillMovies();

            IsRefreshing = false;
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

        bool isRefreshing = false;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                isRefreshing = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region BindingCommands
        private ICommand _selectedMovieCommand;
        public ICommand SelectedMovieCommand => _selectedMovieCommand ??=
            new Command<Movie>(args => this.OnSelectedMovie(args));

        //this navigation is ugly but no time to create a NavigationService layer
        private ICommand _closeCommand;
        public ICommand CloseCommand => _closeCommand ??=
            new Command(() => Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync());

        private ICommand _refreshCommand;
        public ICommand RefreshCommand => _refreshCommand ??=
            new Command(ExecuteRefreshCommand);
        #endregion
    }
}


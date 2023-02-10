using Xamarin.Forms;

using MovieDB.ViewModels;
using Rg.Plugins.Popup.Services;

namespace MovieDB.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            var vm = new MoviesViewModel();
            //this navigation is ugly but no time to create a NavigationService layer
            vm.NavToMovieDetails = (movie) => {
                var movieDetailsPage = new MovieDetailsPage();
                movieDetailsPage.BindingContext = movie;
                PopupNavigation.Instance.PushAsync(movieDetailsPage);
             };
            this.BindingContext = vm;
        }
    }
}


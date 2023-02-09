using Xamarin.Forms;

using MovieDB.ViewModels;

namespace MovieDB.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new MoviesViewModel();
        }
    }
}


using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: ExportFont("Rift_Demi_Italic.otf", Alias = "RiftFont")]
[assembly: ExportFont("Roboto_Medium.ttf", Alias = "RobotoFont")]
[assembly: ExportFont("Gotham_Medium.otf", Alias = "GothamFont")]
namespace MovieDB
{
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent();

            MainPage = new Views.MainPage();
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}


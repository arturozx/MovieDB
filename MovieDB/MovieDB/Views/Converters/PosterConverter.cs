using System;
using System.Globalization;
using Xamarin.Forms;

namespace MovieDB.Views.Converters
{
    public class PosterConverter : IValueConverter
    {
        private const string IMAGE_PATH = "http://image.tmdb.org/t/p/w{0}/";

        public string ImageWith { get; set; } = "300";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{string.Format(IMAGE_PATH,ImageWith)}{value.ToString()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


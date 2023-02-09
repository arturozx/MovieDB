using System;
using System.Globalization;
using Xamarin.Forms;

namespace MovieDB.Views.Converters
{
    public class PosterConverter : IValueConverter
    {
        private const string IMAGE_PATH = "http://image.tmdb.org/t/p/w300/";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{IMAGE_PATH}{value.ToString()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


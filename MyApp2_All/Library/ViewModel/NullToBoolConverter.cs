using Avalonia.Data.Converters;
using System;
using System.Globalization;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using System.Linq;

namespace MyApp2.ViewModels
{
    public class NullToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null; // True if null, show "+"
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}


namespace MyApp2.ViewModels
{
    public class IndexConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value == null || value is not Bitmap)
                return -1;

            // Get the ItemsControl's Items collection
            var itemsControl = parameter as ItemsControl;
            if (itemsControl == null || itemsControl.ItemsSource == null)
                return -1;

            // Find the index of the item
            var items = itemsControl.ItemsSource.Cast<object>().ToList();
            return items.IndexOf(value);
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

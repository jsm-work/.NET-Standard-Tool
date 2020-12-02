using System;
using System.Windows;

namespace XAML_Converter
{
    /// <summary>
    /// Bool To Visibility
    /// </summary>
    public class BoolToVisibilityConverter : BaseOnewayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}

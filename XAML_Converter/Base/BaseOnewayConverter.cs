using System;
using System.Collections.Generic;
using System.Text;

namespace XAML_Converter
{
    public abstract class BaseOnewayConverter : System.Windows.Markup.MarkupExtension, System.Windows.Data.IValueConverter
    {
        public BaseOnewayConverter() { }

        public abstract object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}

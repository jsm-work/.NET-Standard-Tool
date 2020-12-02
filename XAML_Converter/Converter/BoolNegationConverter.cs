using System;
using System.Collections.Generic;
using System.Text;

namespace XAML_Converter
{
    class BoolNegationConverter : BaseOnewayConverter
    {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !((bool)value);
        }
    }

}

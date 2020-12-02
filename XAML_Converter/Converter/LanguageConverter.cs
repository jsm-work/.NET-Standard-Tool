using System;
using System.Collections.Generic;
using System.Text;

namespace XAML_Converter
{
    class LanguageConverter : BaseOnewayConverter
    {
		public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string key = parameter as string;
			System.Globalization.CultureInfo culture = value as System.Globalization.CultureInfo;

			if (culture == null || key == null || !culture.text.ContainsKey(key))
				return "";

			return defn.text[key];
		}
	}
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SelfMonitoringApp.Pages.Converters
{
    public class AddRemoveSuggestionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.GetType() != typeof(bool))
                return false;

            bool getText = false;
            if (parameter != null)
                if (parameter.ToString() == "text")
                    getText = true;

            bool conversionValue = (bool)value;

            if (getText)
                return conversionValue ? "-" : "+";
            else
                return conversionValue ? ColorConverters.FromHex("ff0000") : ColorConverters.FromHex("#ffc107");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.Controls
{
    public class BoolTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string trueText = string.Empty;
            string falseText = string.Empty;
            if(parameter != null && !string.IsNullOrEmpty(parameter.ToString()))
            {
                string paramString = parameter.ToString();
                if(paramString.Contains("~"))
                {
                    string[] stringValues = paramString.Split('~');
                    trueText = stringValues[0];
                    falseText = stringValues[1];
                }
            }
        
            if (value != null && value.GetType() == typeof(bool))
            {
                bool castbool = (bool)value;
                return castbool ? trueText : falseText;
            }
            else
                return "wrong input value";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

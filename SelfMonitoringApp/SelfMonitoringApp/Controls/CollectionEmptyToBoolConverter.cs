using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace SelfMonitoringApp.Controls
{
    public class CollectionEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Wrong val supplied
            if (value.GetType() != typeof(ICollection) || value is null)
                return false;

            bool invertOutput = false;
            if (parameter != null && !string.IsNullOrEmpty(parameter.ToString()))
                invertOutput = parameter.ToString() == "not";

            ICollection inputCollection = value as ICollection;
            return invertOutput ? inputCollection.Count > 0 : inputCollection.Count == 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();   
        }
    }
}

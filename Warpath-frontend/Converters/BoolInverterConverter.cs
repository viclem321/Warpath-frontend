using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace Warpath_frontend.Converters;

public class BoolInverterConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue)  return !boolValue;
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool boolValue) return !boolValue;
        return value;
    }
}

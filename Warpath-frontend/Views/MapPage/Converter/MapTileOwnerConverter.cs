using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;

namespace Warpath_frontend.Views.MapPage.Converter;


public class MapTileOwnerExistConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TileType tileType) // Vérifie que value est bien un TileMapType
        {
            if(tileType == TileType.Village)
            {
                return true;
            }
        }
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


public class MapTileOwnerColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool isMyVillage) // Vérifie que value est bien un TileMapType
        {
            if (isMyVillage)
            {
                return "Yellow";
            }
            else { return "Black";  }
        }
        return "White";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
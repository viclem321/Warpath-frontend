using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;

namespace Warpath_frontend.Views.MapPage.Converter;


public class MapTileImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TileType tileType) // Vérifie que value est bien un TileMapType
        {
            return tileType switch
            {
                TileType.Empty => "gras1.jpg",
                TileType.Village => "v1.jpg",
                _ => "default_tile.png"
            };
        }

        return "default_tile.png";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

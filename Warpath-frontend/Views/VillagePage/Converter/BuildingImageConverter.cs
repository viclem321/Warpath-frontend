using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Maui.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;
using System.Diagnostics.Metrics;

namespace Warpath_frontend.Views.VillagePage.Converter;


public class BuildingImageConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is int level && parameter is BuildingType type) // Vérifie que value est bien un TileMapType
        {
            if(level == 0) { return "emptybuilding.png"; }
            switch (type)
            {
                case BuildingType.Hq:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "hq1.png"; }
                    else if (level == 2) { return "hq2.png"; }
                    else if (level > 2) { return "hq3.png"; }
                    break;

                case BuildingType.Scierie:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "scierie1.png"; }
                    else if (level == 2) { return "scierie2.png"; }
                    else if (level > 2) { return "scierie3.png"; }
                    break;

                case BuildingType.Ferme:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "ferme1.png"; }
                    else if (level == 2) { return "ferme2.png"; }
                    else if (level > 2) { return "ferme3.png"; }
                    break;

                case BuildingType.Mine:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "mine1.png"; }
                    else if (level == 2) { return "mine2.png"; }
                    else if (level > 2) { return "mine3.png"; }
                    break;

                case BuildingType.Entrepot:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "entrepot1.png"; }
                    else if (level == 2) { return "entrepot2.png"; }
                    else if (level > 2) { return "entrepot3.png"; }
                    break;

                case BuildingType.CampMilitaire:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "campMilitaire1.png"; }
                    break;

                case BuildingType.Caserne:
                    if (level < 1) { return ""; }
                    else if (level == 1) { return "caserne1.png"; }
                    else if (level > 1) { return "caserne2.png"; }
                    break;

                default:
                    break;
            }
        }
        return "";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
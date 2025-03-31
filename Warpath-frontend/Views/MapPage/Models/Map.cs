using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;

namespace Warpath_frontend.Views.MapPage.Models;




public partial class MapPageModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<MapTilePageModel> mapTiles;


    public MapPageModel()
    {
        MapTiles = new ObservableCollection<MapTilePageModel>();
        for (int i = 0; i < CatalogueGlobal.gridDimension * CatalogueGlobal.gridDimension; i++)
        {
            MapTiles.Add(new MapTilePageModel());
        }
    }


    public void FromDto(MapDto mapDto, int pActifVillageLocation)
    {
        int counter1 = 0;
        foreach(MapTilePageModel mapTile in MapTiles)
        {
            bool isActifVillage = false; if (counter1 == pActifVillageLocation) { isActifVillage = true; }
            mapTile.FromDto(mapDto.mapTiles[counter1], counter1, isActifVillage);

            counter1++;
        }
    }


}





public partial class MapTilePageModel : ObservableObject
{
    [ObservableProperty]
    public int location;
    [ObservableProperty]
    public TileType type;
    [ObservableProperty]
    public string? name;
    [ObservableProperty]
    public string? owner;
    [ObservableProperty]
    public bool isActifVillage;



    public void FromDto(MapTileDTO mapTileDto, int pLocation, bool pIsMyActifVillage)
    {
        Location = pLocation; Type = mapTileDto.type;
        if(Type == TileType.Village) { Name = "Village"; Owner = mapTileDto.owner; } else { Name = "Empty"; Owner = "Empty"; }
        IsActifVillage = pIsMyActifVillage;
    }

}




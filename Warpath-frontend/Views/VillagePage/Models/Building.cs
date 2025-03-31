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

namespace Warpath_frontend.Views.VillagePage.Models;

public partial class BuildingPageModel : ObservableObject
{
    [ObservableProperty]
    private BuildingType? buildingName;
    [ObservableProperty]
    private int level = 0;
    [ObservableProperty]
    private bool isInConstruction = false;

    public static BuildingPageModel FromDto(BuildingDto buildingDto)
    {
        if (buildingDto is HqDTO hqDto) { return HqPageModel.FromDto(hqDto); }
        else if (buildingDto is ResourceBuildingDto resourceBuildingDto) { return ResourceBuildingPageModel.FromDto(resourceBuildingDto); }
        else if (buildingDto is EntrepotDTO entrepotDto) { return EntrepotPageModel.FromDto(entrepotDto); }
        else if (buildingDto is CampMilitaireDTO campMilitaireDTO) { return CampMilitairePageModel.FromDto(campMilitaireDTO); }
        else if (buildingDto is CaserneDTO caserneDto) { return CasernePageModel.FromDto(caserneDto); }
        else { return new BuildingPageModel(); }
    }

}


public class HqPageModel : BuildingPageModel
{
    public HqPageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction;
    }
    public static HqPageModel FromDto(HqDTO hqDto)
    {
        return new HqPageModel(BuildingType.Hq, hqDto.level, hqDto.isInConstruction);

    }
}


public class ResourceBuildingPageModel : BuildingPageModel
{
    public int quantity { get; set; }
    public DateTime lastHarvest { get; set; }

    public static ResourceBuildingPageModel FromDto(ResourceBuildingDto ressourceBuildingDto)
    {
        if (ressourceBuildingDto is ScierieDTO scierieDto) { return ScieriePageModel.FromDto(scierieDto); }
        else if (ressourceBuildingDto is FermeDTO fermeDto) { return FermePageModel.FromDto(fermeDto); }
        else if (ressourceBuildingDto is MineDTO mineDto) { return MinePageModel.FromDto(mineDto); }
        else { return new ResourceBuildingPageModel(); }
    }
}
public class ScieriePageModel : ResourceBuildingPageModel {
    public ScieriePageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction, int pQuantity, DateTime pLastHarvest)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction; quantity = pQuantity; lastHarvest = pLastHarvest;
    }
    public static ScieriePageModel FromDto(ScierieDTO scierieDTO)
    {
        return new ScieriePageModel(BuildingType.Scierie, scierieDTO.level, scierieDTO.isInConstruction, scierieDTO.quantity, scierieDTO.lastHarvest);

    }
}
public class FermePageModel : ResourceBuildingPageModel {
    public FermePageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction, int pQuantity, DateTime pLastHarvest)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction; quantity = pQuantity; lastHarvest = pLastHarvest;
    }
    public static FermePageModel FromDto(FermeDTO fermeDTO)
    {
        return new FermePageModel(BuildingType.Ferme, fermeDTO.level, fermeDTO.isInConstruction, fermeDTO.quantity, fermeDTO.lastHarvest);

    }
}
public class MinePageModel : ResourceBuildingPageModel {
    public MinePageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction, int pQuantity, DateTime pLastHarvest)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction; quantity = pQuantity; lastHarvest = pLastHarvest;
    }
    public static MinePageModel FromDto(MineDTO mineDTO)
    {
        return new MinePageModel(BuildingType.Mine, mineDTO.level, mineDTO.isInConstruction, mineDTO.quantity, mineDTO.lastHarvest);
    }
}


public partial class EntrepotPageModel : BuildingPageModel
{
    [ObservableProperty]
    private ObservableCollection<int> stock = new();

    public EntrepotPageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction, ObservableCollection<int> pStock)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction; Stock = pStock;
    }
    public static EntrepotPageModel FromDto(EntrepotDTO entrepotDTO)
    {
        ObservableCollection<int> newStock = new ObservableCollection<int>(); newStock?.Add(entrepotDTO?.stock[0] ?? 0); newStock?.Add(entrepotDTO?.stock[1] ?? 0); newStock?.Add(entrepotDTO?.stock[2] ?? 0);
        return new EntrepotPageModel(BuildingType.Entrepot, entrepotDTO.level, entrepotDTO.isInConstruction, newStock);
    }
}

public class CampMilitairePageModel : BuildingPageModel
{
    public int nSoldats; public int nSoldatsDisponible;

    public CampMilitairePageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction, int pNSoldats, int pNSoldatsDispo)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction; nSoldats = pNSoldats; nSoldatsDisponible = pNSoldatsDispo;
    }
    public static CampMilitairePageModel FromDto(CampMilitaireDTO campMilitaireDTO)
    {
        return new CampMilitairePageModel(BuildingType.CampMilitaire, campMilitaireDTO.level, campMilitaireDTO.isInConstruction, campMilitaireDTO.nSoldats, campMilitaireDTO.nSoldatsDisponible);
    }
}

public class CasernePageModel : BuildingPageModel
{
    public bool isTraining; public DateTime endTrainingAt; public int nSoldatsTraining;

    public CasernePageModel(BuildingType? pBuildingType, int pLevel, bool pIsInConstruction, bool pIsTraining, DateTime pEndTrainingAt, int pNSoldatsTrain)
    {
        BuildingName = pBuildingType; Level = pLevel; IsInConstruction = pIsInConstruction; isTraining = pIsTraining; endTrainingAt = pEndTrainingAt; nSoldatsTraining = pNSoldatsTrain;
    }
    public static CasernePageModel FromDto(CaserneDTO caserneDTO)
    {
        return new CasernePageModel(BuildingType.Caserne, caserneDTO.level, caserneDTO.isInConstruction, caserneDTO.isTraining, caserneDTO.endTrainingAt, caserneDTO.nSoldatsTraining);
    }
}

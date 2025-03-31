using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;

namespace Warpath_frontend.AppState.Models;






public class Building { 
    public string? buildingType; public int level = 0; public bool isInConstruction = false;
}

public class Hq : Building {
    public static Hq FromDto(HqDTO hqDto)
    {
        return new Hq { buildingType = hqDto.buildingType, level = hqDto.level, isInConstruction = hqDto.isInConstruction };
    }
}


public class ResourceBuilding : Building
{
    public int quantity { get; set; }
    public DateTime lastHarvest { get; set; }

    public static ResourceBuilding FromDto(ResourceBuildingDto ressourceBuildingDto)
    {
        return new ResourceBuilding { buildingType = ressourceBuildingDto.buildingType, level = ressourceBuildingDto.level, isInConstruction = ressourceBuildingDto.isInConstruction, quantity = ressourceBuildingDto.quantity, lastHarvest = ressourceBuildingDto.lastHarvest };
    }
}
public class Scierie : ResourceBuilding { }
public class Ferme : ResourceBuilding { }
public class Mine : ResourceBuilding { }


public class Entrepot : Building
{
    public List<int> stock { get; set; } = new();

    public static Entrepot FromDto(EntrepotDTO entrepotDto)
    {
        return new Entrepot { buildingType = entrepotDto.buildingType, level = entrepotDto.level, isInConstruction = entrepotDto.isInConstruction, stock = entrepotDto.stock };
    }
}

public class CampMilitaire : Building
{
    public int nSoldats; public int nSoldatsDisponible;

    public static CampMilitaire FromDto(CampMilitaireDTO campMilitaireDto)
    {
        return new CampMilitaire { buildingType = campMilitaireDto.buildingType, level = campMilitaireDto.level, isInConstruction = campMilitaireDto.isInConstruction, nSoldats = campMilitaireDto.nSoldats, nSoldatsDisponible = campMilitaireDto.nSoldatsDisponible };
    }
}

public class Caserne : Building
{
    public bool isTraining; public DateTime endTrainingAt; public int nSoldatsTraining;

    public static Caserne FromDto(CaserneDTO caserneDTO)
    {
        return new Caserne { buildingType = caserneDTO.buildingType, level = caserneDTO.level, isInConstruction = caserneDTO.isInConstruction, isTraining = caserneDTO.isTraining, endTrainingAt = caserneDTO.endTrainingAt, nSoldatsTraining = caserneDTO.nSoldatsTraining };
    }
}
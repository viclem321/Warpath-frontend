using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Views;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Warpath_frontend.Services.Api;
using Warpath_frontend.Services.AppState;
using Warpath_frontend.AppState.Models;
using Warpath_frontend.Views.VillagePage.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui;
using Newtonsoft.Json.Linq;
using System.Threading;
using Warpath_frontend.Services;


namespace Warpath_frontend.Views.VillagePage;



public partial class VillageViewModel : ObservableObject, IQueryAttributable
{
    private SignalRService _signalRService;
    public AppStateService _appStateService; private readonly UserApiService _userApiService; private readonly PlayerApiService _playerApiService; public readonly MapApiService _mapApiService; private readonly VillageApiService _villageApiService;
    private CancellationTokenSource? _cancellationTokenSource;

    [ObservableProperty]
    private VillagePageModel? village;

    [ObservableProperty] private int woodAmount;
    [ObservableProperty] private int foodAmount;
    [ObservableProperty] private int oilAmount;

    [ObservableProperty]
    private bool isLoading;




    public VillageViewModel(AppStateService appStateService, UserApiService userApiService, PlayerApiService playerApiService, MapApiService mapApiService, VillageApiService villageApiService, SignalRService signalRService)
    {
        _signalRService = signalRService;
        _appStateService = appStateService; _userApiService = userApiService; _playerApiService = playerApiService; _mapApiService = mapApiService; _villageApiService = villageApiService;
        Village = null; IsLoading = true;
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Village = null; IsLoading = true;
        Start();
    }




    public async void Start()
    {
        if( await ImportVillage() == true)
        {
            StartTimer5Secs();  return;
        }
        // if fail start
        await Shell.Current.DisplayAlert("Erreur", "Echec dans le chargement de ce village", "OK");
        CleanAppStateVillage();
        await Shell.Current.GoToAsync("//PlayerPage?param=1");
    }

    public async Task<bool> ImportVillage()
    {
        VillageDto? villageDto = await _villageApiService.ImportAllVillageDatas(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue);
        if (villageDto != null)
        {
            Village = VillagePageModel.FromDto(villageDto); if(Village != null)
            {
                bool successActualizeAppState = ActualizeAppStateVillage(); if(successActualizeAppState == true) { return true; }
            }
        }
        return false;
    }

    public bool ActualizeAppStateVillage()
    {
        CampMilitairePageModel? campMilitaire = (CampMilitairePageModel?)Village?.Buildings[(int)BuildingType.CampMilitaire]; if (campMilitaire != null)
        {
            if(_appStateService?.user?.player is Player p) { 
                p.actifVillage = new Village(campMilitaire.nSoldats, campMilitaire.nSoldatsDisponible);
                return true;
            }
        }
        return false;
    }
    public bool CleanAppStateVillage()
    {
        Player? appStatePlayer = _appStateService?.user?.player; if (appStatePlayer != null)
        {
            appStatePlayer.actifVillageLocation = int.MaxValue; appStatePlayer.actifVillage = null; return true;
        }
        return false;
    }


    private async void StartTimer5Secs()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        while (!_cancellationTokenSource.Token.IsCancellationRequested)
        {
            EntrepotPageModel? entrepot = (EntrepotPageModel?)Village?.Buildings[4];
            WoodAmount = entrepot?.Stock[0] ?? 0; FoodAmount = entrepot?.Stock[1] ?? 0; OilAmount = entrepot?.Stock[2] ?? 0;

            await Task.Delay(1000);
        }
    }
    public void StopStockUpdateTimer()
    {
        _cancellationTokenSource?.Cancel();
    }

    public void OnAppearing()
    {
        _signalRService.OnAttackNotificationReceived += ReceivNotifAttack;
    }
    public void OnDisAppearing()
    {
        _signalRService.OnAttackNotificationReceived -= ReceivNotifAttack;
    }






    [RelayCommand]
    public async Task<bool> StartUpgradeBuilding(BuildingType buildingType)
    {
        UpgradeActionDto? resultUpgrade = await _villageApiService.StartUpgradeBuilding(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue, buildingType);
        if (resultUpgrade != null)
        {
            // actualize builging
            BuildingPageModel buildingToUpgrade = Village?.Buildings[(int)resultUpgrade.buildingType] ?? new BuildingPageModel();
            buildingToUpgrade.IsInConstruction = true;
            // actualize UpgradeAction
            Village?.UpgradeAction1?.FromDto(resultUpgrade);
            // actualize entrepot
            JObject buildingCata = CatalogueGlobal.buildings[buildingType.ToString()];
            int woodCost = (int?)buildingCata?["Levels"]?[buildingToUpgrade.Level + 1]?["Cost"]?["wood"] ?? int.MaxValue;
            int foodCost = (int?)buildingCata?["Levels"]?[buildingToUpgrade.Level + 1]?["Cost"]?["food"] ?? int.MaxValue;
            int oilCost = (int?)buildingCata?["Levels"]?[buildingToUpgrade.Level + 1]?["Cost"]?["oil"] ?? int.MaxValue;
            if (Village.Buildings[(int)BuildingType.Entrepot] is EntrepotPageModel entrepot)
            {
                entrepot.Stock[(int)ResourceType.Wood] -= woodCost; entrepot.Stock[(int)ResourceType.Food] -= foodCost; entrepot.Stock[(int)ResourceType.Oil] -= oilCost;
            }
            return true;
        }
        return false;
    }


    [RelayCommand]
    public async Task<bool> EndUpgradeAction1()
    {
        bool successEndUpgrade = await _villageApiService.EndUpgradeAction1(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue);
        if (successEndUpgrade)
        {
            Village.Buildings[(int)Village.UpgradeAction1.BuildingType].Level += 1;
            Village.Buildings[(int)Village.UpgradeAction1.BuildingType].IsInConstruction = false;
            Village.UpgradeAction1.Active = false;
            return true;
        }
        return false;
    }


    [RelayCommand]
    public async Task<bool> RecoltResources(BuildingType buildingType)
    {
        int? resultRecolt = await _villageApiService.RecoltResources(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue, buildingType);
        if (resultRecolt != null)
        {
            if (Village?.Buildings[(int)buildingType] is ResourceBuildingPageModel resourceBuilding)
            {
                resourceBuilding.lastHarvest = DateTime.UtcNow;
                if (Village.Buildings[(int)BuildingType.Entrepot] is EntrepotPageModel entrepot)
                {
                    if (buildingType == BuildingType.Scierie) { entrepot.Stock[(int)ResourceType.Wood] = resultRecolt ?? 0; }
                    else if (buildingType == BuildingType.Ferme) { entrepot.Stock[(int)ResourceType.Food] = resultRecolt ?? 0; }
                    else if (buildingType == BuildingType.Mine) { entrepot.Stock[(int)ResourceType.Oil] = resultRecolt ?? 0; }
                    return true;
                }
            }
        }
        return false;
    }


    [RelayCommand]
    public async Task<bool> CaserneStartTraining(int nTroops)
    {
        (int? nTroopsTraining, DateTime? endAt) = await _villageApiService.CaserneStartTraining(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue, nTroops);
        if (nTroopsTraining != null)
        {
            if (Village?.Buildings[(int)BuildingType.Caserne] is CasernePageModel caserne )
            {
                // actualize entrepot
                JObject soldatCata = CatalogueGlobal.troops["Soldat"]; JObject? soldatCost = (JObject?)soldatCata["Cost"];
                int woodCost = (int)(soldatCost?["wood"] ?? 0); int foodCost = (int)(soldatCost?["food"] ?? 0); int oilCost = (int)(soldatCost?["oil"] ?? 0);
                if (Village?.Buildings[(int)BuildingType.Entrepot] is EntrepotPageModel entrepot)
                {
                    entrepot.Stock[(int)ResourceType.Wood] -= woodCost * nTroops; entrepot.Stock[(int)ResourceType.Food] -= foodCost * nTroops; entrepot.Stock[(int)ResourceType.Oil] -= oilCost * nTroops;
                }
                // update caserne
                caserne.isTraining = true;
                caserne.nSoldatsTraining = nTroops;
                caserne.endTrainingAt = endAt ?? DateTime.UtcNow;
            }
            return true;
        }
        return false;
    }

    [RelayCommand]
    public async Task<bool> CaserneEndTraining(bool nothing)
    {
        bool successEndTraining = await _villageApiService.CaserneEndTraining(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue);
        if (successEndTraining)
        {
            if (Village?.Buildings[(int)BuildingType.Caserne] is CasernePageModel caserne)
            {
                if (Village?.Buildings[(int)BuildingType.CampMilitaire] is CampMilitairePageModel campMilitaire)
                {
                    campMilitaire.nSoldats += caserne.nSoldatsTraining; campMilitaire.nSoldatsDisponible += caserne.nSoldatsTraining;
                }
                caserne.isTraining = false; caserne.nSoldatsTraining = 0;
            }
            return true;
        }
        return false;
    }

    private void ReceivNotifAttack(string message)
    {
        var popup = new Components.PopupReceivAttackk(message, this);
        Application.Current.Windows[0].Page?.ShowPopup(popup);
    }







    [RelayCommand]
    public void OpenBuildingPopup(BuildingType buildingType)
    {
        var popup = new Components.PopupBuilding(Village?.Buildings[(int)buildingType] ?? new BuildingPageModel(), this);
        Application.Current.Windows[0].Page?.ShowPopup(popup);
    }


    [RelayCommand]
    public async Task GoToMap()
    {
        if (IsLoading) { return; }
        // open isLoading popup
        IsLoading = true;
        await Task.Delay(100);
        bool successActualizeAppState = ActualizeAppStateVillage(); if(successActualizeAppState)
        {
            await Shell.Current.GoToAsync("//MapPage?param=1");
        }
        IsLoading = false;
    }



}


using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Maui.Views;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Warpath_frontend.Services.Api;
using Warpath_frontend.Services.AppState;
using Warpath_frontend.AppState.Models;
using Warpath_frontend.Views.MapPage.Models;
using Warpath_frontend.Services;
using System.Diagnostics;


namespace Warpath_frontend.Views.MapPage;



public partial class MapViewModel : ObservableObject, IQueryAttributable
{
    private SignalRService _signalRService;
    public AppStateService _appStateService; private readonly UserApiService _userApiService; private readonly PlayerApiService _playerApiService; private readonly VillageApiService _villageApiService; public readonly MapApiService _mapApiService;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private MapPageModel map;

    [ObservableProperty]
    private int dimensionsGrid;
    [ObservableProperty]
    private int dimensionsCase;

    [ObservableProperty]
    private int nSoldatsDispo;




    public MapViewModel(AppStateService appStateService, UserApiService userApiService, PlayerApiService playerApiService, VillageApiService villageApiService, MapApiService mapApiService, SignalRService signalRService)
    {
        _signalRService = signalRService;
        _appStateService = appStateService; _userApiService = userApiService; _playerApiService = playerApiService; _villageApiService = villageApiService; _mapApiService = mapApiService;
        DimensionsGrid = CatalogueGlobal.gridDimension * 100; DimensionsCase = DimensionsGrid / CatalogueGlobal.gridDimension; nSoldatsDispo = 0;
        IsLoading = true; Map = new MapPageModel();
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        IsLoading = true;
        Start();
    }





    public async void Start()
    {
        await ImportMap();
        NSoldatsDispo = _appStateService.user?.player?.actifVillage?.nSoldatsDispo ?? 0;
    }

    public async Task<bool> ImportMap()
    {
        MapDto? mapDTO = await _mapApiService.ImportAllMap(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "");
        if (mapDTO != null)
        {
            Map.FromDto(mapDTO, _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue);
            return true;
        }
        return false;
    }


    public void OnAppearing()
    {
        _signalRService.OnAttackNotificationReceived += ReceivNotifAttack;
    }
    public void OnDisAppearing()
    {
        _signalRService.OnAttackNotificationReceived -= ReceivNotifAttack;
    }

    private void ReceivNotifAttack(string message)
    {
        ActualizeAppStateCampMilitaire();
        var popup = new Components.PopupReceivAttack(message, this);
        Application.Current.Windows[0].Page?.ShowPopup(popup);
    }





    [RelayCommand]
    public void OpenInfosCasePopup(MapTilePageModel mapTile)
    {
        var popup = new Components.PopupInfosCase(mapTile, this);
        Application.Current.Windows[0].Page?.ShowPopup(popup);
    }



    [RelayCommand]
    public async Task<RapportFightDto?> AttackVillage(int location)
    {
        int nSoldatsToSend = _appStateService.user?.player?.actifVillage?.nSoldatsDispo ?? 0;
        RapportFightDto? rapportAttack = await _mapApiService.AttackVillage(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue, location, nSoldatsToSend);;
        if (rapportAttack != null)
        {
            await ActualizeAppStateCampMilitaire();
            return rapportAttack;
        }
        return null;
    }


    [RelayCommand]
    public async Task<bool> ActualizeAppStateCampMilitaire()
    {
        CampMilitaireDTO? campMilitaireDTO = await _villageApiService.GetCampMilitaireDatas(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", _appStateService.user?.player?.actifVillageLocation ?? int.MaxValue); if(campMilitaireDTO != null)
        {
            if (_appStateService?.user?.player?.actifVillage is Village v)
            {
                v.nSoldats = campMilitaireDTO.nSoldats; v.nSoldatsDispo = campMilitaireDTO.nSoldatsDisponible; 
                NSoldatsDispo = _appStateService.user?.player?.actifVillage?.nSoldatsDispo ?? 0; return true;
            }
        }
        return false;
    }

    [RelayCommand]
    public async Task EnterInMyVillage()
    {
        if (IsLoading) { return; }
        IsLoading = true;
        await Task.Delay(50);
        await Shell.Current.GoToAsync("//VillagePage?param=1");
        IsLoading = false;
    }

}



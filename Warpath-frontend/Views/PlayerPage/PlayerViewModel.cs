using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Warpath.Shared.DTOs;
using Warpath_frontend.Services.Api;
using Warpath_frontend.Services.AppState;
using Warpath_frontend.AppState.Models;


namespace Warpath_frontend.Views.PlayerPage;



public partial class PlayerViewModel : ObservableObject, IQueryAttributable
{
    public AppStateService _appStateService; private readonly UserApiService _userApiService; private readonly PlayerApiService _playerApiService;


    [ObservableProperty]
    private string villagesLocationText;
    [ObservableProperty]
    private string userVillageChoice;
    [ObservableProperty]
    private string newVillageChoice;




    public PlayerViewModel(AppStateService appStateService, UserApiService userApiService, PlayerApiService playerApiService) {
        _appStateService = appStateService; _userApiService = userApiService; _playerApiService = playerApiService;
        VillagesLocationText = ""; UserVillageChoice = ""; NewVillageChoice = "";
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        VillagesLocationText = ""; UserVillageChoice = ""; NewVillageChoice = "";
        Start();
    }


    public async void Start()
    {
        if (await CreatePlayerIfDoesntExist())
        {
            if(await ImportAndDisplayAllVillages())
            {
                return;
            }
        }
        // else
        await Shell.Current.DisplayAlert("Erreur", "Echec, veuillez vous reconnecter", "OK");
        _appStateService.UserDisconnect();
        await Shell.Current.GoToAsync("//LoginPage?param=1");
    }

    public async Task<bool> CreatePlayerIfDoesntExist()
    {
        // if player doesnt exist, create one
        string? playerPseudo = await _userApiService.ImportUserDatasAsync(_appStateService.user?.token ?? "", _appStateService.user?.username ?? ""); if (playerPseudo == null)
        {
            bool successCreatePlayer = await _userApiService.CreatePlayerAsync(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.username ?? "");
            if (successCreatePlayer) { playerPseudo = _appStateService.user?.username; } else { return false; }
        }
        // actualize AppState with player datas
        if (_appStateService.user != null) { _appStateService.user.playerPseudo = playerPseudo; }
        return true;
    }

    public async Task<bool> ImportAndDisplayAllVillages()
    {
        PlayerDto? playerDto = await _playerApiService.ImportAllPlayerDatas(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "");
        if(playerDto != null && playerDto.pseudo == _appStateService.user?.playerPseudo)
        {
            Player player = new Player(playerDto.pseudo ?? "", playerDto.allMapVillages ?? new List<int>(), int.MaxValue, null);
            if(_appStateService.user != null) _appStateService.user.player = player;
            // display
            VillagesLocationText = string.Join(", ", _appStateService.user?.player?.allVillagesLocation ?? new List<int>());
            return true;
        }
        return false;
    }




    [RelayCommand]
    public async Task EnterInVillage()
    {
        if (int.TryParse(UserVillageChoice, out int choiceVillageLocation))
        {
            if(_appStateService.user?.player is Player p)
            {
                if( p.allVillagesLocation.Contains(choiceVillageLocation) )
                {
                    p.actifVillageLocation = choiceVillageLocation;
                    await Shell.Current.GoToAsync("//VillagePage?param=1");
                    return;
                }
            }
        }
        await Shell.Current.DisplayAlert("Erreur", "Aucun de vos village n'est situé à cet endroit.", "OK");
        return;
    }


    [RelayCommand]
    public async Task CreateNewVillage()
    {
        if (int.TryParse(NewVillageChoice, out int newVillageInt))
        {
            bool successCreateVillage = await _playerApiService.CreateNewVillageAsync(_appStateService.user?.token ?? "", _appStateService.user?.username ?? "", _appStateService.user?.playerPseudo ?? "", newVillageInt); if (successCreateVillage)
            {
                await ImportAndDisplayAllVillages(); return;
            }
        }
        await Shell.Current.DisplayAlert("Erreur", "Impossible de creer un village a cette position", "OK");
    }


}
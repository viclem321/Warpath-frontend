using Warpath_frontend.Services.Api;
using Warpath_frontend.Services.AppState;
using Warpath_frontend.Services;
using Warpath_frontend.AppState.Models;

namespace Warpath_frontend.Views.LoginPage
{
    public partial class LoginPage : ContentPage
    {
        private AppStateService _appStateService; private readonly UserApiService _userApiService;
        private readonly SignalRService _signalRService;

        public LoginPage(AppStateService appStateService, UserApiService userApiService, SignalRService signalRService)
        {
            InitializeComponent();
            _appStateService = appStateService; _userApiService = userApiService; _signalRService = signalRService;
        }



        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = entryUsername.Text;
            string password = entryPassword.Text;

            (bool successLogin, string? username1, string? token) = await _userApiService.LoginAsync(username, password);

            if(successLogin && username1 != null && token != null)
            {
                string? playerPseudo = await _userApiService.ImportUserDatasAsync(token, username1);
                User newUser = new User(username1, token, playerPseudo, null);
                _appStateService.UserLogin(newUser);
                //Connection au SignalR
                await _signalRService.ConnectAsync();
                _signalRService.ListenForMessages();
                await Shell.Current.GoToAsync("//PlayerPage?param=1");
            } else
            {
                await DisplayAlert("Erreur", "Echec, veuillez reesayer.", "OK");
            }
        }


        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//RegisterPage?param=1");
        }





        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
            {
                border.WidthRequest = width * 0.95; // 95% de la largeur de l'écran
            }
            else { border.WidthRequest = 330; }
        }

    }
}

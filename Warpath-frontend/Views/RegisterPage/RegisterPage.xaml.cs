using Warpath_frontend.Services.Api;
using Warpath_frontend.Services.AppState;
using Warpath_frontend.AppState.Models;

namespace Warpath_frontend.Views.RegisterPage
{


    public partial class RegisterPage : ContentPage
    {
        private AppStateService _appStateService; private readonly UserApiService _userApiService;
        public RegisterPage(AppStateService appStateService, UserApiService userApiService)
        {
            InitializeComponent();
            _appStateService = appStateService; _userApiService = userApiService;
        }


        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string username = entryUsername.Text;
            string password = entryPassword.Text;

            bool successRegister = await _userApiService.RegisterAsync(username, password);

            if (successRegister)
            {
                labelMessage.Text = "Inscription réussie !";
                await Task.Delay(1000); // Attente 1 seconde pour l'effet
                await Shell.Current.GoToAsync("//LoginPage"); // Rediriger vers la page de connexion (si elle existe)
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez reesayer.", "OK");
            }
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
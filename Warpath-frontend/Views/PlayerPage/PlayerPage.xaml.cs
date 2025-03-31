

namespace Warpath_frontend.Views.PlayerPage;


public partial class PlayerPage : ContentPage
{
    public PlayerPage(PlayerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        //update title of the page
        string title = viewModel._appStateService.user?.playerPseudo ?? "Your Page";
        TitlePage.Text = "Bonjour, " + title;
    }



    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
        {
            border1.WidthRequest = width * 0.98;
        }
        else { border1.WidthRequest = 600; }
    }

}

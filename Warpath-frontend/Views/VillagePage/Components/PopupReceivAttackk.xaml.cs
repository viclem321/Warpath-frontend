using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Warpath_frontend.Views.VillagePage.Models;
using Warpath_frontend.Views.MapPage.Components;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class PopupReceivAttackk : Popup
{
    string idRapport; RapportFightDto? lastAttackRapport;
    public PopupReceivAttackk(string pIdRapport, VillageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel; idRapport = pIdRapport;
    }



    private async void Button_OpenRapport_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel pViewModel)
        {
            lastAttackRapport = await pViewModel._mapApiService.GetRapport(pViewModel._appStateService?.user?.token ?? "", pViewModel?._appStateService?.user?.username ?? "", pViewModel?._appStateService?.user?.playerPseudo ?? "", idRapport);
            var popup = new PopupRapportFight(lastAttackRapport ?? new RapportFightDto(), false);
            Application.Current.MainPage.ShowPopup(popup);
        }
    }

    void OnCloseButtonClicked(object sender, EventArgs e) => Close();
}
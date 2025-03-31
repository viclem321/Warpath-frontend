using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Warpath_frontend.Views.MapPage.Models;

namespace Warpath_frontend.Views.MapPage.Components;

public partial class PopupInfosCase : Popup
{
    MapTilePageModel mapTile; RapportFightDto? lastAttackRapport;
	public PopupInfosCase(MapTilePageModel pMapTile, MapViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        mapTile = pMapTile;
        if(mapTile.Type == TileType.Empty) { villagePopup.IsVisible = false; EmptyPopup.IsVisible = true; }
        else if (mapTile.Type == TileType.Village) { 
            villagePopup.IsVisible = true; EmptyPopup.IsVisible = false; buttonOpenRapport.IsVisible = false;
            if(viewModel._appStateService.user?.player?.actifVillageLocation == mapTile.Location) { itsMyVillage.IsVisible = true; itsNotMyVillage.IsVisible = false; }
            else { itsMyVillage.IsVisible = false; itsNotMyVillage.IsVisible = true; }
        }
    }



    private async void Button_EnterInVillage_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();

        if (BindingContext is MapViewModel pViewModel)
        {
            await pViewModel.EnterInMyVillage();
        }
    }


    private async void Button_AttackVillage_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is MapViewModel pViewModel)
        {
            RapportFightDto? newRapport = await pViewModel.AttackVillage(mapTile.Location); if(newRapport != null)
            {
                lastAttackRapport = newRapport;
                resultOperation.Text = "Vous avez attaquez " + mapTile.Owner; buttonOpenRapport.IsVisible = true;
                return;
            }
            resultOperation.Text = "Impossible d'attaquer ce village"; buttonOpenRapport.IsVisible = false; return;
        }
    }

    private async void Button_OpenRapport_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is MapViewModel pViewModel)
        {
            var popup = new PopupRapportFight(lastAttackRapport ?? new RapportFightDto(), true);
            Application.Current.MainPage.ShowPopup(popup);
        }
    }

    void OnCloseButtonClicked(object sender, EventArgs e) => Close();
}
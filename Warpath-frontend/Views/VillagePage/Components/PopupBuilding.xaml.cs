using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;


namespace Warpath_frontend.Views.VillagePage.Components;

public partial class PopupBuilding : Popup
{
	BuildingPageModel building;
	public PopupBuilding(BuildingPageModel pBuilding, VillageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        building = pBuilding;
		nameBuilding.Text = building.BuildingName.ToString();
		levelBuilding.Text = building.Level.ToString();
        // charger la view du bon batiment
        if (building.Level == 0) { BuildingContent.Content = new VoidBuildingView(viewModel); }
        else
        {
            switch (building.BuildingName)
            {
                case BuildingType.Hq:
                    BuildingContent.Content = new HqView(viewModel, (HqPageModel)building);
                    break;
                case BuildingType.Scierie:
                    BuildingContent.Content = new ScierieView(viewModel, (ScieriePageModel)building);
                    break;
                case BuildingType.Ferme:
                    BuildingContent.Content = new FermeView(viewModel, (FermePageModel)building);
                    break;
                case BuildingType.Mine:
                    BuildingContent.Content = new MineView(viewModel, (MinePageModel)building);
                    break;
                case BuildingType.Entrepot:
                    BuildingContent.Content = new EntrepotView(viewModel, (EntrepotPageModel)building);
                    break;
                case BuildingType.CampMilitaire:
                    BuildingContent.Content = new CampMilitaireView(viewModel, (CampMilitairePageModel)building);
                    break;
                case BuildingType.Caserne:
                    BuildingContent.Content = new CaserneView(viewModel, (CasernePageModel)building);
                    this.Closed += (s, e) => ((CaserneView)BuildingContent.Content).StopTimer();
                    break;
                default:
                    BuildingContent.Content = new Label { Text = "Bâtiment inconnu", HorizontalOptions = LayoutOptions.Center };
                    break;
            }
        }
    }



    void OnCloseButtonClicked(object sender, EventArgs e) => Close();


    private void Button_Upgrade_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel viewModel)
        {
            viewModel.StartUpgradeBuildingCommand.Execute(building.BuildingName);
        }
    }
}
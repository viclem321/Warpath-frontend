using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class MineView : ContentView
{
    MinePageModel mine;
    public MineView(VillageViewModel viewModel, MinePageModel pMine)
    {
        InitializeComponent();
        BindingContext = viewModel;
        mine = pMine;
        int? production = (int?)CatalogueGlobal.buildings?["Mine"]?["Levels"]?[mine.Level]?["Production"];
        int? capacity = (int?)CatalogueGlobal.buildings?["Mine"]?["Levels"]?[mine.Level]?["Capacity"];
        LabelProductionAmount.Text = "La production de fer est de " + production;
        LabelCapacityAmount.Text = "La capacité est de " + capacity;
    }


    public async void Recolt_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel viewModel)
        {
            await viewModel.RecoltResourcesCommand.ExecuteAsync(mine.BuildingName);
        }
    }
}
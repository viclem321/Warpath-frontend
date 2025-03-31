using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class ScierieView : ContentView
{
    ScieriePageModel scierie;
    public ScierieView(VillageViewModel viewModel, ScieriePageModel pScierie)
    {
        InitializeComponent();
        BindingContext = viewModel;
        scierie = pScierie;
        int? production = (int?)CatalogueGlobal.buildings?["Scierie"]?["Levels"]?[scierie.Level]?["Production"];
        int? capacity = (int?)CatalogueGlobal.buildings?["Scierie"]?["Levels"]?[scierie.Level]?["Capacity"];
        LabelProductionAmount.Text = "La production de bois est de " + production;
        LabelCapacityAmount.Text = "La capacité est de " + capacity;
    }


    public async void Recolt_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel viewModel)
        {
            await viewModel.RecoltResourcesCommand.ExecuteAsync(scierie.BuildingName);
        }
    }
}
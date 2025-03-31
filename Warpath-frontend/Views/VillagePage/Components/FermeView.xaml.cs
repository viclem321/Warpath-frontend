using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class FermeView : ContentView
{
    FermePageModel ferme;
    public FermeView(VillageViewModel viewModel, FermePageModel pFerme)
    {
        InitializeComponent();
        BindingContext = viewModel;
        ferme = pFerme;
        int? production = (int?)CatalogueGlobal.buildings?["Ferme"]?["Levels"]?[ferme.Level]?["Production"];
        int? capacity = (int?)CatalogueGlobal.buildings?["Ferme"]?["Levels"]?[ferme.Level]?["Capacity"];
        LabelProductionAmount.Text = "La production d'aliments est de " + production;
        LabelCapacityAmount.Text = "La capacité est de " + capacity;
    }


    public async void Recolt_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel viewModel)
        {
            await viewModel.RecoltResourcesCommand.ExecuteAsync(ferme.BuildingName);
        }
    }
}
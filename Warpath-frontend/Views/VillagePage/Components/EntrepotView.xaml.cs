using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class EntrepotView : ContentView
{
    EntrepotPageModel entrepot;
    public EntrepotView(VillageViewModel viewModel, EntrepotPageModel pEntrepot)
    {
        InitializeComponent();
        BindingContext = viewModel;
        entrepot = pEntrepot;

        int? capacity = (int?)CatalogueGlobal.buildings?["Entrepot"]?["Levels"]?[entrepot.Level]?["Capacity"];
        LabelStockBois.Text = " " + entrepot.Stock[(int)ResourceType.Wood];
        LabelStockAliments.Text = " " + entrepot.Stock[(int)ResourceType.Food];
        LabelStockPetrole.Text = " " + entrepot.Stock[(int)ResourceType.Oil];
        LabelCapacityAmount.Text = "La capacité est de " + capacity;
    }

}
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class VoidBuildingView : ContentView
{
	public VoidBuildingView(VillageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
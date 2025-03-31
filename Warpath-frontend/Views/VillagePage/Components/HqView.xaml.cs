using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class HqView : ContentView
{
	HqPageModel hq;
	public HqView(VillageViewModel viewModel, HqPageModel pHq)
	{
		InitializeComponent();
        BindingContext = viewModel;
		hq = pHq;
    }
}
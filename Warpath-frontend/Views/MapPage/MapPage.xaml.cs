using System.Diagnostics;
using Warpath_frontend.Services;

namespace Warpath_frontend.Views.MapPage;

public partial class MapPage : ContentPage
{
    private SignalRService _signalRService;

    public MapPage(MapViewModel viewModel, SignalRService signalRService)
	{
        InitializeComponent();
        BindingContext = viewModel; _signalRService = signalRService;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Vérifie que le BindingContext est bien un ViewModel
        if (BindingContext is MapViewModel viewModel)
        {
            Dispatcher.Dispatch(async () =>
            {
                await Task.Delay(600);
                viewModel.IsLoading = false;
            });
            viewModel.OnAppearing();
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is MapViewModel viewModel)
        {
            viewModel.OnDisAppearing();
        }
    }



}
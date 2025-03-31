using Warpath_frontend.Services;

namespace Warpath_frontend.Views.VillagePage;



public partial class VillagePage : ContentPage
{
    private SignalRService _signalRService;
    public VillagePage(VillageViewModel viewModel, SignalRService signalRService)
    {
        InitializeComponent();
        BindingContext = viewModel; _signalRService = signalRService;
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Vérifie que le BindingContext est bien un ViewModel
        if (BindingContext is VillageViewModel viewModel)
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
        if (BindingContext is VillageViewModel viewModel)
        {
            viewModel.StopStockUpdateTimer();
            viewModel.OnDisAppearing();
        }

    }


}

using System.Diagnostics;
using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class CaserneView : ContentView
{
    CasernePageModel caserne; private IDispatcherTimer? _timer;
    public CaserneView(VillageViewModel viewModel, CasernePageModel pCaserne)
    {
        InitializeComponent();
        BindingContext = viewModel;
        caserne = pCaserne; _timer = null;
        ActualizeDisplay();
        StartTimer();
    }

    private void StartTimer()
    {
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s, e) => OnTimerTick();
        _timer.Start();
    }


    private void OnTimerTick()
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            ActualizeDisplay();
        });
    }


    public void ActualizeDisplay()
    {
        if (caserne.isTraining) 
        {
            SectionIfTroopArentTraining.IsVisible = false; SectionIfTroopTraining.IsVisible = true;

            var remaining = caserne.endTrainingAt - DateTime.UtcNow;
            // if finish
            if (remaining.TotalSeconds <= 0)
            {
                ifEndTraining.IsVisible = true; ifNotEndTraining.IsVisible = false;
                labelEndTraining.Text = caserne.nSoldatsTraining + " soldats sont prêt à combattre";
            }
            else
            {
                ifEndTraining.IsVisible = false; ifNotEndTraining.IsVisible = true;
                LabelIfNotEndTraining.Text = caserne.nSoldatsTraining + " soldats s'entrainent";
                LabelIfNotEndTrainingTimer.Text = $"{remaining.Minutes:D2}:{remaining.Seconds:D2}";
            }
        }
        else { SectionIfTroopArentTraining.IsVisible = true; SectionIfTroopTraining.IsVisible = false; }
    }


    public async void Train_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel viewModel)
        {
            if (int.TryParse(entryNTroopsToTrain.Text, out int nTroops))
            {
                entryNTroopsToTrain.Text = "";
                await viewModel.CaserneStartTrainingCommand.ExecuteAsync(nTroops);
            }
        }
        ActualizeDisplay();
    }
    public async void EndTraining_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is VillageViewModel viewModel)
        {
            await viewModel.CaserneEndTrainingCommand.ExecuteAsync(true);
        }
        ActualizeDisplay();
    }



    public void StopTimer()
    {
        _timer?.Stop();
    }
}
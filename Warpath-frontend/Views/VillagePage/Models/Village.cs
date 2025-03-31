using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;

namespace Warpath_frontend.Views.VillagePage.Models;


public partial class VillagePageModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<BuildingPageModel> buildings;

    [ObservableProperty]
    private UpgradeActionPageModel upgradeAction1;

    public VillagePageModel()
    {
        Buildings = new ObservableCollection<BuildingPageModel>(); upgradeAction1 = new UpgradeActionPageModel();
    }
    public VillagePageModel(ObservableCollection<BuildingPageModel> pBuildings, UpgradeActionPageModel pUpgradeAction)
    {
        Buildings = pBuildings; UpgradeAction1 = pUpgradeAction;
    }

    public static VillagePageModel? FromDto(VillageDto villageDto)
    {
        VillagePageModel newVillage = new VillagePageModel();
        foreach (BuildingDto buildingDto in villageDto.buildings)
        {
            newVillage.Buildings.Add(BuildingPageModel.FromDto(buildingDto));
        }
        newVillage.UpgradeAction1.FromDto(villageDto.upgradeAction1);
        return newVillage;
    }
}





public partial class UpgradeActionPageModel : ObservableObject
{
    [ObservableProperty]
    public bool active;
    // if activ
    [ObservableProperty]
    public BuildingType buildingType;
    [ObservableProperty]
    public bool finish;
    [ObservableProperty]
    public DateTime endUpgradeAt;
    // if not finish
    [ObservableProperty]
    private string timeRemaining;
    private Timer? _timer;

    public UpgradeActionPageModel()
    {
        Active = false; BuildingType = BuildingType.Hq; Finish = false; EndUpgradeAt = new DateTime(); TimeRemaining = ""; _timer = null;
    }

    public bool FromDto(UpgradeActionDto upgradeActionDto)
    {
        Active = upgradeActionDto.active;  BuildingType = upgradeActionDto.buildingType; 
        EndUpgradeAt = upgradeActionDto.endUpgradeAt; var remaining = EndUpgradeAt - DateTime.UtcNow; if (remaining.TotalSeconds <= 0) { Finish = true; } else { Finish = false; }
        if(Active) { StartTimer(); } 
        return true;

    }

    public void StartTimer()
    {
        _timer?.Dispose();
        _timer = new Timer(UpdateTimeRemaining, null, 0, 1000);
    }
    private void UpdateTimeRemaining(object? state)
    {
        if (Active)
        {
            var remaining = EndUpgradeAt - DateTime.UtcNow;

            if (remaining.TotalSeconds <= 0)
            {
                _timer?.Dispose();
                TimeRemaining = "Terminé !";
                Finish = true;
            }
            else
            {
                TimeRemaining = $"{remaining.Minutes:D2}:{remaining.Seconds:D2}";
            }
        }
    }
}

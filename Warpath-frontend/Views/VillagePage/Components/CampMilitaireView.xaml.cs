using Warpath.Shared.Catalogue;
using Warpath_frontend.Views.VillagePage.Models;

namespace Warpath_frontend.Views.VillagePage.Components;

public partial class CampMilitaireView : ContentView
{
    CampMilitairePageModel campMilitaire;
    public CampMilitaireView(VillageViewModel viewModel, CampMilitairePageModel pCampMilitaire)
    {
        InitializeComponent();
        BindingContext = viewModel;
        campMilitaire = pCampMilitaire;
        LabelNSoldats.Text = "Vous possèdez " + campMilitaire.nSoldats + " soldats";
        LabelNSoldatsDispo.Text = campMilitaire.nSoldatsDisponible + " sont disponible";
    }

}
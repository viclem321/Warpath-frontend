using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Warpath_frontend.Views.MapPage.Models;

namespace Warpath_frontend.Views.MapPage.Components;

public partial class PopupRapportFight : Popup
{
    RapportFightDto rapport;
    public PopupRapportFight(RapportFightDto pRapport, bool areYouAttacker)
    {
        InitializeComponent();
        rapport = pRapport;

        labelAttaquant.Text = " " + rapport.attacker; labelAttaquantNSoldats.Text = " " + rapport.nSoldatsAttacker.ToString(); labelAttaquantNSoldatsSurvived.Text = " " + rapport.nSoldatsSurvivedAttacker.ToString();
        labelDefenseur.Text = " " + rapport.defenser; labelDefenseurNSoldats.Text = " " + rapport.nSoldatsDefenser.ToString(); labelDefenseurNSoldatsSurvived.Text = " " + rapport.nSoldatsSurvivedDefenser.ToString();
        if (areYouAttacker)
        {
            if (rapport.winner == rapport.attacker) { labelWinner.Text = "Vous avez remporté le combat!"; } else { labelWinner.Text = "Vous avez perdu le combat."; }
        } else
        {
            if (rapport.winner != rapport.attacker) { labelWinner.Text = "Vous avez remporté le combat!"; } else { labelWinner.Text = "Vous avez perdu le combat."; }
        }
    }

    void OnCloseButtonClicked(object sender, EventArgs e) => Close();
}
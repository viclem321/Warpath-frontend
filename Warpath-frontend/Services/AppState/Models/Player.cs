

namespace Warpath_frontend.AppState.Models;






public class Player
{
    public string pseudo;
    public List<int> allVillagesLocation;
    public int actifVillageLocation;  public Village? actifVillage;

    public Player(string pPseudo, List<int> pAllVillagesLocation, int pActifVillageLocation, Village? pActifVillage)
    {
        pseudo = pPseudo; allVillagesLocation = pAllVillagesLocation; actifVillageLocation = pActifVillageLocation; actifVillage = pActifVillage;
    }
}
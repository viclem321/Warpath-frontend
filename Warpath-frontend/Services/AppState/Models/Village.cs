using System.Diagnostics;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;

namespace Warpath_frontend.AppState.Models;
    


public class Village
{
    public int nSoldats; public int nSoldatsDispo;

    public Village(int pNSoldats, int pNSoldatsDispo)
    {
        nSoldats = pNSoldats; nSoldatsDispo = pNSoldatsDispo;
    }

}

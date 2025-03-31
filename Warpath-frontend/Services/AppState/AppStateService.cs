using Warpath_frontend.AppState.Models;


namespace Warpath_frontend.Services.AppState;




public class AppStateService
{
    public User? user;
    public AppStateService()
    {
        user = null;
    }

    public bool UserLogin(User pUser)
    {
        user = pUser;
        return true;
    }

    public bool UserDisconnect()
    {
        user = null;
        return true;
    }


}




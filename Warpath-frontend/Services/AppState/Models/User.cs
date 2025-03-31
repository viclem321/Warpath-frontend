

namespace Warpath_frontend.AppState.Models;




public class User
{
    public string username;
    public string token;
    public string? playerPseudo;  public Player? player;

    public User(string pUsername, string pToken, string?pPlayerPseudo, Player? pPlayer)
    {
        username = pUsername; token = pToken; playerPseudo = pPlayerPseudo; player = pPlayer;
    }

}


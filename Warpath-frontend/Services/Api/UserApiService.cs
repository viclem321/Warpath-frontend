using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Warpath_frontend.AppState.Models;

namespace Warpath_frontend.Services.Api
{



    public class UserApiService : BaseApiService
    {
        public UserApiService()
        {
        }




        

        public async Task<(bool success, string? username, string? token)> LoginAsync(string pUsername, string pPassword)
        {
            // login with creds
            var loginData = new { Username = pUsername, Password = pPassword };
            HttpResponseMessage? response = await SendRequest("login", HttpMethod.Post, null, loginData); if(response != null)
            {
                if(response.IsSuccessStatusCode)
                {
                    string newToken = await response.Content.ReadAsStringAsync();
                    return (true, pUsername, newToken);
                }
            }
            return (false, null, null);
        }

        public async Task<bool> RegisterAsync(string pUsername, string pPassword)
        {
            var registerData = new { Username = pUsername, Password = pPassword };
            HttpResponseMessage? response = await SendRequest("register", HttpMethod.Post, null, registerData); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> CreatePlayerAsync(string token, string username, string newPlayerPseudo)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/createPlayer", HttpMethod.Post, token, newPlayerPseudo); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }


        public async Task<string?> ImportUserDatasAsync(string token, string username)  // return PlayerPseudo or null
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/getPlayerName", HttpMethod.Get, token); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
            }
            return null;
        }




    }
}

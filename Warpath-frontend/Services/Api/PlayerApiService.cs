using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Newtonsoft.Json;

namespace Warpath_frontend.Services.Api
{



    public class PlayerApiService : BaseApiService
    {
        public PlayerApiService()
        {
        }




        public async Task<PlayerDto?> ImportAllPlayerDatas(string token, string username, string playerPseudo)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/getDatas", HttpMethod.Get, token); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<PlayerDto>(json);
                }
            }
            return null;
        }


        // Action on this model
        public async Task<bool> CreateNewVillageAsync(string token, string username, string playerPseudo, int location)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/createNewVillage", HttpMethod.Post, token, location); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> DeleteVillageAsync(string token, string username, string playerPseudo, int location)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/deleteVillage", HttpMethod.Post, token, location); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
    }

}



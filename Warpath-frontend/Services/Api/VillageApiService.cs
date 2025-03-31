using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Warpath_frontend.Services.Api
{



    public class VillageApiService : BaseApiService
    {
        public VillageApiService()
        {

        }




        public async Task<VillageDto?> ImportAllVillageDatas(string token, string username, string playerPseudo, int villageLocation)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/getVillageDatas", HttpMethod.Get, token); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    return JsonConvert.DeserializeObject<VillageDto>(json, settings);
                }
            }
            return null;
        }



        public async Task<UpgradeActionDto?> StartUpgradeBuilding(string token, string username, string playerPseudo, int villageLocation, BuildingType buildingType)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/building/" + (int)buildingType + "/startUpgradeBuilding", HttpMethod.Post, token); if (response != null)
            {
                if (response.IsSuccessStatusCode) 
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    return JsonConvert.DeserializeObject<UpgradeActionDto>(json, settings);
                }
            }
            return null;
        }

        public async Task<bool> EndUpgradeAction1(string token, string username, string playerPseudo, int villageLocation)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/endUpgradeAction1", HttpMethod.Post, token); if (response != null)
            {
                if (response.IsSuccessStatusCode) { return true; }
            }
            return false;
        }






        public async Task<int?> RecoltResources(string token, string username, string playerPseudo, int villageLocation, BuildingType buildingType)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/building/" + (int)buildingType + "/recolt", HttpMethod.Post, token); if (response != null)
            {
                if (response.IsSuccessStatusCode) 
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    if (int.TryParse(responseBody, out int result)) { return result; }
                }
            }
            return null;
        }




        public async Task<CampMilitaireDTO?> GetCampMilitaireDatas(string token, string username, string playerPseudo, int villageLocation)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/building/" + (int)BuildingType.CampMilitaire + "/getAllDatas", HttpMethod.Get, token); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                    return JsonConvert.DeserializeObject<CampMilitaireDTO>(json, settings);
                }
            }
            return null;
        }


        public async Task<(int?, DateTime?)> CaserneStartTraining(string token, string username, string playerPseudo, int villageLocation, int nTroopsToTrain)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/building/caserne/trainingTroops/" + nTroopsToTrain , HttpMethod.Post, token); if (response != null)
            {
                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<(int, DateTime)>(json);
                }
            }
            return (null, null);
        }

        public async Task<bool> CaserneEndTraining(string token, string username, string playerPseudo, int villageLocation)
        {
            HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/village/building/caserne/endTraining", HttpMethod.Post, token); if (response != null)
            {
                if (response.IsSuccessStatusCode) { return true; }
            }
            return false;
        }


    }

}



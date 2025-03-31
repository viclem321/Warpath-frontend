using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warpath.Shared.Catalogue;
using Warpath.Shared.DTOs;
using Warpath_frontend.Views.MapPage.Models;

namespace Warpath_frontend.Services.Api;


public class MapApiService : BaseApiService
{

    public MapApiService()
    {

    }




    public async Task<MapDto?> ImportAllMap(string token, string username, string playerPseudo)
    {
        HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/getAllMap", HttpMethod.Get, token); if (response != null)
        {
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                return JsonConvert.DeserializeObject<MapDto>(json, settings);
            }
        }
        return null;
    }



    public async Task<RapportFightDto?> AttackVillage(string token, string username, string playerPseudo, int villageLocation, int villageDefLocation, int nSoldatsToSend)
    {
        HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/" + villageLocation + "/attack/" + villageDefLocation, HttpMethod.Post, token, nSoldatsToSend); if (response != null)
        {
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                return JsonConvert.DeserializeObject<RapportFightDto>(json, settings);
            }
        }
        return null;
    }


    public async Task<RapportFightDto?> GetRapport(string token, string username, string playerPseudo, string idRapport)
    {
        HttpResponseMessage? response = await SendRequest("user/" + username + "/player/" + playerPseudo + "/map/getOneRapport/" + idRapport, HttpMethod.Get, token); if (response != null)
        {
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                return JsonConvert.DeserializeObject<RapportFightDto>(json, settings);
            }
        }
        return null;
    }





}


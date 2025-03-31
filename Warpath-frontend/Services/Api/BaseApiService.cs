using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Warpath_frontend.Services.Api
{
    public class BaseApiService
    {
        private readonly HttpClient _httpClient;

        public BaseApiService()
        {
            _httpClient = new HttpClient  { BaseAddress = new Uri("http://localhost:5000/api/") };
        }


        // 🔹 Fonction générique pour envoyer des requêtes HTTP
        public async Task<HttpResponseMessage?> SendRequest(string endpoint, HttpMethod method, string? token, object? data = null)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(method, endpoint);

                if (token != null) { request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);  }
                if (data != null) { string json = JsonConvert.SerializeObject(data); request.Content = new StringContent(json, Encoding.UTF8, "application/json"); }

                return await _httpClient.SendAsync(request);
            }
            catch { return null; }
        }
    }
}
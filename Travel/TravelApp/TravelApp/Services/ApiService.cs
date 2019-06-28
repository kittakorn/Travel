using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TravelApp.Helpers;
using TravelApp.Models;

namespace TravelApp.Services
{
    class ApiService
    {
        HttpClientHandler clientHandler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
        };

        public async Task<List<Place>> GetPlaceAsync()
        {
            var client = new HttpClient(clientHandler);
            var json = await client.GetStringAsync(Constant.ApiAddress + "api/Places");
            var places = JsonConvert.DeserializeObject<List<Place>>(json);

            return places;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(HttpMethod.Post, Constant.ApiAddressAccount + "Token")
            {
                Content = new FormUrlEncodedContent(keyValues)
            };

            var client = new HttpClient(clientHandler);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);
            return jwtDynamic.Value<string>("access_token");
        }

        public async Task<bool> RegisterAsync(Register register)
        {
            var client = new HttpClient(clientHandler);
            var json = JsonConvert.SerializeObject(register);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(
                Constant.ApiAddressAccount + "api/Account/Register", httpContent);
            return response.IsSuccessStatusCode;
        }

    }
}

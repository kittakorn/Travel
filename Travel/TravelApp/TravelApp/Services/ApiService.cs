﻿using System;
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
        
        public async Task<List<Place>> GetPlaceAsync()
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(Constant.ApiAddress + "api/Places");
            var places = JsonConvert.DeserializeObject<List<Place>>(json);
            return places;
        }

        public async Task<Place> GetPlaceAsync(int id)
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(Constant.ApiAddress + "api/Places/" + id);
            var place = JsonConvert.DeserializeObject<Place>(json);
            return place;
        }

       
        public async Task<AspNetUser> GetUserAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Setting.AccessToken);
            var json = await client.GetStringAsync(Constant.ApiAddressAccount + "api/Account/GetUser");
            var user = JsonConvert.DeserializeObject<AspNetUser>(json);
            return user;
        }



        public async Task<bool> PostCommentAsync(Comment comment)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(comment);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Constant.ApiAddress + "api/Comments", content);
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> LoginAsync(string email, string password)
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

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);
            var accessToken = jwtDynamic.Value<string>("access_token");
            var userId = jwtDynamic.Value<string>("userId");
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(userId))
                return false;
            Setting.AccessToken = accessToken;
            Setting.UserId = userId;
            return true;
        }

        public async Task<bool> RegisterAsync(Register register)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(
                Constant.ApiAddressAccount + "api/Account/Register", httpContent);
            return response.IsSuccessStatusCode;
        }

    }
}

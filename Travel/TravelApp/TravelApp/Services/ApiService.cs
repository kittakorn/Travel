using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TravelApp.Helpers;
using TravelApp.Models;

namespace TravelApp.Services
{
    public class ApiService
    {

        public async Task<bool> PutUserAsync(EditProfile model)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Constant.ApiAddressAccount + "api/Account/EditProfile", content);
            return response.IsSuccessStatusCode;

        }
        public async Task<bool> PutUserAsync(ChangePassword model)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Setting.AccessToken);
            var json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Constant.ApiAddressAccount + "api/Account/ChangePassword", content);
            return response.IsSuccessStatusCode;
        }
        public async Task<User> GetUserAsync()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Setting.AccessToken);
            var json = await client.GetStringAsync(Constant.ApiAddressAccount + "api/Account/GetUser");
            var user = JsonConvert.DeserializeObject<User>(json);
            return user;
        }

        public async Task<List<Place>> GetPlaceAsync()
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(Constant.ApiAddress + "api/Places");
            var places = JsonConvert.DeserializeObject<List<Place>>(json);
            return places;
        }

        public async Task<bool> DeleteCommentAsync(int id)
        {
            var client = new HttpClient();

            var response = await client.DeleteAsync(
                Constant.ApiAddress + "api/Comments/" + id);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> PutCommentAsync(Comment comment)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(comment);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PutAsync(
                Constant.ApiAddress + "api/Comments/" + comment.CommentId, content);
            return response.IsSuccessStatusCode;
        }


        public async Task<Place> GetPlaceAsync(int id)
        {
            var client = new HttpClient();
            var json = await client.GetStringAsync(Constant.ApiAddress + "api/Places/" + id);
            var place = JsonConvert.DeserializeObject<Place>(json);
            return place;
        }

        public async Task<bool> LoginAsync(Login login)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", login.Email),
                new KeyValuePair<string, string>("password", login.Password),
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

        public async Task<bool> PostCommentAsync(Comment comment)
        {
            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(comment);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(Constant.ApiAddress + "api/Comments", content);
            return response.IsSuccessStatusCode;
        }

    }
}
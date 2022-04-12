using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace CodeAid.UI.Data
{
    public class ApiManager
    {
        string baseUrl = "https://localhost:7238/";

        public async Task<UserModel> GetUser(string id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest/", id);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<UserModel>(strResponse);
                    return data;
                }
            }
            return null;
        }
        public async Task<bool> RegisterUser(IdentityUserDto userToRegister)
        {
            // Call the constructor with the identity user dto 
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat(baseUrl, "api/user");
                var response = await httpClient.PostAsJsonAsync<IdentityUserDto>(url, userToRegister);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<List<InterestModel>> GetAllInterest()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<InterestModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }
        public async Task<List<UserModel>> GetUser()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<UserModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }
        public async Task<bool> CreateInterest(InterestDto interest, string id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/Interest/", id);
                var response = await httpClient.PostAsJsonAsync<InterestDto>(url, interest);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }
        public async Task<List<string>> GetUserInterests(string id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest/my-interests/", id);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<string>>(strResponse);
                    return data;
                }
            }
            return null;
        }

        public async Task<bool> CreateThread(ThreadDto thread, string id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, $"api/thread/CreateThread/{id}");

                var response = await httpClient.PostAsJsonAsync(url, thread);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<List<ThreadModel>> GetAllThreads()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/thread/");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<ThreadModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }

        public async Task<List<ThreadModel>> GetAllQuestions()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/thread/");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<ThreadModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CodeAid.UI.Data
{
    public class ApiManager
    {
        string baseUrl = "https://localhost:7238/";

        public async Task<UserModel> GetUser(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/user/", accessToken);
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
        public async Task<InterestModel> GetInterest(int id, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest/", id, accessToken);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<InterestModel>(strResponse);
                    return data;
                }
            }
            return null;
        }
        public async Task<List<InterestModel>> GetAllInterest(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest/", accessToken);
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
        //public async Task<List<UserModel>> GetUser(string id)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        string url = string.Concat(baseUrl, "api/interest");
        //        var response = await httpClient.GetAsync(url);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var strResponse = await response.Content.ReadAsStringAsync();
        //            var data = JsonConvert.DeserializeObject<List<UserModel>>(strResponse);
        //            return data;
        //        }
        //    }
        //    return null;
        //}

        public async Task<List<InterestModel>> GetUserInterests(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/User/Interests/", accessToken);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<InterestModel>>(strResponse);
                    return data;
                }
                //var response = await httpClient.GetFromJsonAsync<List<UserInterestModel>>(url);

                //if (response != null)
                //{
                //    return response;
                //}
                //return null;
            }
            return null;
        }

        public async Task<bool> AddInterestToUser(InterestModel interest, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat($"{baseUrl}api/user/interest/add/{interest.Id}/{accessToken}");
                var response = await httpClient.PostAsJsonAsync<InterestModel>(url, interest);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> CreateInterest(InterestDto interest, string id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/Interest/create/", id);
                var response = await httpClient.PostAsJsonAsync<InterestDto>(url, interest);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<bool> RegisterUser(IdentityUserDto userToRegister)
        {
            // Call the constructor with the identity user dto 
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat(baseUrl, "api/user/signup");
                var response = await httpClient.PostAsJsonAsync<IdentityUserDto>(url, userToRegister);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            return false;
        }
        public async Task<bool> DeleteInterest(int id, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/Interest/{id}/{accessToken}");
                var response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }

        }
    }
}

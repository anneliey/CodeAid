using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

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
        public async Task<InterestModel> GetInterest(int id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat($"{baseUrl}api/interest/{id}");
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

        public async Task<List<InterestModel>> GetAllInterest()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest/list");
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
        //public async Task<List<InterestModel>> GetAllInterest(string accessToken)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        string url = string.Concat(baseUrl, "api/interest/list/", accessToken);
        //        var response = await httpClient.GetAsync(url);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var strResponse = await response.Content.ReadAsStringAsync();
        //            var data = JsonConvert.DeserializeObject<List<InterestModel>>(strResponse);
        //            return data;
        //        }
        //    }
        //    return null;
        //}

        public async Task<List<InterestModel>> GetRegisterInterest()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/interest/list");
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


        public async Task<bool> CreateInterest(InterestDto interest, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/Interest/create/", accessToken);
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


        public async Task<bool> EditInterest(InterestModel interest, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/Interest/{accessToken}");
                var response = await httpClient.PutAsJsonAsync(url, interest);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
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


        public async Task<bool> EditThread(ThreadDto thread, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/thread/Edit/{accessToken}");
                var response = await httpClient.PutAsJsonAsync(url, thread);

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


        public async Task<bool> DeleteThread(int id, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/Thread/{id}/{accessToken}");
                var response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task<ThreadModel> GetThread(int id, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat($"{baseUrl}api/thread/{id}/{accessToken}");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ThreadModel>(strResponse);
                    return data;
                }
            }
            return null;
        }


        public async Task<List<MessageModel>> GetAllMessages()
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/Message/GetAll/");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<MessageModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }


        public async Task<bool> PostMessages(MessageDto message, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, $"api/Message/Create/{accessToken}");
                var response = await httpClient.PostAsJsonAsync(url, message);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }


        public async Task<List<MessageModel>> GetThreadMessages(string accessToken, int id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = string.Concat(baseUrl, "api/Message/thread ", accessToken);
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<MessageModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }


        public async Task<bool> DeleteMessage(int id, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/Message/{id}/{accessToken}");
                var response = await httpClient.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }


        public async Task<List<MessageModel>> GetUserMessages(string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/User/Messages/{accessToken}");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<MessageModel>>(strResponse);
                    return data;
                }
            }
            return null;
        }


        public async Task<MessageModel> GetMessage(int id)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/Message/{id}");
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var strResponse = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<MessageModel>(strResponse);
                    return data;
                }
            }
            return null;
        }


        public async Task<IdentityUser> DeleteAccount(string id)
        {
            string url = String.Concat(baseUrl, "api/user/", id);

            HttpClient httpClient = new();

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await httpClient.SendAsync(httpRequest);

            return null;
        }


        public async Task<IdentityUser> DeactivateAccount(string id)
        {
            string url = String.Concat(baseUrl, "api/user/deactivate/", id);

            HttpClient httpClient = new();

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await httpClient.SendAsync(httpRequest);

            return null;
        }


        public async Task<IActionResult> ActivateAccount(string id)
        {
            string url = String.Concat(baseUrl, "api/user/activate/", id);

            HttpClient httpClient = new();

            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(string.Empty, Encoding.UTF8, "application/json")
            };
            HttpResponseMessage response = await httpClient.SendAsync(httpRequest);

            return null;
        }



        public async Task<bool> EditMessage(MessageDto message, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat($"{baseUrl}api/Message/Edit/{accessToken}");
                var response = await httpClient.PutAsJsonAsync(url, message);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
        }

    }
}

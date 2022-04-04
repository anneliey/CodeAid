using Newtonsoft.Json;

namespace CodeAid.UI.Data
{
    public class ApiManager
    {
        string baseUrl = "https://localhost:7238/";
        public async Task<IdentityUserDto> RegisterUser(IdentityUserDto userToRegister)
        {
            // Call the constructor with the identity user dto 
            using (var httpClient = new HttpClient())
            {
                string url = String.Concat(baseUrl, "api/user");

                using (var response = await httpClient.PostAsJsonAsync<IdentityUserDto>(url, userToRegister))
                {
                    //string ApiResponse = await response.Content.ReadAsStringAsync();
                    //userToRegister = JsonConvert.DeserializeObject<IdentityUserDto>(ApiResponse);
                    //return userToRegister;

                    if (response.IsSuccessStatusCode)
                    {

                    }
                }
            }
            return null;

        }
    }
}

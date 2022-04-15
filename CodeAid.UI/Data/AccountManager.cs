using Microsoft.AspNetCore.Identity;

namespace CodeAid.UI.Data
{
    public class AccountManager
    {
        //public void GetUser(string id)
        //{
        //    ApiManager apiManager = new ApiManager();
        //    apiManager.GetUser(id);
        //}

        public async Task<bool> AddInterestToUser(InterestModel interest, string userId)
        {
            ApiManager apiManager = new ApiManager();
            var interestToAdd = await apiManager.AddInterestToUser(interest, userId);
            if (interestToAdd)
            {
                return true;
            }
            return false;
        }

        public async Task DeleteUser(IdentityUser userDto)
        {
            var id = userDto.Id;

            ApiManager apiManager = new();
            await apiManager.DeleteAccount(id);
        }

        public async Task DeactiveUser(string id)
        {
            ApiManager apiManager = new();
            await apiManager.DeactivateAccount(id);
        }

        public async Task<bool> ActivateUser(string id)
        {
            ApiManager apiManager = new();
            var result = await apiManager.ActivateAccount(id);
            if (result != null)
            {
                return true;
            }
            return false;
        }

    }
}

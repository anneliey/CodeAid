using Microsoft.AspNetCore.Identity;

namespace CodeAid.UI.Data
{
    public class AccountManager
    {
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

        public async Task DeleteAccount(IdentityUser userDto)
        {
            var id = userDto.Id;

            ApiManager apiManager = new();
            await apiManager.DeleteAccount(id);
        }

        public async Task DeactiveAccount(string id)
        {
            ApiManager apiManager = new();
            await apiManager.DeactivateAccount(id);
        }

        public async Task<bool> ActivateAccount(string id)
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

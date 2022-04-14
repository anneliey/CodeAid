using Microsoft.AspNetCore.Identity;

namespace CodeAid.UI.Data
{
    public class InterestManager
    {
        public async Task<InterestModel> GetInterest(int id, IdentityUser user)
        {
            ApiManager apiManager = new ApiManager();
            var interests = await apiManager.GetInterest(id, user.Id);
            return interests;
        }

        public async Task<List<InterestModel>> GetInterests()
        {
            ApiManager apiManager = new ApiManager();
            var interests = await apiManager.GetAllInterest();
            return interests;
        }

        public async Task<List<InterestModel>> GetRegisterInterests(IdentityUser user)
        {
            ApiManager apiManager = new ApiManager();
            var interests = await apiManager.GetRegisterInterest();
            return interests;
        }

        public async Task<List<InterestModel>> GetUserInterests(string id)
        {
            ApiManager apiManager = new ApiManager();
            var userInterests = await apiManager.GetUserInterests(id);
            return userInterests;
        }

        public async Task<bool> CreateInterest(InterestDto interest, string userId)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.CreateInterest(interest, userId);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> EditInterest(InterestModel interest, IdentityUser user)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.EditInterest(interest, user.Id);
            if (result)
            {
                return true;
            }
            return false;
        }

    }
}

using Microsoft.AspNetCore.Identity;

namespace CodeAid.UI.Data
{
    public class InterestManager
    {
        //public async Task<InterestModel> GetInterest(int id)
        //{
        //    ApiManager apiManager = new ApiManager();
        //    var interests = await apiManager.GetInterest(id);
        //    return interests;
        //}

        public async Task<List<InterestModel>> GetInterests(IdentityUser user)
        {
            ApiManager apiManager = new ApiManager();
            var interests = await apiManager.GetAllInterest(user.Id);
            return interests;
        }
        public async Task<List<InterestModel>> GetUserInterests(string id)
        {
            ApiManager apiManager = new ApiManager();
            var userInterests = await apiManager.GetUserInterests(id);
            return userInterests;
        }
        public async Task<bool> CreateInterest(InterestDto interest, string id)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.CreateInterest(interest, id);
            if (result)
            {
                return true;
            }
            return false;
        }
    }
}

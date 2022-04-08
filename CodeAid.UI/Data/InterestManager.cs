using Microsoft.AspNetCore.Identity;

namespace CodeAid.UI.Data
{
    public class InterestManager
    {
        public async Task<List<InterestModel>> GetInterests()
        {
            ApiManager apiManager = new ApiManager();
            var interests = await apiManager.GetAllInterest();
            return interests;
        }
        public async Task<bool> CreateInterest(InterestModel interest, string id)
        {
            ApiManager apiManager = new ApiManager();
            var interests = await apiManager.CreateInterest(interest, id);
            return interests;

        }
    }
}

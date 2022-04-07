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
        public async Task<List<string>> GetUserInterests(IdentityUser user)
        {
            if (user != null)
            {
                var id = user.Id;
                ApiManager apiManager = new ApiManager();
                var interests = await apiManager.GetUserInterests(id);
                return interests;
            }
            return null;
        }
    }
}

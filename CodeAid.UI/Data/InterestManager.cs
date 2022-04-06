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
    }
}

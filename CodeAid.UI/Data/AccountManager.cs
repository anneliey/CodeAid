﻿namespace CodeAid.UI.Data
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
    }
}
﻿using Microsoft.AspNetCore.Identity;

namespace CodeAid.UI.Data
{
    public class ThreadManager
    {

        public async Task<bool> CreateThread(ThreadDto thread, string id)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.CreateThread(thread, id);
            if (result)
            {
                return true;
            }
            return false;
        }
        public async Task<List<ThreadModel>> GetAllThreads()
        {
            ApiManager apiManager = new ApiManager();
            var threads = await apiManager.GetAllThreads();
            return threads;
        }

        public async Task<List<ThreadModel>> GetQuestions()
        {
            ApiManager apiManager = new ApiManager();
            var questions = await apiManager.GetAllQuestions();
            return questions;
        }

        public async Task<bool> EditThread(ThreadDto thread, IdentityUser user)
        {
            ApiManager apiManager = new ApiManager();
            var result = await apiManager.EditThread(thread, user.Id);
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<ThreadModel> GetThread(int id, IdentityUser user)
        {
            ApiManager apiManager = new ApiManager();
            var thread = await apiManager.GetThread(id, user.Id);
            return thread;
        }

    }
}

using Microsoft.AspNetCore.Identity;

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

        public async Task<bool> EditThread(ThreadModel thread, IdentityUser user)
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

        /// <summary>
        /// Method for searching the productlist for users specific searchterm.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        /// ThreadManager threadManager = new();
        public async Task<List<ThreadModel>> Search(string searchTerm)
        {
            ThreadManager threadManager = new();

            List<ThreadModel> AllThreads = await threadManager.GetAllThreads();

            if (string.IsNullOrEmpty(searchTerm))
            {
                return AllThreads;
            }
            return AllThreads.Where(product => product.QuestionTitle.ToLower().Contains(searchTerm.ToLower())).ToList();
            //return AllThreads.Where(product => product.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
        }




    }
}

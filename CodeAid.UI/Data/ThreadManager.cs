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

        public async Task<List<ThreadModel>> GetThread(int id)
        {
            ApiManager apiManager = new ApiManager();
            var allThreads = await apiManager.GetAllThreads();
            var sortedThreads = allThreads.Where(x => x.Id == id).ToList();
            return sortedThreads;
        }

        public async Task<List<ThreadModel>> GetQuestions()
        {
            ApiManager apiManager = new ApiManager();
            var questions = await apiManager.GetAllQuestions();
            return questions;
        }
    }
}

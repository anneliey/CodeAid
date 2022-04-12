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
        public async Task<List<ThreadModel>> GetThreads()
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
    }
}

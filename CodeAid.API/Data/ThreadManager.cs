namespace CodeAid.API.Data
{
    public class ThreadManager
    {
        private readonly AppDbContext _context;

        public ThreadManager(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<ThreadModel> Search(string SearchTerm)
        {
            var searchTerm = _context.Threads.ToList();

            if (string.IsNullOrEmpty(SearchTerm))
            {
                return searchTerm;
            }

            var lowercaseSearchText = SearchTerm.ToLower();
            var searchResult = searchTerm.Where(s => s.QuestionTitle.ToLower().Contains(lowercaseSearchText));

            return searchResult;
        }
    }
}

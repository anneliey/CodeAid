using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class SortThreadByTitleModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public SortThreadByTitleModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public List<ThreadModel> AllThreads { get; set; }
        public InterestModel Interest { get; set; }
        public int CurrentInterestId { get; set; }
        public ThreadModel Thread { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }
        public List<ThreadModel> Result { get; set; }


        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                if (id == 0)
                {
                    ThreadManager threadManager = new();

                    AllThreads = await threadManager.Search(SearchTerm);
                    Result = AllThreads.OrderBy(prod => prod.QuestionTitle).ToList();



                }
                else
                {
                    // Id is not 0
                    // Get specific thread with the id
                    InterestManager interestManager = new();

                    Interest = await interestManager.GetInterest(id, user);
                    CurrentInterestId = Interest.Id;
                }

            }

            return Page();
        }


        public async Task<IActionResult> OnPost(ThreadModel thread)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ApiManager apiManager = new();

                await apiManager.DeleteThread(thread.Id, user.Id);
            }
            return RedirectToPage("/Threads");
        }


    }
}

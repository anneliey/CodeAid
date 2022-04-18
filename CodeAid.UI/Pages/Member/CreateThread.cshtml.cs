using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class CreateThreadModel : PageModel
    {

        private readonly SignInManager<IdentityUser> _signInManager;

        public CreateThreadModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        //[Required(ErrorMessage = "Title is required!")]
        //public string QuestionTitle { get; set; }

        //[Required(ErrorMessage = "Question can't be empty!")]
        //public string Question { get; set; }
        //public List<ThreadModel> AllThreads { get; set; }
        public ThreadDto Thread { get; set; }
        public List<InterestModel> AllInterests { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public int CurrentInterestId { get; set; }

        public async Task<IActionResult> OnGet(int id) // id is interest id
        {
            CurrentInterestId = id;

            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                ThreadManager threadManager = new();

                Thread.InterestId = CurrentInterestId; // Hard coded elden ring interest - Hämtas sen från razor page property CurrentInterestId

                var result = await threadManager.CreateThread(Thread, user.Id);

                if (!result)
                {
                    ErrorMessage = "Thread already exists!";
                }
                else
                {
                    return RedirectToPage("/Threads");
                }
            }
            return Page();
        }
    }
}

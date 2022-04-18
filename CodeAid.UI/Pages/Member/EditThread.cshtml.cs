using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    [BindProperties]
    public class EditThreadModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public ThreadModel Thread { get; set; }

        public EditThreadModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ThreadManager threadManager = new();
                Thread = await threadManager.GetThread(id, user);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ThreadDto threadDto = new ThreadDto
                {
                    ThreadId = Thread.Id,
                    QuestionTitle = Thread.QuestionTitle,
                    Question = Thread.Question,
                };

                ThreadManager threadManager = new();
                await threadManager.EditThread(threadDto, user);
            }
            return RedirectToPage("/Threads");
        }
    }
}

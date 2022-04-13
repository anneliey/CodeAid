using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class ThreadsModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        
        public ThreadsModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public List<ThreadModel> AllThreads { get; set; }
        public async Task<IActionResult> OnGet()
        {
            ThreadManager threadManager = new();
            AllThreads = await threadManager.GetAllThreads();
            return Page();
        }

        public async Task<IActionResult> OnPost(ThreadModel thread)
        {
            ThreadManager threadManager = new();
            AllThreads = await threadManager.GetThread(thread.Id);
            return RedirectToPage("/question/");
        }
    }
}

using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class QuestionModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public QuestionModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public ThreadModel Question { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            ThreadManager manager = new();
            Question = await manager.GetThread(id);
            return Page();
        }
    }
}

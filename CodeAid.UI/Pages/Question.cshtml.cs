using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    public class QuestionModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public QuestionModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public List<ThreadModel> AllQuestions { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            ThreadManager manager = new();
            AllQuestions = await manager.GetThread(id);
            return Page();
        }
    }
}

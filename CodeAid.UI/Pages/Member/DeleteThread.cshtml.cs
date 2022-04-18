using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    public class DeleteThreadModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public DeleteThreadModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet(int id)
        {
            // Delete thread with id
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

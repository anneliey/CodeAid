using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member.Interest
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public List<InterestModel> UserInterests { get; set; }
        public bool EmptyList { get; set; } = false;

        public async Task<IActionResult> OnGet()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                InterestManager manager = new();
                UserInterests = await manager.GetUserInterests(user.Id);
                if (UserInterests == null || UserInterests.Count == 0)
                {
                    EmptyList = true;
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(InterestModel interest)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ApiManager apiManager = new();

                await apiManager.DeleteInterest(interest.Id, user.Id);
            }
            return RedirectToPage("/Member/Interest/Index");
        }
    }
}

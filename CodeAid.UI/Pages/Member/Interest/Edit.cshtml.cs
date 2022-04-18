using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member.Interest
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public InterestModel Interest { get; set; }

        public EditModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                InterestManager interestManager = new();
                Interest = await interestManager.GetInterest(id);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                InterestManager interestManager = new();
                await interestManager.EditInterest(Interest, user);
            }
            return RedirectToPage("/Member/Interest/Index");
        }
    }
}

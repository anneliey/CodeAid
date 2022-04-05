using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUserDto> signInManager;

        public LogoutModel(SignInManager<IdentityUserDto>signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Login");
        }

        public IActionResult OnPostDontLogoutAsync()
        {
            return RedirectToPage("/Index");
        }

    }
}

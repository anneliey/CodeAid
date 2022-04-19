using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    public class DeactivateAccountModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public DeactivateAccountModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string Message { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            AccountManager accountManager = new AccountManager(); 
            IdentityUserDto identityUserDto = new()
            {
                Username = user.UserName,
            };
            var result = accountManager.DeactiveAccount(user.Id);

            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
            
        }
    }
}

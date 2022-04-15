using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    public class DeleteAccountModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public DeleteAccountModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public string Message { get; set; } = string.Empty;
        public bool isDeleted { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            AccountManager accountManager = new AccountManager();
            var result = accountManager.DeleteAccount(user);

            if (result != null)
            {
                //isDeleted = true;
                Message = "This account has been deleted.";
                await _signInManager.SignOutAsync();
                return RedirectToPage("/Index");
            }
            else
            {
                Message = "Failed to delete account.";
                return Page();
            }
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    public class ActivateAccountModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public ActivateAccountModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public string Message { get; set; } = string.Empty;
    }
}

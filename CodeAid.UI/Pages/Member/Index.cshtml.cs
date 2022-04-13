using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public string DateRegistered { get; set; }
        public UserModel CurrentUser { get; set; } = new();
        public async Task OnGet()
        {
            var identityUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (identityUser != null)
            {
                CurrentUser.Username = identityUser.UserName;

                //ApiManager apiManager = new ApiManager();
                //var userDb = apiManager.GetUser(identityUser.UserName);
                //DateRegistered = CurrentUser.DateRegistered.ToLongDateString();

            }

        }
    }
}

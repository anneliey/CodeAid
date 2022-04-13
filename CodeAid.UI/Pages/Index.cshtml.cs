using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        
        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public List<InterestModel> AllInterests { get; set; }

        public async Task <IActionResult> OnGet(string interest)
        {
            
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                InterestManager interestManager = new();

                AllInterests = await interestManager.GetInterests(user);
            }
            return Page();
                
        }
    }
}
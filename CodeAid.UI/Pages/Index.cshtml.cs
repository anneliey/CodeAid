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

        public List<InterestModel> AllInterests { get; set; } = new();

        public async Task <IActionResult> OnGet()
        {
            InterestManager interestManager = new();

            AllInterests = await interestManager.GetInterests();

            return Page();
                
        }
    }
}
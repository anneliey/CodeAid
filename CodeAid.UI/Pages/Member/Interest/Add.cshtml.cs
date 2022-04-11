using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member.Interest
{
    [BindProperties]
    public class AddModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AddModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public List<InterestModel> AllInterests { get; set; }
        public InterestDto Interest { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public async Task<IActionResult> OnGet()
        {
            InterestManager manager = new();
            AllInterests = await manager.GetInterests();
            return Page();
        }
        public async Task<IActionResult> OnPostCreateInterest()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                InterestManager interestManager = new();
                var result = await interestManager.CreateInterest(Interest, user.Id);
                if (!result)
                {
                    ErrorMessage = "Interest already exists!";
                }
                else
                {
                    return RedirectToPage("/Member/Interest/Index");
                }
            }
            return Page();
        }
    }
}

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
        public List<InterestModel> AllInterests { get; set; }

        public async Task<IActionResult> OnGet()
        {
            InterestManager manager = new();
            AllInterests = await manager.GetInterests();
            return Page();

        }

        //public void OnPost()
        //{
        //    InterestModel interest = new()
        //    {
        //        Name = Interest.Name
        //    }.ToList();
        //}
    }
}

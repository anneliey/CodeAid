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
        public string Name { get; set; } = string.Empty;
        public List<InterestModel> AllInterests { get; set; }
        public InterestModel Interest { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public async Task OnGet()
        {
            InterestManager manager = new();
            AllInterests = await manager.GetInterests();
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
            }
            return null;

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

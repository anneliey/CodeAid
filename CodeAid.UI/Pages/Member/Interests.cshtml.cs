using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    [BindProperties]
    public class InterestsModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public InterestsModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public string User { get; set; }
        public List<string> AllInterests { get; set; }
        public async Task OnGet()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            User = user.UserName;
            InterestManager interestManager = new();
            AllInterests = await interestManager.GetUserInterests(user);
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

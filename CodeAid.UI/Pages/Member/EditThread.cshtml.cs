using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member
{
    [BindProperties]
    public class EditThreadModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public ThreadModel Thread { get; set; }

        public EditThreadModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ThreadManager threadManager = new();
                Thread = await threadManager.GetThread(id, user);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ThreadManager threadManager = new();
                await threadManager.EditThread(Thread, user);
            }
            return RedirectToPage("/Threads");
        }

        //private readonly SignInManager<IdentityUser> _signInManager;

        //public EditThreadModel(SignInManager<IdentityUser> signInManager)
        //{
        //    _signInManager = signInManager;
        //}

        //public ThreadDto Thread { get; set; }
        //public List<InterestModel> AllInterests { get; set; }
        //public string ErrorMessage { get; set; } = string.Empty;

        //public async Task<IActionResult> OnGet()
        //{
        //    var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
        //    InterestManager manager = new();
        //    AllInterests = await manager.GetInterests(user);
        //    return Page();
        //}
        //public async Task<IActionResult> OnPost()
        //{
        //    var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
        //    if (user != null)
        //    {
        //        ThreadManager threadManager = new();

        //        var result = await threadManager.CreateThread(Thread, user.Id);

        //        if (!result)
        //        {
        //            ErrorMessage = "Thread already exists!";
        //        }
        //        else
        //        {
        //            return RedirectToPage("/Threads");
        //        }
        //    }
        //    return Page();
        //}
        //public async Task<IActionResult> OnPostEditThread()
        //{
        //    var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
        //    if (user != null)
        //    {
        //        ThreadManager threadManager = new();
        //        await threadManager.EditThread(Thread, user);
        //    }
        //    return RedirectToPage("/Member/EditThread");
        //}


    }
}

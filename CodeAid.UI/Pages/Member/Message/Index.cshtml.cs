using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member.Message
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public IndexModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public List<MessageModel> UserMessages { get; set; }
        public bool MessageRemoved { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                MessageManager manager = new();
                UserMessages = await manager.GetUserMessages(user.Id);
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                MessageManager messageManager = new();

                var result = await messageManager.DeleteMessage(id, user.Id);
                if (result)
                {
                    TempData["success"] = "Messages removed successfully";
                    return RedirectToPage("/member/message/index", MessageRemoved);
                }
            }
            return Page();
        }
    }
}

using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages.Member.Message
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        public MessageModel Message { get; set; }
        public EditModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                ApiManager apiManager = new();
                Message = await apiManager.GetMessage(id);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                MessageDto messageDto = new MessageDto
                {
                    MessageId = Message.Id,
                    Message = Message.Message
                };
                MessageManager messageManager = new MessageManager();
                await messageManager.EditMessage(messageDto, user.Id);
            }
            return RedirectToPage("/Member/Message/Index");
        }
    }
}

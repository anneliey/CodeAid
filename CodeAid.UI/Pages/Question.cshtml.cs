using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    public class QuestionModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public QuestionModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [BindProperty]
        public ThreadModel Question { get; set; }
        [BindProperty]
        public MessageModel Message { get; set; }
        [BindProperty]
        public IdentityUser CurrentUser { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            CurrentUser = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

            if (CurrentUser != null)
            {
                ThreadManager manager = new();
                Question = await manager.GetThread(id, CurrentUser);
                //MessageManager messageManager = new MessageManager();
                //var messageId = 17;
                //Message = await messageManager.GetMessage(messageId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                MessageManager messageManager = new();
                await messageManager.CreateMessage(new MessageDto
                {
                    Message = Message.Message,
                    ThreadId = id

                }, user.Id);

            }
            return RedirectToPage("/member/message/index");
           
        }
    }
}


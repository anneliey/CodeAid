using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class QuestionModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public QuestionModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public ThreadModel Question { get; set; }
        public List<MessageModel> AllMessages { get; set; }
        public MessageModel Message { get; set; }
        public IdentityUser CurrentUser { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            
            if (user != null)
            {
                ThreadManager manager = new();
                Question = await manager.GetThread(id, user);
                CurrentUser = user;
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                MessageManager messageManager = new();
                var result = await messageManager.CreateMessage(new MessageDto
                {
                    Message = Message.Message,
                    ThreadId = Question.Id
                }, user.Id);
            }
            return RedirectToAction($"/Question/{Question.Id}");

        }
    }



}


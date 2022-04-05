using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CodeAid.UI.Pages
{
    [BindProperties]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ErrorMessage { get; set; }


        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("ErrorMessage");

            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(Username, Password, false, false);
                if (identityResult.Succeeded)
                {

                    return RedirectToPage("/Index");
                }
                else
                {

                    ErrorMessage = "Invalid username or password";
                }
            }

            return Page();
        }
    }
}

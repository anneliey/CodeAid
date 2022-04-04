using CodeAid.UI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CodeAid.UI.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public RegisterModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [Required(ErrorMessage = "Username is required!")]
        [MaxLength(20, ErrorMessage = "Username is too long!")]
        [MinLength(3, ErrorMessage = "Username is too short!")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Verify password is required!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match!")]
        public string VerifiedPassword { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                ApiManager apiManager = new();
                IdentityUserDto identityUserDto = new()
                {
                    Username = Username,
                    Email = Email,
                    Password = Password
                };

                // Call the constructor with the identity user dto 
                var result = await apiManager.RegisterUser(identityUserDto);
                if (result != null)
                {
                    return RedirectToAction("/Index");
                }
            }
            return Page();
        }
    }
}

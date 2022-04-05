using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CodeAid.UI.Pages.Member
{
    [BindProperties]
    public class ChangePasswordModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public ChangePasswordModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Current password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = 
            "The new password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var user = await _signInManager.UserManager.GetUserAsync(HttpContext.User);

                if (user != null)
                {
                    var result = await _signInManager.UserManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToPage("ChangePasswordConfirm");
                    }
                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return Page();
                    }

                    //await _signInManager.RefreshSignInAsync(user);

                }

            }
            return Page();
        }

    }
}

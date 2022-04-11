using Microsoft.AspNetCore.Identity;

namespace CodeAid.API.Data
{
    public class AccessTokenManager
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccessTokenManager(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public bool HasValidAccessToken(string accessToken)
        {
            var confirmToken = _signInManager.UserManager.Users.Where(x => x.Id == accessToken).FirstOrDefault();
            if (confirmToken != null)
            {
                return true;
            }
            return false;
        }
    }
}

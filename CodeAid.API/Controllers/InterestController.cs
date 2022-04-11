using CodeAid.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeAid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;

        public InterestController(SignInManager<IdentityUser> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<InterestModel>> GetAllInterests()
        {
            var result = _context.Interests;
            if (result.Any())
            {
                var resultList = result.ToList();

                return Ok(resultList);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("{accessToken}")]
        public async Task<IActionResult> CreateInterest([FromBody] InterestDto interestToAdd, string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var exists = _context.Interests.Where(x => x.Name == interestToAdd.Name).FirstOrDefault();
                if (exists == null)
                {
                    var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                    //var dbUser = _context.Users.Where(x => x.Username == identityUser.UserName).FirstOrDefault();

                    var dbUser = _context.Users
                        .Include(u => u.UserInterests)
                        .Include(u => u.Interests)
                        .FirstOrDefault(x => x.Username == identityUser.UserName);


                    InterestModel interest = new InterestModel
                    {
                        //UserId = dbUser.Id,
                        Name = interestToAdd.Name,
                        User = dbUser,

                    };
                    dbUser.UserInterests.Add(new UserInterestModel
                    {
                        Interest = interest,
                        User = dbUser
                    });
                    //UserInterestModel UserInterests = new UserInterestModel
                    //{

                    //    InterestId = interest.Id,
                    //    Interest = interest,
                    //    User = dbUser

                    //};
                    //interest.UserInterests.Add(UserInterests);
                    //dbUser.Interests.Add(interest);
                    //dbUser.UserInterests.Add(UserInterests);

                    _context.Users.Update(dbUser);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            return null;
        }
    }
}

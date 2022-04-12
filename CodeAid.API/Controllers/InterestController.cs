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
        [Route("{id}/{accessToken}")]
        public ActionResult<InterestModel> GetInterest(string accessToken, int id)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var interest = _context.Interests.Where(x => x.Id.Equals(id)).FirstOrDefault();
                if (interest != null)
                {
                    return interest;
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{accessToken}")]
        public ActionResult<List<InterestModel>> GetAllInterests(string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var result = _context.Interests;
                if (result.Any())
                {
                    var resultList = result.ToList();

                    return Ok(resultList);
                }
                return BadRequest();
            }
            return null;
        }

        [HttpPost]
        [Route("Create/{accessToken}")]
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
                    var dbUser = _context.Users
                        .Include(u => u.UserInterests)
                        .Include(u => u.Interests)
                        .FirstOrDefault(x => x.Username == identityUser.UserName);

                    InterestModel interest = new InterestModel
                    {
                        Name = interestToAdd.Name,
                        User = dbUser,
                    };
                    dbUser.UserInterests.Add(new UserInterestModel
                    {
                        Interest = interest,
                        User = dbUser
                    });

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

        [HttpDelete]
        [Route("{id}/{accessToken}")]
        public async Task<ActionResult> DeleteInterest(string accessToken, [FromRoute] int id)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var result = _context.Interests.Any(x => x.UserId == dbUser.Id);
                if (result)
                {
                    var interest = _context.Interests.Where(x => x.Id == id).FirstOrDefault();
                    if (interest != null)
                    {
                        _context.Interests.Remove(interest);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                    return NotFound();
                }
            }
            return null;
        }
    }
}

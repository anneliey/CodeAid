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
        public ActionResult<InterestModel> GetInterest([FromRoute] int id, string accessToken)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var interest = _context.Interests.Where(i => i.UserInterests.Any(ui => ui.UserId == dbUser.Id)).ToList();

                return _context.Interests.Include(i => i.Threads).Select(i => new InterestModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Threads = i.Threads.Select(t => new ThreadModel()
                    {
                        Id = t.Id,
                        Question = t.Question,
                        QuestionTitle = t.QuestionTitle,
                    }).ToList()
                }).FirstOrDefault(x => x.Id == id);

                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("List")]
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

        //[HttpGet]
        //[Route("List")]
        //public ActionResult<List<InterestModel>> GetRegisterInterests(string accessToken)
        //{
        //    AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
        //    var isValid = accessTokenManager.HasValidAccessToken(accessToken);
        //    if (isValid)
        //    {
        //        var result = _context.Interests;
        //        if (result.Any())
        //        {
        //            var resultList = result.ToList();

        //            return Ok(resultList);
        //        }
        //        return BadRequest();
        //    }
        //    return null;
        //}

        [HttpPost]
        [Route("Create/{accessToken}")]
        public async Task<IActionResult> CreateInterest([FromBody] InterestDto interestToAdd, string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var interestToLower = interestToAdd.Name.ToLower();
                var result = _context.Interests.Where(x => x.Name == interestToLower).FirstOrDefault();
                if (result == null)
                {
                    var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                    var dbUser = _context.Users
                        .Include(u => u.UserInterests)
                        .Include(u => u.Interests)
                        .FirstOrDefault(x => x.Username == identityUser.UserName);

                    InterestModel interest = new InterestModel
                    {
                        Name = interestToLower,
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
            }
            return BadRequest();
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

        [HttpPut]
        [Route("{accessToken}")]
        public async Task<IActionResult> EditInterest([FromBody] InterestModel interestToUpdate, string accessToken)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var exists = _context.Interests.Any(x => x.UserId == dbUser.Id);

                if (exists)
                {
                    var interest = _context.Interests.Where(x => x.Id == interestToUpdate.Id).FirstOrDefault();
                    if (interest != null && interest.Threads == null)
                    {
                        interest.Name = interestToUpdate.Name;

                        _context.Interests.Update(interest);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
            }
            return BadRequest();
        }
    }
}

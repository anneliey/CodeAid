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
        [Route("{id}")]
        public ActionResult<InterestModel> GetInterest([FromRoute] int id)
        {
            if (id != null && id != 0)
            {
                return _context.Interests
                    .Include(i => i.User)
                    .Include(i => i.Threads)
                    .ThenInclude(t => t.User).Select(i => new InterestModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Threads = i.Threads.Select(t => new ThreadModel
                        {
                            Id = t.Id,
                            Question = t.Question,
                            QuestionTitle = t.QuestionTitle,
                            ThreadDate = t.ThreadDate,
                            User = t.User,
                            Messages = t.Messages.Select(m => new MessageModel
                            {
                                PostDate = m.PostDate,
                                Message = m.Message,
                                MessageEdit = m.MessageEdit,
                            }).ToList(),
                        }).OrderByDescending(t => t.ThreadDate).ToList()
                    }).FirstOrDefault(x => x.Id == id);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("List")]
        public ActionResult<List<InterestModel>> GetAllInterests()
        {
            var result = _context.Interests
                    .Include(i => i.User)
                    .Include(i => i.Threads)
                    .ThenInclude(t => t.User).Select(i => new InterestModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Threads = i.Threads.Select(t => new ThreadModel
                        {
                            Id = t.Id,
                            Question = t.Question,
                            QuestionTitle = t.QuestionTitle,
                            ThreadDate = t.ThreadDate,
                            User = t.User,
                            Messages = t.Messages.Select(m => new MessageModel
                            {
                                PostDate = m.PostDate,
                                Message = m.Message,
                                MessageEdit = m.MessageEdit,
                            }).ToList(),
                        }).OrderByDescending(t => t.ThreadDate).ToList()
                    }).ToList();
            if (result.Any())
            {
                return Ok(result);
            }
            return BadRequest();
        }


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


        //Added delete to route
        [HttpDelete]
        [Route("Delete/{id}/{accessToken}")]
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
                        if (interest.User.Username == dbUser.Username)
                        {
                            _context.Interests.Remove(interest);
                            await _context.SaveChangesAsync();
                            return Ok();
                        }
                    }
                    return NotFound();
                }
            }
            return null;
        }


        //Added edit to route
        [HttpPut]
        [Route("Edit/{accessToken}")]
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
                        if (interest.User.Username == dbUser.Username)
                        {
                            interest.Name = interestToUpdate.Name;

                            _context.Interests.Update(interest);
                            await _context.SaveChangesAsync();
                            return Ok();
                        }
                    }
                }
            }
            return BadRequest();
        }
    }
}

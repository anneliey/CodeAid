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
        [HttpGet]
        [Route("my-interests/{accessToken}")]
        public ActionResult<List<string>> GetCurrentUserInterests(string accessToken)
        {
            var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(accessToken)).FirstOrDefault();

            if (identityUser != null)
            {
                var userDb = _context.Users.Where(u => u.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var list = _context.Interests
                    .Where(u => u.UserId.Equals(userDb.Id))
                    .Select(list => list.Name).ToList();
                return list;
            }
            return BadRequest();
        }

        // NOT FINISHED!
        [HttpPost]
        public async Task<ActionResult<InterestModel>> CreateInterest([FromBody] InterestModel interestToAdd)
        {
            if (interestToAdd != null)
            {
                InterestModel model = new()
                {
                    Name = interestToAdd.Name,
                    UserInterests = interestToAdd.UserInterests,
                };
                _context.Interests.Add(model);
                _context.SaveChanges();
                //if (result.)
                return Ok(model);
            }
            return BadRequest();
        }


    }
}

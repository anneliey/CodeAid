using CodeAid.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeAid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadController : ControllerBase
    {
        //public IEnumerable<ThreadModel> threadSearch { get; set; } = new List<ThreadModel>();

        //[BindProperty]
        //public string SearchTerm { get; set; }

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;

        public ThreadController(SignInManager<IdentityUser> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ThreadModel>> GetAllThreads()
        {
            var result = _context.Threads.Include(t => t.User).Include(t => t.Messages).OrderByDescending(t => t.ThreadDate).ToList();
            //var thread = _context.Threads.Include(t => t.Messages.OrderByDescending(t => t.PostDate)).Where(t => t.Id == id).FirstOrDefault();

            if (result.Any())
            {
                foreach (var t in result)
                {
                    t.User.Threads = null;
                    foreach (var ut in t.Messages)
                    {
                        ut.Thread = null;
                        ut.Message = null;
                        ut.User = null;
                    }
                }
                return Ok(result);
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("{accessToken}")]
        public ActionResult<List<ThreadModel>> GetAllQuestions()
        {
            var result = _context.Threads;
            if (result.Any())
            {
                var resultList = result.ToList();

                return Ok(resultList);
            }

            return BadRequest();
        }


        [HttpGet]
        [Route("my-threads/{accessToken}")]
        public ActionResult<List<ThreadModel>> GetUserThread(string acessToken)
        {
            var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(acessToken)).FirstOrDefault();

            if (identityUser != null)
            {
                var userDb = _context.Users.Where(u => u.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var list = _context.Threads
                    .Where(u => u.UserId.Equals(userDb.Id)).ToList();

                return list;
            }
            return BadRequest();
        }


        [HttpPost("[action]/{accessToken}")]
        public async Task<IActionResult> CreateThread([FromBody] ThreadDto thread, [FromRoute] string accessToken, int interestId)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);

            var exists = _context.Threads.Where(x => x.QuestionTitle == thread.QuestionTitle).FirstOrDefault();

            if (exists == null)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username == identityUser.UserName).FirstOrDefault();

                var threadInterest = await _context.Interests.FirstOrDefaultAsync(i => i.Id == thread.InterestId);

                if (threadInterest != null)
                {
                    ThreadModel threadQuestion = new ThreadModel()
                    {
                        User = dbUser,
                        Question = thread.Question,
                        QuestionTitle = thread.QuestionTitle,
                        ThreadDate = DateTime.Now,
                        Interest = threadInterest
                    };

                    _context.Threads.Add(threadQuestion);
                    await _context.SaveChangesAsync();

                    return Ok();
                }
            }

            return BadRequest();
        }


        [HttpPut]
        [Route("Edit/{accessToken}")]
        public async Task<IActionResult> EditThread([FromBody] ThreadDto threadDto, string accessToken)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var threadToUpdate = _context.Threads.Where(x => x.Id == threadDto.ThreadId).FirstOrDefault();

                if (threadToUpdate != null && threadToUpdate.Messages == null)
                {
                    if (threadToUpdate.User.Id == dbUser.Id)
                    {
                        threadToUpdate.QuestionTitle = threadDto.QuestionTitle;
                        threadToUpdate.Question = threadDto.Question;

                        _context.Threads.Update(threadToUpdate);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }

            }
            return BadRequest();
        }


        //Added delete to route
        [HttpDelete]
        [Route("Delete/{id}/{accessToken}")]
        public async Task<ActionResult> DeleteThread(string accessToken, [FromRoute] int id)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var result = _context.Threads.Any(x => x.UserId == dbUser.Id);
                if (result)
                {
                    var thread = _context.Threads.Where(x => x.Id == id).FirstOrDefault();
                    if (thread != null)
                    {
                        if (thread.User.Id == dbUser.Id)
                        {
                            _context.Threads.Remove(thread);
                            await _context.SaveChangesAsync();
                            return Ok();
                        }
                    }
                    return NotFound();
                }
            }
            return null;
        }


        [HttpGet]
        [Route("{id}/{accessToken}")]
        public ActionResult<ThreadModel> GetThread([FromRoute] int id, string accessToken)
        {
            AccessTokenManager accessTokenManager = new(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var thread = _context.Threads
                    .Include(t => t.User)
                    .Include(t => t.Messages)
                    .ThenInclude(m => m.User).Select(t => new ThreadModel
                    {
                        Id = t.Id,
                        Question = t.Question,
                        QuestionTitle = t.QuestionTitle,
                        ThreadDate = t.ThreadDate,
                        User = t.User,
                        UserId = t.UserId,
                        Messages = t.Messages.Select(m => new MessageModel
                        {
                            Id = m.Id,
                            Message = m.Message,
                            PostDate = m.PostDate,
                            MessageEdit = m.MessageEdit,
                            UserId = m.UserId,
                            ThreadId = m.ThreadId,
                            User = m.User,
                        }).OrderByDescending(x => x.PostDate).ToList()
                    }).FirstOrDefault(t => t.Id == id);

                if (thread != null)
                {
                    foreach (var m in thread.Messages)
                    {
                        m.Thread = null;
                    }
                    return Ok(thread);
                }
                return null;
            }
            return BadRequest();
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<List<ThreadModel>>> Search(string threadTitle)
        {
            try
            {

                IQueryable<ThreadModel> thread = _context.Threads;

                if (!string.IsNullOrEmpty(threadTitle))
                {
                    thread = thread.Where(t => t.QuestionTitle.Contains(threadTitle));
                }

                var result = thread;

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


    }
}


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
        private readonly AppDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public ThreadController(AppDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ActionResult<List<ThreadModel>> GetAllThreads()
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
                // change for your method
                var list = _context.Threads
                    .Where(u => u.UserId.Equals(userDb.Id)).ToList();

                return list;
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<ActionResult<List<ThreadModel>>> PostThread(ThreadModel thread, string id)
        {
            var exists = _context.Threads.Where(x => x.QuestionTitle == thread.QuestionTitle).FirstOrDefault();
            if (exists == null)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id == id).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username == identityUser.UserName).FirstOrDefault();
                ThreadModel interest = new ThreadModel()
                {
                    User = dbUser,
                    Question = thread.Question,
                    QuestionTitle = thread.QuestionTitle,
                    ThreadDate = thread.ThreadDate,
                    Messages = thread.Messages,
                    Interest = thread.Interest

                };

                _context.Threads.Add(thread);
                await _context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return null;
            }


        }


        //[HttpPut]
        //public async Task<ActionResult<List<ThreadModel>>> UpdateThread(ThreadModel threadToUpdate, string id)
        //{
        //    var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(id)).FirstOrDefault();

        //    if (identityUser != null)
        //    {
        //        var userDb = _context.Users.Where(u => u.Username.Equals(identityUser.UserName)).FirstOrDefault();
        //        // change for your method
        //       var dbThread = await _

        //        return list;
        //    }
        //    return BadRequest();
        //var dbThread = await _context.Threads.FindAsync(threadToUpdate.Id);
        //if (dbThread == null)
        //{
        //    return BadRequest("Thread not found");
        //}

        //dbThread.QuestionTitle = threadToUpdate.QuestionTitle;
        //dbThread.Question = threadToUpdate.Question;  

        //await _context.SaveChangesAsync();
        //return Ok(await _context.Threads.ToListAsync());





        //}



        //[HttpDelete("{id}")]
        //public async Task<ActionResult<List<ThreadModel>>> DeleteThread(string accessToken, int id)
        //{

        //    var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(accessToken)).FirstOrDefault();

        //    if (identityUser != null)
        //    {
        //        var userDb = _context.Users.Where(u => u.Username.Equals(identityUser.UserName)).FirstOrDefault();
        //        var dbTread = await _context.Threads.FindAsync(id);

        //           _context.Threads.Remove(dbTread);
        //           await _context.SaveChangesAsync();

        //        return Ok(await _context.Threads.ToListAsync());
        //    }
        //    return BadRequest();

        //}










    }
}

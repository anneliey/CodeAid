using CodeAid.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeAid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;


        public MessageController(AppDbContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<MessageModel> GetMessage([FromRoute] int id)
        {
            var message = _context.Messages.Where(m => m.Id == id).Include(m => m.User).Select(m => new MessageModel
            {
                Id = m.Id,
                Message = m.Message,
                PostDate = m.PostDate,
                ThreadId = m.ThreadId,
                User = new UserModel()
                {
                    Id = m.User.Id,
                    Username = m.User.Username,
                    Banned = m.User.Banned,
                    Deleted = m.User.Deleted,
                }

            }).FirstOrDefault();
            if (message != null)
            {
                return Ok(message);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<MessageModel>> GetAllMessages()
        {
            var result = _context.Messages;
            if (result.Any())
            {
                var resultList = result.ToList();

                return Ok(resultList);
            }
            return BadRequest();
        }


        [HttpGet]
        [Route("thread /{acessToken}")]
        public ActionResult<List<MessageModel>> GetThreadMessages(string accessToken, int id)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var messages = _context.Messages.Include(m => m.User).Where(m => m.ThreadId == id).Select(t => new MessageModel
                {
                    Message = t.Message,
                    User = new UserModel() // Project the user into a user with the data we want (without circular references)
                    {
                        Id = t.User.Id,
                        Username = t.User.Username,
                        Banned = t.User.Banned,
                        Deleted = t.User.Deleted
                    }
                }).ToList();

                return messages;
            }
            return BadRequest();
        }


        [HttpPost]
        [Route("Create/{accessToken}")]
        public async Task<ActionResult> PostMessages(MessageDto message, string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id == accessToken).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username == identityUser.UserName).FirstOrDefault();

                if (dbUser != null)
                {
                    // Get the specific thread in the MessageDto object (sent from client)
                    var thread = _context.Threads.FirstOrDefault(t => t.Id == message.ThreadId);

                    // If thread was found
                    if (thread != null)
                    {
                        // Construct the message object
                        var messageToAdd = new MessageModel()
                        {
                            Message = message.Message,
                            User = dbUser,
                            Thread = thread,
                            PostDate = DateTime.Now
                        };

                        _context.Messages.Add(messageToAdd);
                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                }
            }
            return BadRequest();
        }


        [HttpPut]
        [Route("Edit/{accessToken}")]
        public async Task<IActionResult> EditMessage([FromBody] MessageDto updatedMessage, string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);
            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(accessToken)).FirstOrDefault();
                var userDb = _context.Users.Where(u => u.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var messageToUpdate = _context.Messages.Where(t => t.Id == updatedMessage.MessageId).FirstOrDefault();

                if (messageToUpdate != null)
                {
                    messageToUpdate.Message = updatedMessage.Message;
                    messageToUpdate.MessageEdit = true;
                    _context.Messages.Update(messageToUpdate);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
            }
            return BadRequest();
        }


        [HttpDelete("{id}/{accessToken}")]
        //[Route("{id}/{accessToken}")]
        public async Task<ActionResult> DeleteMessage([FromRoute] int id, string accessToken)
        {
            // Todo: Set the message bool property "Deleted" to True
            AccessTokenManager accessTokenManager = new(_signInManager);
			var isValid = accessTokenManager.HasValidAccessToken(accessToken);
			if (isValid)
			{
				var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
				var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
				var result = _context.Interests.Any(x => x.UserId == dbUser.Id);
				if (result)
				{
					var message = _context.Messages.Where(x => x.Id == id).FirstOrDefault();
					if (message != null)
					{
						_context.Messages.Remove(message);
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



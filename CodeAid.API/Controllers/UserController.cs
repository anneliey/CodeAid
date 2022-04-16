﻿using CodeAid.API.Data;
using CodeAid.UI.Pages.Member;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodeAid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;

        public UserController(SignInManager<IdentityUser> signInManager, AppDbContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }
        //[HttpGet]
        //[Route("{accessToken}")]
        //public async Task<ActionResult<UserModel>> GetUser(string accessToken)
        //{
        //    AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
        //    var isValid = accessTokenManager.HasValidAccessToken(accessToken);
        //    if (isValid)
        //    {
        //        var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();


        //    var dbUser = _context.Users.Where(x => x.Username == identityUser.UserName).FirstOrDefault();

        //    //var dbUser = _context.Users.Include(u => u.UserInterests).ThenInclude(u => u.Interest).Include(u => u.Messages).ThenInclude(m => m.Thread).Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
        //    if (dbUser != null)
        //    {
        //        return dbUser;
        //    }
        //    return NotFound();

        //    }
        //    return BadRequest();

        //}
        [HttpGet]
        [Route("Interests/{accessToken}")]
        public ActionResult<List<InterestModel>> GetUserInterests(string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);

            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(u => u.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users.Where(x => x.Username.Equals(identityUser.UserName)).FirstOrDefault();
                var userInterests = _context.Interests.Where(i => i.UserInterests.Any(ui => ui.UserId == dbUser.Id)).ToList();

                if (userInterests != null)
                {
                    foreach (var userInterest in userInterests)
                    {
                        userInterest.User = null;
                    }
                    return Ok(userInterests);
                }
                return BadRequest();
            }
            return null;
        }

        [HttpPost]
        [Route("Interest/Add/{id}/{accessToken}")]
        public async Task<IActionResult> AddInterestToUser([FromBody] InterestModel interestToAdd, string accessToken)
        {
            AccessTokenManager accessTokenManager = new AccessTokenManager(_signInManager);
            var isValid = accessTokenManager.HasValidAccessToken(accessToken);

            if (isValid)
            {
                var identityUser = _signInManager.UserManager.Users.Where(x => x.Id.Equals(accessToken)).FirstOrDefault();
                var dbUser = _context.Users
                    .Include(u => u.UserInterests)
                    .Include(u => u.Interests)
                    .FirstOrDefault(x => x.Username == identityUser.UserName);
                var interestName = _context.Interests.Where(i => i.Id == interestToAdd.Id).Select(i => i.Name).FirstOrDefault();
                var exists = dbUser.UserInterests.Any(ui => ui.Interest.Name == interestName);

                if (!exists)
                {
                    if (interestName != null && dbUser != null)
                    {
                        InterestModel interest = new InterestModel
                        {
                            Name = interestName,
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
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("SignUp")]
        public async Task<IActionResult> RegisterUser([FromBody] IdentityUserDto userToSignUp)
        {
            // Create an empty identity user
            IdentityUser newUser = new();

            // Add prop to identity user
            newUser.UserName = userToSignUp.Username;
            newUser.Email = userToSignUp.Email;

            // Create user
            var createUserResult = await _signInManager.UserManager.CreateAsync(newUser, userToSignUp.Password);

            if (createUserResult.Succeeded)
            {
                //var userInterestToAdd = userToSignUp.UserInterests.ForEach(ui => ).ToList();
                //var userInterestToAdd = _context.Interests.Where(i => i.Id == userToSignUp.UserInterests.FirstOrDefault()).ToList();
                UserModel user = new();
                user.Username = newUser.UserName;
                user.DateRegistered = DateTime.Now;

                _context.Users.Add(user);
                if (userToSignUp.UserInterests != null && userToSignUp.UserInterests.Count > 0)
                {
                    var list = new List<InterestModel>();
                    foreach (var interest in userToSignUp.UserInterests)
                    {
                        var i = _context.Interests.Where(i => i.Id == interest).FirstOrDefault();
                        list.Add(i);
                    }
                    if (list != null && list.Count > 0)
                    {
                        foreach (var i in list)
                        {
                            _context.UserInterests.Add(new UserInterestModel
                            {
                                User = user,
                                Interest = i
                            });
                        }
                    }
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}

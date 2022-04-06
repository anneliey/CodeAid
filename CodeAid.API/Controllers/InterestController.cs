using CodeAid.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeAid.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InterestController(AppDbContext context)
        {
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
        //[HttpPost]
        //public async InterestModel CreateInterest([FromBody]InterestModel interestToAdd)
        //{
        //    InterestModel model = new InterestModel();
        //    model.Add
        //    var result = 
        //    return 
        //}
    }
}

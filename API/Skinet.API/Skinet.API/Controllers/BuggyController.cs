using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public BuggyController(StoreContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var thing = _dbContext.Products.Find(42);

            if(thing == null)
            {
                return NotFound();
            }

            return Ok();
        }


        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var thing = _dbContext.Products.Find(42);
            var thingToRun = thing.ToString();

            return Ok();
        }


        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }


        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
    }
}

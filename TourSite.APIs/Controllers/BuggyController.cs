using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Controllers;
using TourSite.APIs.Errors;
using TourSite.Repository.Data.Contexts;

namespace Store.APIs.Controllers
{
    
    public  class BuggyController : BaseApiController
    {
        private readonly TourDbContext context ;
        public BuggyController(TourDbContext _context)
        {
            context = _context;
        }


        [HttpGet("notfound")]
        public async Task<IActionResult> GetNotFoundError()
        {
            var tour = await context.Tours.FindAsync(99);
            if (tour == null) return NotFound(new APIErrerResponse(404));
            return Ok(tour);
        }

        [HttpGet("ServerError")] //Null Reference Exception in the backend
        public async Task<IActionResult> GetServerError()
        {
            var tour = await context.Tours.FindAsync(99);
            var tourToReturn = tour.ToString();
            return Ok(tourToReturn);
        }

        [HttpGet("BadRequestbyiD")]
        public async Task<IActionResult> GetByIdBadReqError(int id)
        {
            var tour = await context.Tours.FindAsync(id);
            if (tour == null) return BadRequest(new APIErrerResponse(400,$"tour with id {id} not found"));
            return Ok(tour);
        }
        [HttpGet("BadRequest")]
        public async Task<IActionResult> GetByIdBadReqError()
        {
            return BadRequest("This is a bad request error");
        }

        [HttpGet("Unauthorized")]
        public async Task<IActionResult> GetUnauhorizeError()
        {
            return Unauthorized();
        }


    }
}

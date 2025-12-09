using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Errors;

namespace TourSite.APIs.Controllers
{
    [Route("error/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {
        public IActionResult Error() 
        {
            return NotFound(new APIErrerResponse(StatusCodes.Status404NotFound,"This endpoint not found"));
        }
    }
}

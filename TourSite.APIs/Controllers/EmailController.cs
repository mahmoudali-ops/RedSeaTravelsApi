using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.DTOs.Email;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Emails;

namespace TourSite.APIs.Controllers
{
    
    public class EmailController : BaseApiController
    {
        private readonly IEmailService emailsService;
        public EmailController(IEmailService _emailsService)
        {
            emailsService = _emailsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmails([FromQuery] EmailSpecParams SpecParams)
        {
            var emails = await emailsService.GetAllEmailsToursAsync(SpecParams);
            return Ok(emails);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmailId(int? id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var email = await emailsService.GetCatEmailByIdAsync(id.Value);
            if (email == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Email with this Id : {id}"));
            }
            return Ok(email);
        }

        [HttpPost("sendemail")]
        public async Task<IActionResult> SendEmail([FromBody] EmailsDto dto)
        {
            await emailsService.AddEmailAsync(dto);
            return Ok(new { message = "Emai  sent successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEmail(int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result = await emailsService.DeleteEmailAsync(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Email with this Id : {id}"));
            }
            return Ok(new { message = "Email  deleted successfully" });
        }
    }
}

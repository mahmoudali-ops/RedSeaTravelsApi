using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Tours;
using TourSite.Service.Services.TourImgs;

namespace TourSite.APIs.Controllers
{
    
    public class ToursController : BaseApiController
    {
        private readonly IToursService toursService;
        public ToursController(IToursService _toursService)
        {
            toursService = _toursService;
        }
        [HttpGet("client")]
        public async Task<IActionResult> GetAllTours([FromQuery] TourSpecParams tourSpecParams)
        {
            var tours = await toursService.GetAllToursAsync(tourSpecParams);
            return Ok(tours);
        }
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllTrueTours([FromQuery] TourSpecParams tourSpecParams)
        {
            var tours = await toursService.GetAllToursTrueAsync(tourSpecParams);
            return Ok(tours);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(int? id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var tour = await toursService.GetTourByIdAsync(id.Value);
            if (tour == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Tour with this Id : {id}"));
            }
            return Ok(tour);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTour([FromForm] TourCreateDto dto)
        {
            await toursService.AddTourAsync(dto);
            return Ok(new { message = "Tour created successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdataTour([FromForm] TourCreateDto dto, int id)
        {
            if (id <= 0)
                return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));

            var result = await toursService.UpdateTour(dto, id);

            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Tour with this Id : {id}"));
            }

            return Ok(new { message = "Tour updated successfully" });
        }


        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTour(int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result =await toursService.DeleteTour(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Tour with this Id : {id}"));
            }
            return Ok(new { message = "Tour  deleted successfully" });
        }

    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.TourImgs;


namespace TourSite.APIs.Controllers
{
  
    public class TourImgController : BaseApiController
    {
        private readonly ITourImgService tourimgsService;
        public TourImgController(ITourImgService _toursService)
        {
            tourimgsService = _toursService;
        }
        [HttpGet("client")]
        public async Task<IActionResult> GetAllTours([FromQuery] TourImgSpeciParams SpecParams)
        {
            var toursimg = await tourimgsService.GetAllTourImgsAsync(SpecParams);

            return Ok(toursimg);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAdminTours([FromQuery] TourImgSpeciParams SpecParams)
        {
            var toursimg = await tourimgsService.GetAllTourImgsAdminAsync(SpecParams);

            return Ok(toursimg);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(int? id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var TouImg = await tourimgsService.GetTourImgByIdAsync(id.Value);
            if (TouImg == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no TouImg with this Id : {id}"));
            }
            return Ok(TouImg);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHotel([FromForm] TourImgCreateDto dto)
        {
            await tourimgsService.AddImgTourAsync(dto);
            return Ok(new { message = "Tour Image tour created successfully" });
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdataCategoryTour([FromForm] TourImgCreateDto dto, int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result =await tourimgsService.UpdatImgTour(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Tour Image with this Id : {id}"));
            }
            return Ok(new { message = "Tour Image updated successfully" });
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteCategoryTour(int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result =await tourimgsService.DeleteImgTour(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Tour Image with this Id : {id}"));
            }
            return Ok(new { message = "Tour Image deleted successfully" });
        }
    }
}

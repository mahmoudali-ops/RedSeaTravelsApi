using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourSite.APIs.Attributes;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Tours;
using TourSite.Service.Services.CatTours;

namespace TourSite.APIs.Controllers
{
  
    public class CategorTourController : BaseApiController
    {
        private readonly ICategoryTourService cattoursService;
        public CategorTourController(ICategoryTourService _cattoursService)
        {
            cattoursService = _cattoursService;
        }
        //[Cached(4)]
        [HttpGet("client")]
        public async Task<IActionResult> GetAllTours([FromQuery] CatTourSpecParams SpecParams)
        {
            var categories = await cattoursService.GetAllCatToursAsync(SpecParams);
            if(categories is null) return NotFound(new APIErrerResponse(404, "There is no categoriesTours .."));
            return Ok(categories);
        }
        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAdminTours([FromQuery] CatTourSpecParams SpecParams)
        {
            var categories = await cattoursService.GetAllCatToursAdminAsync(SpecParams);
            if (categories is null) return NotFound(new APIErrerResponse(404, "There is no categoriesTours .."));
            return Ok(categories);
        }

        [HttpGet("{id}")]
        //[Cached(4)]
        public async Task<IActionResult> GetTourById(int? id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var categoryTour = await cattoursService.GetCatTourByIdAsync(id.Value);
            if (categoryTour == null)
            {
                return NotFound(new APIErrerResponse(404,$"There is no categoryTour with this Id : {id}"));
            }
            return Ok(categoryTour);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCategoryTour([FromForm] CategorToutCreateDto dto)
        {
            await cattoursService.AddCatTourAsync(dto);
            return Ok(new { message = "Category tour created successfully"});
        }

        [HttpPut("update/{id}")]
        public  async Task<IActionResult> UpdataCategoryTour([FromForm] CategorToutCreateDto dto,int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result=await cattoursService.UpdateCatTour(dto,id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no categoryTour with this Id : {id}"));
            }
            return Ok(new { message = "Category tour updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryTour(int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
           var result=await cattoursService.DeleteCatTour(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no categoryTour with this Id : {id}"));
            }
            return Ok(new { message = "Category tour deleted successfully" });
        }
    }
}

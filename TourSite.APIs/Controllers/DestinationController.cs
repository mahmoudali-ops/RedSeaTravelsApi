using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourSite.APIs.Attributes;
using TourSite.APIs.Errors;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Destnations;

namespace TourSite.APIs.Controllers
{
  
    public class DestinationController : BaseApiController
    {
        private readonly IDestinationService destService;
        public DestinationController(IDestinationService _destService)
        {
            destService = _destService;
        }
        [HttpGet("client")]
       // [Cached(30)]

        public async Task<IActionResult> GetAllTours([FromQuery] DestnationSpecParams SpecParams)
        {
            var destnations = await destService.GetAllDestToursAsync(SpecParams);
            if (destnations is null) return NotFound(new APIErrerResponse(404, "There is no categoriesTours .."));

            return Ok(destnations);
        }

        [HttpGet("admin")]
        // [Cached(30)]

        public async Task<IActionResult> GetAllAdminTours([FromQuery] DestnationSpecParams SpecParams)
        {
            var destnations = await destService.GetAllDestToursAdminAsync(SpecParams);
            if (destnations is null) return NotFound(new APIErrerResponse(404, "There is no categoriesTours .."));

            return Ok(destnations);
        }

        [HttpGet("{id}")]
        //[Cached(30)]

        public async Task<IActionResult> GetTourById(int? id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var destnation = await destService.GetDestByIdAsync(id.Value);
            if (destnation == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Destnation with this Id : {id}"));
            }
            return Ok(destnation);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateDestnaion([FromForm] DestnationToutCreateDto dto)
        {
            await destService.AddDestnationAsync(dto);
            return Ok(new { message = "Destnation tour created successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdataCategoryTour([FromForm] DestnationToutCreateDto dto, int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result =await destService.UpdateDest(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Destnation with this Id : {id}"));
            }
            return Ok(new { message = "Destnation tour updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCategoryTour(int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result =await destService.DeleteDest(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Destnation with this Id : {id}"));
            }
            return Ok(new { message = "Destnation tour deleted successfully" });
        }


    }
}

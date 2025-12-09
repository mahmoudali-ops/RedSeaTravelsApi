using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TourSite.APIs.Errors;

using TourSite.Core.DTOs.Transfer;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Transfers;
using TourSite.Core.Specification.Users;


namespace TourSite.APIs.Controllers
{
    
    public class TransferController : BaseApiController
    {
        private readonly ITransferService transsService;
        public TransferController(ITransferService _transsService)
        {
            transsService = _transsService;
        }
        [HttpGet("client")]
        public async Task<IActionResult> GetAllTours([FromQuery] TrasferSpecParam SpecParams)
        {
            var transfers = await transsService.GetAllTransToursAsync(SpecParams);

            return Ok(transfers);
        }

        [HttpGet("admin")]
        public async Task<IActionResult> GetAllAdminTours([FromQuery] TrasferSpecParam SpecParams)
        {
            var transfers = await transsService.GetAllTransToursAdminAsync(SpecParams);

            return Ok(transfers);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTourById(int? id)
        {
            if (id == null) return BadRequest(new APIErrerResponse(400, "Id required .. can not be null"));

            var transfer = await transsService.GetCatTransByIdAsync(id.Value);
            if (transfer == null)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Transfer with this Id : {id}"));
            }
            return Ok(transfer);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTransfer([FromForm] TransferCreateDto dto)
        {
            await transsService.AddTransferAsync(dto);
            return Ok(new { message = "Transfer created successfully" });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdataTransfer([FromForm] TransferCreateDto dto, int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result =await transsService.UpdateTransfer(dto, id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Hotel with this Id : {id}"));
            }
            return Ok(new { message = "Transfer updated successfully" });
        }

        [HttpDelete("delete/{id}")]
        public  async Task<IActionResult> DeleteTransfer(int id)
        {
            if (id <= 0) return BadRequest(new APIErrerResponse(400, "Id required .. can not be less than or equal 0"));
            var result = await transsService.Deletetransfer(id);
            if (!result)
            {
                return NotFound(new APIErrerResponse(404, $"There is no Hotel with this Id : {id}"));
            }
            return Ok(new { message = "Transfer deleted successfully" });
        }
    }
}

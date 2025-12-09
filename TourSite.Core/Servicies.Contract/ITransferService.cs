using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.Transfer;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Transfers;

namespace TourSite.Core.Servicies.Contract
{
    public interface ITransferService
    {
        Task<PageinationResponse<TransferAllDto>> GetAllTransToursAsync(TrasferSpecParam transSpecParams);
        Task<PageinationResponse<TransferAllDto>> GetAllTransToursAdminAsync(TrasferSpecParam transSpecParams);

        Task<TransferDto> GetCatTransByIdAsync(int id);

        Task AddTransferAsync(TransferCreateDto TransferDto);
        Task <Boolean> UpdateTransfer(TransferCreateDto dto, int id);
        Task <Boolean> Deletetransfer(int id);
    }
}


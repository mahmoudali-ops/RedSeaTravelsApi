using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;
using TourSite.Core.Specification.Tours;

namespace TourSite.Core.Servicies.Contract
{
    public interface IToursService
    {
        Task<PageinationResponse<TourAllDto>> GetAllToursAsync(TourSpecParams tourSpecParams);
        Task<PageinationResponse<TourAllDto>> GetAllToursTrueAsync(TourSpecParams tourSpecParams);
        Task<TourDto> GetTourByIdAsync(int id);

        Task AddTourAsync(TourCreateDto TourCreateDto);
       Task <Boolean> UpdateTour(TourCreateDto dto, int id);
       Task<Boolean> DeleteTour(int id);

    }
}

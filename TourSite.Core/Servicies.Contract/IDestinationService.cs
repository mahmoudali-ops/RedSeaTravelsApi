using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Destnations;

namespace TourSite.Core.Servicies.Contract
{
    public interface IDestinationService
    {
        Task<PageinationResponse<DestnationAllDto>> GetAllDestToursAsync(DestnationSpecParams DestnationSpecParams);

        Task<PageinationResponse<DestnationAllDto>> GetAllDestToursAdminAsync(DestnationSpecParams DestnationSpecParams);

        Task<DestnationDto> GetDestByIdAsync(int id);

        Task AddDestnationAsync(DestnationToutCreateDto DestnationDto);
        Task<Boolean> UpdateDest(DestnationToutCreateDto dto, int id);
        Task<Boolean> DeleteDest(int id);
    }
}

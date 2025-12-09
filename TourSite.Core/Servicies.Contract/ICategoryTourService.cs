using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.Tours;

namespace TourSite.Core.Servicies.Contract
{
    public interface ICategoryTourService
    {
        Task<PageinationResponse<CategorToutAllDto>> GetAllCatToursAsync(CatTourSpecParams cattourSpecParams);
        Task<PageinationResponse<CategorToutAllDto>> GetAllCatToursAdminAsync(CatTourSpecParams cattourSpecParams);

        Task<CategorToutDto> GetCatTourByIdAsync(int id);

        Task AddCatTourAsync(CategorToutCreateDto categorToutDto);
        Task <Boolean> UpdateCatTour(CategorToutCreateDto dto, int id);
       Task <Boolean> DeleteCatTour(int id);



    }
}

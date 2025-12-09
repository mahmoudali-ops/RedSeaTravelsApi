using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.Specification.CatgeoryTour;
using TourSite.Core.Specification.TourImgs;

namespace TourSite.Core.Servicies.Contract
{
    public interface ITourImgService
    {
        Task<PageinationResponse<TourImgDto>> GetAllTourImgsAsync(TourImgSpeciParams tourImgSpecParams);
        Task<PageinationResponse<TourImgDto>> GetAllTourImgsAdminAsync(TourImgSpeciParams tourImgSpecParams);

        Task<TourImgDto> GetTourImgByIdAsync(int id);

        Task AddImgTourAsync(TourImgCreateDto ImgTourToutDto);
       Task <Boolean> UpdatImgTour(TourImgCreateDto dto, int id);
        Task<Boolean>  DeleteImgTour(int id);
    }
}

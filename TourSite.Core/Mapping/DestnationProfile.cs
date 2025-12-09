using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.DTOs.Transfer;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class DestnationProfile : Profile
    {
      public DestnationProfile(IConfiguration configuration)
        {
            CreateMap<Destination, DestnationDto>()
         // 🖼️ نضيف الـ BaseUrl على الصورة
         .ForMember(d => d.ImageCover, options => options.
               MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"));


            // =================== DESTINATION CREATE ===================
            CreateMap<Tour, TourDto>()
                .ForMember(d => d.ImageCover,
                    opt => opt.MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"))
                .ForMember(d => d.CategoryName,
                    opt => opt.MapFrom(s => s.Category.Title))
                .ForMember(d => d.DestinationName,
                    opt => opt.MapFrom(s => s.Destination.Title))
                .ForMember(d => d.Title,
                    opt => opt.MapFrom(s => s.Title))
                .ForMember(d => d.Description,
                    opt => opt.MapFrom(s => s.Description))
                .ForMember(d => d.TourImgs,
                    opt => opt.MapFrom(s => s.TourImgs));



        }
    }
}

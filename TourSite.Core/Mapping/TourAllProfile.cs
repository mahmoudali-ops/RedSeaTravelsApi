using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class TourAllProfile : Profile
    {
        public TourAllProfile(IConfiguration configuration)
        {
            CreateMap<Tour, TourAllDto>()
                // ✅ الصورة الأساسية
                .ForMember(dest => dest.ImageCover,
                    opt => opt.MapFrom(src => $"{configuration["BaseUrl"]}{src.ImageCover}"))
                // ✅ إسم التصنيف
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Title))
                // ✅ إسم الوجهة
                .ForMember(dest => dest.DestinationName,
                    opt => opt.MapFrom(src => src.Destination.Title));
        

          








        }
    }
}

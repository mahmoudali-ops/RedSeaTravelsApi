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
    public class TourProfile : Profile
    {
        public TourProfile(IConfiguration configuration)
        {
            CreateMap<Tour, TourDto>()
                // ✅ الصورة الأساسية
                .ForMember(dest => dest.ImageCover,
                    opt => opt.MapFrom(src => $"{configuration["BaseUrl"]}{src.ImageCover}"))

                 .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Title))
                // ✅ إسم الوجهة
                .ForMember(dest => dest.DestinationName,
                    opt => opt.MapFrom(src => src.Destination.Title))
                // ✅ قائمة الصور
                .ForMember(dest => dest.TourImgs,
                    opt => opt.MapFrom(src => src.TourImgs))
                .ForMember(dest => dest.Highlights,
                    opt => opt.MapFrom(src => src.Highlights))
                .ForMember(dest => dest.Includeds,
                    opt => opt.MapFrom(src => src.Includeds))
                .ForMember(dest => dest.NotIncludeds,
                    opt => opt.MapFrom(src => src.NotIncludeds));


            // Entity → DTO
   
            CreateMap<TourHighlight, TourHighlightDto>();
            CreateMap<TourIncluded, TourIncludedDto>();
            CreateMap<TourNotIncluded, TourNotIncludedDto>();
            // DTO → Entity (للـ Create/Update)
            CreateMap<TourImgDto, TourImg>();
            CreateMap<TourHighlightDto, TourHighlight>();
            CreateMap<TourIncludedDto, TourIncluded>();
            CreateMap<TourNotIncludedDto, TourNotIncluded>();


            CreateMap<TourCreateDto, Tour>();



        }
    }
}

using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.DTOs.Email;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class CategoryTourProfile : Profile
    {
        public CategoryTourProfile(IConfiguration configuration)
        {
            CreateMap<CategoryTour, CategorToutDto>()

                .ForMember(d => d.ImageCover, options => options.
               MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"))

             .ForMember(d => d.Tours,
                opt => opt.MapFrom(s => s.Tours));

            // =================== CATEGORY CREATE ===================
            CreateMap<CategorToutCreateDto, CategoryTour>()
                .ForMember(dest => dest.ImageCover, opt => opt.Ignore());
            // لإنك هتتعامل مع رفع الصورة manually
            // TOUR
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

            //// TOUR IMAGES
            //CreateMap<TourImg, TourImgDto>()
            //    .ForMember(d => d.ImageCarouselUrl,
            //        opt => opt.MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCarouselUrl}"))
            //    .ForMember(d => d.Titles,
            //        opt => opt.MapFrom(s => s.Title))
            //    .ForMember(d => d.TourName,
            //        opt => opt.MapFrom(s => s.Tour.Title));
        }
    }

}

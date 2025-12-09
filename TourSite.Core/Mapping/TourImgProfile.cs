using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class TourImgProfile : Profile
    {
        public TourImgProfile(IConfiguration configuration)
        {
            CreateMap<TourImg, TourImgDto>()
                                //.ForMember(d => d.TourName, options => options.MapFrom(s => s.Tour.Title))
                                .ForMember(d => d.ImageCarouselUrl, options => options.MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCarouselUrl}"));


            CreateMap<TourImgCreateDto, TourImg>();
        }
    }
}

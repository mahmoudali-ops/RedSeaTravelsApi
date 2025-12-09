using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.CategoryTour;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class CategoryTourAllProfile:Profile
    {
        public CategoryTourAllProfile(IConfiguration configuration)
        {
            CreateMap<CategoryTour, CategorToutAllDto>()

               .ForMember(d => d.ImageCover, options => options.
               MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"))
               ;

        }
    }
}

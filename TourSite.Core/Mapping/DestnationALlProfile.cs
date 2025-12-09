using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class DestnationALlProfile:Profile
    {
        public DestnationALlProfile(IConfiguration configuration)
        {
            CreateMap<Destination, DestnationAllDto>()
         // 🖼️ نضيف الـ BaseUrl على الصورة
         .ForMember(d => d.ImageCover, options => options.
               MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"));

        }

    }
}

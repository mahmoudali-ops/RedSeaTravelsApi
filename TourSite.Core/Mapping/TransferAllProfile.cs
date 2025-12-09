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
using TourSite.Core.DTOs.Transfer;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class TransferAllProfile : Profile
    {
        public TransferAllProfile(IConfiguration configuration)
        {
            CreateMap<Transfer, TransferAllDto>()
                                .ForMember(d => d.ImageCover, options => options.
                                   MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"))
                                 .ForMember(d => d.DestinationName, options => options.
                                MapFrom(s => s.Destination.Title));


        }
    }
}

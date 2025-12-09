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
using TourSite.Core.DTOs.Transfer;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class TransferProfile : Profile
    {
        public TransferProfile(IConfiguration configuration)
        {
            CreateMap<Transfer, TransferDto>()
            .ForMember(d => d.ImageCover,
                opt => opt.MapFrom(s => $"{configuration["BaseUrl"]}{s.ImageCover}"))

            .ForMember(d => d.DestinationName,
                opt => opt.MapFrom(s => s.Destination.Title))

            .ForMember(d => d.PricesList,
                opt => opt.MapFrom(s => s.PricesList))

            .ForMember(d => d.IncludesList,
                opt => opt.MapFrom(s => s.Includeds))

            .ForMember(d => d.NotIncludedList,
                opt => opt.MapFrom(s => s.NotIncludeds))

            .ForMember(d => d.HighlightList,
                opt => opt.MapFrom(s => s.Highlights));



            // Entity → DTO
            CreateMap<TrasnferPrices, TransferPricesDTO>();
            CreateMap<TransferIncluded, TransferIncludedDto>();
            CreateMap<TransferNotIncluded, TransferNotIncludedDto>();
            CreateMap<TransferHighlight, TransferHighlightDto>();

            // DTO → Entity (للـ Create/Update)
            CreateMap<TransferPricesDTO, TrasnferPrices>();
            CreateMap<TransferIncludedDto, TransferIncluded>();
            CreateMap<TransferNotIncludedDto, TransferNotIncluded>();
            CreateMap<TransferHighlightDto, TransferHighlight>();

            CreateMap<TransferCreateDto, Transfer>();
        }
    }
}

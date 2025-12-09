using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Transfer
{
    public class TransferCreateDto
    {
        public IFormFile? ImageFile { get; set; }
        public bool IsActive { get; set; } = true;
        public string? PriecesListJson { get; set; }
        public string? IncludesListJson { get; set; }
        public string? NotIncludedListJson { get; set; }
        public string? HighlightListJson { get; set; }

        public string ReferenceName { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }

        public int? FK_DestinationID { get; set; }


        public List<TransferPricesDTO> PricesList { get; set; } = new();
        public List<TransferIncludedDto> IncludesList { get; set; } = new();
        public List<TransferNotIncludedDto> NotIncludedList { get; set; } = new();
        public List<TransferHighlightDto> HighlightList { get; set; } = new();


    }
}

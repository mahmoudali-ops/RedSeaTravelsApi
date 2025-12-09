using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.DTOs.TourImg;

namespace TourSite.Core.DTOs.Tours
{
    public class TourCreateDto
    {
        public IFormFile? ImageFile { get; set; }
        public bool IsActive { get; set; } = true;
        public string Title { get; set; }

        public string? LinkVideo { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }
        public string Description { get; set; }

        public string referenceName { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }

        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public string LanguageOptions { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? FK_CategoryID { get; set; }
        public string? FK_UserID { get; set; }
        public int? FK_DestinationID { get; set; }



        public string? IncludesJson { get; set; }
        public List<TourIncludedDto> IncludesPoints { get; set; } = new();
        public string? NonIncludesJson { get; set; }
        public List<TourNotIncludedDto> NonIncludesPoints { get; set; } = new();
        public string? hightlightJson { get; set; }
        public List<TourHighlightDto> hightlightPoints { get; set; } = new();

        public List<TourImgCreateDto> ImagesList { get; set; } = new();


    }
}

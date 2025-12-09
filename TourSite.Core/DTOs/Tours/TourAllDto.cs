using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Email;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.DTOs.TourImg;

namespace TourSite.Core.DTOs.Tours
{
    public class TourAllDto
    {
        public int Id { get; set; }

        public string ImageCover { get; set; }
        public bool IsActive { get; set; }
        public string Title { get; set; }

        public string? LinkVideo { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }

        public string referenceName { get; set; }

        public int Duration { get; set; }
        public decimal Price { get; set; }

        public string StartLocation { get; set; }
        public string Description { get; set; }


        public string EndLocation { get; set; }
        public string LanguageOptions { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? FK_CategoryID { get; set; }
        public string? FK_UserID { get; set; }
        public int? FK_DestinationID { get; set; }

        public string CategoryName { get; set; }

      //  public string CreatedByName { get; set; }
        public string DestinationName { get; set; }

    




    }
}

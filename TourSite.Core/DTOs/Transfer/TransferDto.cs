using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.DTOs.HighLights;
using TourSite.Core.DTOs.Includes;
using TourSite.Core.DTOs.NotIncludes;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.Transfer
{
    public class TransferDto
    {

        public int Id { get; set; }
        public string ReferenceName { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }

        public string ImageCover { get; set; }

        public bool IsActive { get; set; } = true;

        public int? FK_DestinationID { get; set; }

        public string DestinationName { get; set; }

        public List<TransferPricesDTO> PricesList { get; set; }
        public List<TransferIncludedDto> IncludesList { get; set; }
        public List<TransferNotIncludedDto> NotIncludedList { get; set; }
        public List<TransferHighlightDto> HighlightList { get; set; }


    }
}

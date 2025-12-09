using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Transfer
{
    public class TransferAllDto
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


    }
}

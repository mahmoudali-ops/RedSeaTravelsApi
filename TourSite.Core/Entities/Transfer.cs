using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }
        public string ReferenceName { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }

        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }

        public string ImageCover { get; set; }

        public bool IsActive { get; set; } = true;

        // FK
        public int? FK_DestinationID { get; set; }

        [ForeignKey(nameof(FK_DestinationID))]
        public Destination Destination { get; set; }

        // Relations
        public ICollection<TrasnferPrices> PricesList { get; set; }
        public ICollection<TransferIncluded> Includeds { get; set; }
        public ICollection<TransferNotIncluded> NotIncludeds { get; set; }
        public ICollection<TransferHighlight> Highlights { get; set; }
    }
}
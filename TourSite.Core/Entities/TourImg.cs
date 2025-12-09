using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class TourImg
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(150)]
        public string ReferenceName { get; set; }

        public string Title { get; set; }
        public string ImageCarouselUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public int? FK_TourId { get; set; }

        [ForeignKey(nameof(FK_TourId))]
        public Tour Tour { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }
        public string ReferenceName { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageCover { get; set; }
        public bool IsActive { get; set; } = true;
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }
        // Relations
        public ICollection<Tour> Tours { get; set; }
        public ICollection<Transfer> Transfers { get; set; }
    }
}

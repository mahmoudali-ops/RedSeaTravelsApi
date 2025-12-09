using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Tours;

namespace TourSite.Core.DTOs.CategoryTour
{
    public class CategorToutAllDto
    {
        public int Id { get; set; }
        public string ReferenceName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }
        public string ImageCover { get; set; }
        public bool IsActive { get; set; } = true;

    }
}

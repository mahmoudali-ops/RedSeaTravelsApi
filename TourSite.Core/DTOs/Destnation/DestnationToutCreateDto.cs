using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;

namespace TourSite.Core.DTOs.Destnation
{
    public class DestnationToutCreateDto
    {
        public IFormFile? ImageFile { get; set; }
        public bool IsActive { get; set; } = true;
        public string ReferenceName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }

    }
}

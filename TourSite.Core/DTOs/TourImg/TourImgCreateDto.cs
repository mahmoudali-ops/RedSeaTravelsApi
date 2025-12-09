using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.CategorToutCreateDto;

namespace TourSite.Core.DTOs.TourImg
{
    public class TourImgCreateDto
    {
        public IFormFile? ImageFile { get; set; }
        public string ReferenceName { get; set; }

        public string Title { get; set; }

        public bool IsActive { get; set; } = true;

        public int? FK_TourId { get; set; }

    }
}

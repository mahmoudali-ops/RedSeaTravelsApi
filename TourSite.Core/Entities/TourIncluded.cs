using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class TourIncluded
    {
        public int Id { get; set; }
        public int TourId { get; set; }

        [ForeignKey(nameof(TourId))]
        public Tour Tour { get; set; }

        public string Text { get; set; }
    }
}

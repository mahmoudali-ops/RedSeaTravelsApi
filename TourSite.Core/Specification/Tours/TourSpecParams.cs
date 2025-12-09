using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Specification.Tours
{
    public class TourSpecParams
    {
        public int pageSize { get; set; } = 20;
        public int pageIndex { get; set; } = 1;
    }
}

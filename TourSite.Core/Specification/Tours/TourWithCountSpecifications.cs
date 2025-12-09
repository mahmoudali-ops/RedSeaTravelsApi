using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Tours
{
    public class TourWithCountSpecifications : BaseSpecifications<Tour>
    {
        public TourWithCountSpecifications(TourSpecParams specParams) : 
            base(p => p.IsActive == true)
        { }

    }
}

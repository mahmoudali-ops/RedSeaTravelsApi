using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.CatgeoryTour
{
    public class CatTourWithCountSpecifications : BaseSpecifications<CategoryTour>
    {
        public CatTourWithCountSpecifications(CatTourSpecParams specParams)
          : base(p => p.IsActive == true)
        {
        }
    }
}

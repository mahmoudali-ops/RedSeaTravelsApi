using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.CatgeoryTour
{
    public class CatTourSpecificationForAdmin:BaseSpecifications<CategoryTour>
    {
        public CatTourSpecificationForAdmin(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public CatTourSpecificationForAdmin(CatTourSpecParams specParams) :
            base()
        {
            applyIncludes();
            ApplyPag(specParams.pageSize, specParams.pageIndex);
        }
        public void applyIncludes()
        {
            Includes.Add(t => t.Tours);

        }
    }
}

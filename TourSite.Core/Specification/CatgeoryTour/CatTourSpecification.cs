using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Core.Specification.Tours;

namespace TourSite.Core.Specification.CatgeoryTour
{
    public class CatTourSpecification :BaseSpecifications<CategoryTour>
    {
        public CatTourSpecification(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public CatTourSpecification(CatTourSpecParams specParams)
                 : base(p => p.IsActive == true)
        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {

            Includes.Add(c => c.Tours);// tours
            // بدل العبارات اللي كانت تسبب خطأ، نستخدم string paths
            IncludeStrings.Add("Tours.Destination");

        }
    }
}

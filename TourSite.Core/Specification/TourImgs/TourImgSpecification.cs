using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Core.Specification.CatgeoryTour;

namespace TourSite.Core.Specification.TourImgs
{
    public class TourImgSpecification:BaseSpecifications<TourImg>
    {
        public TourImgSpecification(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public TourImgSpecification(TourImgSpeciParams specParams) : 
            base(p => p.IsActive == true)

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {

        }
    }
}

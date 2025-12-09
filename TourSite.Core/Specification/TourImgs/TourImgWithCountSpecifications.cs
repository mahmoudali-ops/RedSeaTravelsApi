using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.TourImgs
{
    public class TourImgWithCountSpecifications : BaseSpecifications<TourImg>
    {
        public TourImgWithCountSpecifications(TourImgSpeciParams specParams) :
            base(p => p.IsActive == true)
        { }
    }
}

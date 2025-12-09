using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Tours
{
    public class TourSpecification : BaseSpecifications<Tour>
    {
        public TourSpecification(int id) : base(t => t.Id == id)
        {
            applyIncludes();

        }
        public TourSpecification(TourSpecParams specParams) : 
            base(p => p.IsActive == true)
        {
            
            applyIncludes();

            ApplyPag(specParams.pageSize, specParams.pageIndex);
        }

        public void applyIncludes()
        {
      

            Includes.Add(t => t.TourImgs);


            Includes.Add(t => t.Includeds);
            Includes.Add(t => t.NotIncludeds);
            Includes.Add(t => t.Highlights);

            Includes.Add(t => t.Destination);

            Includes.Add(t => t.Category);


        }


    }
}

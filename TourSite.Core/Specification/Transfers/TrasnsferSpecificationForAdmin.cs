using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Transfers
{
    public class TrasnsferSpecificationForAdmin:BaseSpecifications<Transfer>
    {
        public TrasnsferSpecificationForAdmin(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public TrasnsferSpecificationForAdmin(TrasferSpecParam specParams) :
            base()

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {
           
            Includes.Add(t => t.Destination);
            Includes.Add(t => t.PricesList);
            Includes.Add(t => t.Includeds);
            Includes.Add(t => t.NotIncludeds);
            Includes.Add(t => t.Highlights);
        }
    }
}

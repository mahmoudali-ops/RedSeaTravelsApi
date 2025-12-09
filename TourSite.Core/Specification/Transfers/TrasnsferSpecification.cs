using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Core.Specification.Users;


namespace TourSite.Core.Specification.Transfers
{
    public class TransferSpecification :  BaseSpecifications<Transfer>
    {
        public TransferSpecification(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public TransferSpecification(TrasferSpecParam specParams) : 
            base(p => p.IsActive == true)

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {
            Includes.Add(t => t.Includeds);
            Includes.Add(t => t.NotIncludeds);
            Includes.Add(t => t.Highlights);
            Includes.Add(t => t.Destination);
            Includes.Add(t => t.PricesList);

        }
    }
}

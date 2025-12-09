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
    public class TransferWithCountSpecifications : BaseSpecifications<Transfer>
    {
        public TransferWithCountSpecifications(TrasferSpecParam specParams) 
            : base(p => p.IsActive == true)
        { }
    }
}

using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Destnations
{
    public class DestnationWitCountSpecification : BaseSpecifications<Destination>
    {
        public DestnationWitCountSpecification(DestnationSpecParams specParams) : 
            base(p => p.IsActive == true)
        {
        }
    }
}

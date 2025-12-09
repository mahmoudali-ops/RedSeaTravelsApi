using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Destnations
{
    public class DestnationSpecification : BaseSpecifications<Destination>
    {
        public DestnationSpecification(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public DestnationSpecification(DestnationSpecParams specParams) : 
            base(p => p.IsActive == true)

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {
            Includes.Add(d => d.Tours);
        }
    }
}

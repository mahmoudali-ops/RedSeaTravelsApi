using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Destnations
{
    public class DestnationSpecificationForAdmin:BaseSpecifications<Destination>
    {
        public DestnationSpecificationForAdmin(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public DestnationSpecificationForAdmin(DestnationSpecParams specParams) :
            base()

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {
            // الرحلات + ترجمتها
            Includes.Add(d => d.Tours);
        }
    }
}

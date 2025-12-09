using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Tours
{
    public class TourForUpdateSpec : BaseSpecifications<Tour>
    {
        public TourForUpdateSpec(int id) : base(t => t.Id == id)
        {
            Includes.Add(t => t.Includeds);
            Includes.Add(t => t.NotIncludeds);
            Includes.Add(t => t.Highlights);

        }
    }
}

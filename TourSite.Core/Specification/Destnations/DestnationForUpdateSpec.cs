using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Destnations
{
    public class DestnationForUpdateSpec : BaseSpecifications<Destination>
    {
        public DestnationForUpdateSpec(int id) : base(t => t.Id == id)
        {
            // Include الترجمات فقط، لأننا محتاجين نحدثها

            // ممكن نضيف includes إضافية لو في حاجة تانية محتاجين نحدثها
            // مثلاً الصور أو tours لو المستقبل هيحتاج
            // Includes.Add(c => c.Tours);
        }
    }

}

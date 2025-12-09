using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Emails
{
    public class EmailSpecification : BaseSpecifications<Email>
    {
        public EmailSpecification(int id) : base(t => t.Id == id)
        {
            applyIncludes();
        }
        public EmailSpecification(EmailSpecParams specParams) : base(
            p =>
            (string.IsNullOrEmpty(specParams.Search) || p.FullName.ToLower().Contains(specParams.Search)))

        {
            ApplyPag(specParams.pageSize, specParams.pageIndex);
            applyIncludes();
        }

        public void applyIncludes()
        {
            Includes.Add(t => t.Tour);

        }
    }
}

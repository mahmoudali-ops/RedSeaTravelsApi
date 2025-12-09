using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;

namespace TourSite.Core.Specification.Emails
{
    public class EmailCountSpecifications : BaseSpecifications<Email>
    {
        public EmailCountSpecifications(EmailSpecParams specParams) : base(
            p =>
            (string.IsNullOrEmpty(specParams.Search) || p.FullName.ToLower().Contains(specParams.Search)))
        {
        }
    }
}

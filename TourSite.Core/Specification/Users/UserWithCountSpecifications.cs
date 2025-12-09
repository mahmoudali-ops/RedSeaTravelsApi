using Store.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.Entities;
using TourSite.Core.Specification.TourImgs;

namespace TourSite.Core.Specification.Users
{
    public class UserWithCountSpecifications : BaseSpecifications<User>
    {
        public UserWithCountSpecifications(UserSpeciParams specParams) : base(
            p =>
            (string.IsNullOrEmpty(specParams.Search) || p.FullName.ToLower().Contains(specParams.Search)))
        { }
    }
}

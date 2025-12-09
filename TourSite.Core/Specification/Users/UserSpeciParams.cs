using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Specification.Users
{
    public class UserSpeciParams
    {
        private string? search;
        public string? Search
        {
            get => search;
            set => search = value?.ToLower();
        }
        public int pageSize { get; set; } = 30;
        public int pageIndex { get; set; } = 1;

    }
}

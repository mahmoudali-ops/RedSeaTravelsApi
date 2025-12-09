using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Specification.Emails
{
    public class EmailSpecParams
    {
        private string? search;
        public string? Search
        {
            get => search;
            set => search = value?.ToLower();
        }
        public int pageSize { get; set; } = 100;
        public int pageIndex { get; set; } = 1;
    }
}

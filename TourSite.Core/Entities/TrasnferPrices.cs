using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class TrasnferPrices
    {
        public int Id { get; set; }
        public int TransferId { get; set; }

        [ForeignKey(nameof(TransferId))]
        public Transfer Transfer { get; set; }

        public string Title { get; set; }

        public decimal PrivtePrice { get; set; }

        public decimal SharedPrice { get; set; }

    }
}

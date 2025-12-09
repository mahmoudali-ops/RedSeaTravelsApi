using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.DTOs.Transfer
{
    public class TransferPricesDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public decimal PrivtePrice { get; set; }

        public decimal SharedPrice { get; set; }

    }
}

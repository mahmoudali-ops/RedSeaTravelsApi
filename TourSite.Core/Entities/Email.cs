using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class Email
    {
        [Key]
        public int Id { get; set; }

        public int? AdultsNumber { get; set; }

        public int? ChildernNumber { get; set; }

        [Required, MaxLength(150)]
        public string FullName { get; set; }

        [Required, MaxLength(150)]
        public string? FullTourName { get; set; }

        [Required, MaxLength(200)]
        public string EmailAddress { get; set; }

        [Required]
        public string Message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? BookingDate { get; set; }

        // Foreign Key
        public int? FK_TourId { get; set; }

        [ForeignKey(nameof(FK_TourId))]
        public Tour Tour { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourSite.Core.Entities
{
    public class User : IdentityUser
    {
        
        [Required, MaxLength(150)]
        public string FullName { get; set; }

        public bool IsActive { get; set; } = true;

        // Relations
        public ICollection<Tour> Tours { get; set; }
    }
}

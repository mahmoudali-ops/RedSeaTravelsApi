using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Tours;

namespace TourSite.Core.DTOs.UserAuth
{
    public class LoginAuthDto
    {
     
        [Required(ErrorMessage = "User password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "User email is required")]
        [EmailAddress]
        public string Email { get; set; }

    }
}

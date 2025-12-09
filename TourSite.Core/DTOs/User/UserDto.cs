using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;

namespace TourSite.Core.DTOs.User
{
    public class UserDto
    {
        public string Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsActive { get; set; } 

    }
    public class UserAuthDto
    {
        public string Token { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }

    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.TourImg;
using TourSite.Core.DTOs.User;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
                
        }

    }
}

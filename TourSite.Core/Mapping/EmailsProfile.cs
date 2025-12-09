using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Email;
using TourSite.Core.DTOs.Tours;
using TourSite.Core.Entities;

namespace TourSite.Core.Mapping
{
    public class EmailsProfile : Profile
    {
        public EmailsProfile()
        {
            CreateMap<Email, EmailsDto>();      
        }   
    
    }

}

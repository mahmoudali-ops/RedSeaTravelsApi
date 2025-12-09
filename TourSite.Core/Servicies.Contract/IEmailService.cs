using Store.Core.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourSite.Core.DTOs.Destnation;
using TourSite.Core.DTOs.Email;
using TourSite.Core.Specification.Emails;

namespace TourSite.Core.Servicies.Contract
{
    public interface IEmailService
    {
        Task<PageinationResponse<EmailsDto>> GetAllEmailsToursAsync(EmailSpecParams SpecParams);
        Task<EmailsDto> GetCatEmailByIdAsync(int id);

        Task AddEmailAsync(EmailsDto EmasilDto);

        Task<Boolean> DeleteEmailAsync(int id);

    }
}

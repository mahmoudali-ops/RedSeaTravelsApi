using AutoMapper;
using Store.Core.Helper;
using TourSite.Core;
using TourSite.Core.DTOs.User;
using TourSite.Core.Entities;
using TourSite.Core.Servicies.Contract;
using TourSite.Core.Specification.Users;

namespace TourSite.Service.Services.Users
{
    public class UserService : IUserService
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<PageinationResponse<UserDto>> GetAllUserAsync(UserSpeciParams specParams)
        {
            var spec = new UserSpecification(specParams);
            var data = mapper.Map<IEnumerable<UserDto>>(await unitOfWork.Repository<User>().GetAllSpecAsync(spec));
            var CountSpec = new UserWithCountSpecifications(specParams);
            var Count = await unitOfWork.Repository<User>().GetCountAsync(CountSpec);
            return new PageinationResponse<UserDto>(specParams.pageIndex, specParams.pageSize, Count, data);

        }



        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            var spec = new UserSpecification(id);
            return mapper.Map<UserDto>(await unitOfWork.Repository<User>().GetByIdSpecAsync(spec));
        }
    }
}

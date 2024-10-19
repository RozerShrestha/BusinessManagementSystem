using AutoMapper;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRoles.SingleOrDefault().Role.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRoles.SingleOrDefault().Role.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth.ToString())))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => Helper.Helpers.GenerateGUID()));



        }
    }
}

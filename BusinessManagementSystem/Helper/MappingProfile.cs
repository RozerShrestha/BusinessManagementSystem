using AutoMapper;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Utility;

namespace BusinessManagementSystem.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //mapping UserDto to User
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRoles.SingleOrDefault().Role.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRoles.SingleOrDefault().Role.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            //mapping User to UserDto
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth.ToString())))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>src.UserId))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => Helper.Helpers.GenerateGUID()));

            //mapping Appointment to AppointmentDto
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest => dest.ReferalName, opt => opt.MapFrom(src => src.Referal.FullName));



            //string occupationValue = "";
            //CreateMap<User, User>()
            //    .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => SD.Occupations.TryGetValue(src.Occupation, out occupationValue)));

        }
    }
}

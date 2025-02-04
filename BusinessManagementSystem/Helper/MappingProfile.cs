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
            //mapping User to UserDto
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.UserRoles.SingleOrDefault().Role.Id))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.UserRoles.SingleOrDefault().Role.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.userGuid, opt => opt.MapFrom(src => src.Guid));
            //mapping UserDto to User
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth.ToString())))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src =>src.UserId))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => Helper.Helpers.GenerateGUID()));

            //Mapping User to UserDetailDto
            CreateMap<User, UserDetailDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Appointments, opt => opt.MapFrom(src =>src.Appointments ));

            //mapping Appointment to AppointmentDto
            CreateMap<Appointment, AppointmentDto>()
                .ForMember(dest => dest.AppointmentId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Deposit, opt => opt.MapFrom(src => src.Payment.Deposit))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.Payment.PaymentMethod))
                .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Payment.Discount))
                .ForMember(dest => dest.DiscountInHour, opt => opt.MapFrom(src => src.Payment.DiscountInHour))
                .ForMember(dest => dest.DueAmount, opt => opt.MapFrom(src => src.Payment.DueAmount))
                .ForMember(dest => dest.TotalCost, opt => opt.MapFrom(src => src.Payment.TotalCost))
                .ForMember(dest => dest.ReferalFullName, opt => opt.MapFrom(src => src.Referal.FullName))
                .ForMember(dest => dest.TipAmount, opt => opt.MapFrom(src => src.Payment.TipAmount))
                .ForMember(dest => dest.ArtistAssigned, opt => opt.MapFrom(src => src.User.FullName));
                


            //mapping AppointmentDto to Appointment
            CreateMap<AppointmentDto, Appointment>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.AppointmentId))
                .ForMember(dest => dest.Payment, opt => opt.Ignore())
                .ForMember(dest => dest.Referal, opt => opt.Ignore());

                //.ForMember(dest => dest.Payment.TipAmount, opt => opt.MapFrom(src => src.TipAmount));

            //mapping AppointmentDto to Payment
            CreateMap<AppointmentDto, Payment>();

            //mapping Tip to TipDto
            CreateMap<Tip, TipDto>()
                .ForMember(dest => dest.TipId, opt => opt.MapFrom(src => src.Id));


            //string occupationValue = "";
            //CreateMap<User, User>()
            //    .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => SD.Occupations.TryGetValue(src.Occupation, out occupationValue)));

        }
    }
}

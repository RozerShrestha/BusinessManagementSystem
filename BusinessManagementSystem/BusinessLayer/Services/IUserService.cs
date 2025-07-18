﻿using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IUserService
    {
        ResponseDto<UserRoleDto> GetAllUser(string roleName);
        ResponseDto<User> GetUserById(int id);
        ResponseDto<UserDetailDto> GetUserDetailDtoById(int id);

        ResponseDto<UserDto> GetUserByGuid(Guid guid);
        ResponseDto<User> GetAllActiveUsers();
        ResponseDto<User> GetSuperadminUser();

        ResponseDto<User> GetAllInactiveUsers();
        ResponseDto<User> CreateUser(UserDto userDto);
        ResponseDto<User> UpdateUser(UserDto userDto);
        ResponseDto<User> DeleteUser(int id);
        dynamic RoleList();
        dynamic GetAllActiveTattooArtist();
        dynamic GetAllActiveTattooArtistWithoutAll();
        dynamic GetArtist(Guid guid);
        bool ValidateUserName(string username);
        bool ValidateEmail(string email);
        bool ValidatePhoneNumber(string phoneNumber);
    }
}

using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IUserService
    {
        ResponseDto<UserRoleDto> GetAllUser(string roleName);
        ResponseDto<User> GetUserById(int id);
        ResponseDto<User> GetAllActiveUsers();
        ResponseDto<User> GetAllInactiveUsers();
        ResponseDto<User> CreateUser(UserDto userDto);
        ResponseDto<User> UpdateUser(User u);
        ResponseDto<User> DeleteUser(int id);
        dynamic RoleList();

    }
}

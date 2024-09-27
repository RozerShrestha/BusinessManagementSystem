using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Org.BouncyCastle.Asn1.Ocsp;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IUserService
    {
        ResponseDto<User> GetAllUser();
        ResponseDto<User> GetUserById(int id);
        ResponseDto<User> GetAllActiveUsers();
        ResponseDto<User> GetAllInactiveUsers();
        ResponseDto<User> Create(UserDto userDto);
        ResponseDto<User> Update(User u);
        ResponseDto<User> Delete(int id);

    }
}

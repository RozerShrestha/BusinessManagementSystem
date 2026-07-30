using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IUser:IGeneric<User>
    {
        List<User> GetAllActiveUsers();
        List<User> GetAllInactiveUsers();
        ResponseDto<UserRoleDto> GetAllUser(string filter);
        ResponseDto<User> GetAllSuperAdmin();
        //ResponseDto<UserDto> GetUser(Guid guid);
        dynamic RoleList();
        dynamic ArtistList();
        dynamic ArtistListWithoutAll();
        dynamic ArtistSelf(Guid guid);

    }
}


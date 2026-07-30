using TattooAppointmentSystem.Dto;

namespace TattooAppointmentSystem.Services
{
    public interface IBase
    {
        UserDto UserDetail(string userName);
        List<MenuDto> MenuList(string roleName);

        dynamic RoleList();
        
    }
}


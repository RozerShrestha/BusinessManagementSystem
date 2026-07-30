using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Services;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IBaseService
    {
        UserDto UserDetail(string userName);
        List<MenuDto> MenuList(string roleName);
        RequestDto GetInitialRequestDtoFilter(string filter);
        RequestDto GetInitialRequestDtoFilterDashboard();

    }
}


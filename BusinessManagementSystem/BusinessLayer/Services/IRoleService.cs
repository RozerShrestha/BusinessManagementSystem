using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IRoleService
    {
        ResponseDto<Role> GetAllRoles();
        ResponseDto<Role> GetRoleById(int id);
        ResponseDto<Role> CreateRole(Role role);
        ResponseDto<Role> UpdateRole(Role role);
        ResponseDto<Role> DeleteRole(Role role);
    }
}


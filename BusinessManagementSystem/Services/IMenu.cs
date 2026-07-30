using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IMenu : IGeneric<Menu>
    {
        dynamic ParentList();
        dynamic RoleList();
        ResponseDto<Menu> GetMenuById(int id);
        Task<ResponseDto<Menu>> CreateMenu(Menu menu);
        ResponseDto<Menu> UpdateMenu(Menu menu);
        Task<ResponseDto<Menu>> GetAllMenuAsync();
    }
}


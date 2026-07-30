using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IMenuService
    {
        dynamic ParentList();
        Multiselect RoleList();
        Task<ResponseDto<Menu>> GetAllMenu();
        ResponseDto<Menu> GetMenuById(int id);
        ResponseDto<Menu> GetAllActiveMenus();
        ResponseDto<Menu> GetAllInactiveMenus();
        Task<ResponseDto<Menu>> CreateMenu(Menu menu);
        ResponseDto<Menu> UpdateMenu(Menu u);
        ResponseDto<Menu> DeleteMenu(int id);
    }
}


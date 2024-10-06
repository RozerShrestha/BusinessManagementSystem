using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IMenuService
    {
        dynamic ParentList();
        dynamic RoleList();
        ResponseDto<Menu> GetAllMenu();
        ResponseDto<Menu> GetMenuById(int id);
        ResponseDto<Menu> GetAllActiveMenus();
        ResponseDto<Menu> GetAllInactiveMenus();
        ResponseDto<Menu> Create(Menu menu);
        ResponseDto<Menu> Update(Menu u);
        ResponseDto<Menu> Delete(int id);
    }
}

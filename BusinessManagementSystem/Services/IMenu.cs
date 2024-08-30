using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IMenu : IGeneric<Menu>
    {
        dynamic ParentList();
        ResponseDto<Menu> CreateMenu(Menu menu);
        ResponseDto<Menu> UpdateMenu(Menu menu);
    }
}

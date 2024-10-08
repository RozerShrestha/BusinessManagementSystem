using AspNetCore;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using System.Net;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class MenuImpl : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        ResponseDto<Menu> _responseDto;

        public MenuImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDto<Menu>();
        }

        public dynamic ParentList()
        {
            var parentList=_unitOfWork.Menu.ParentList();
            return parentList;
        }

        public dynamic RoleList()
        {
            var roleList = _unitOfWork.Role.GetRoles();
            return roleList;
        }
        public ResponseDto<Menu> GetAllActiveMenus()
        {
            _responseDto = _unitOfWork.Menu.GetAll(p => p.Status == true);
            return _responseDto;
        }

        public ResponseDto<Menu> GetAllInactiveMenus()
        {
            _responseDto = _unitOfWork.Menu.GetAll(p => p.Status == false);
            return _responseDto;
        }

        public ResponseDto<Menu> GetAllMenu()
        {
            _responseDto = _unitOfWork.Menu.GetAll();
            return _responseDto;
        }

        public ResponseDto<Menu> GetMenuById(int id)
        {
            _responseDto = _unitOfWork.Menu.GetFirstOrDefault(p => p.Id == id);
            return _responseDto;
        }

        public ResponseDto<Menu> Create(Menu menu)
        {
            _responseDto = _unitOfWork.Menu.CreateMenu(menu);
            return _responseDto;
        }

        public ResponseDto<Menu> Update(Menu u)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Menu> Delete(int id)
        {
            Menu menu=_unitOfWork.Menu.GetById(id).Data;
            if(menu != null)
            {
                _responseDto = _unitOfWork.Menu.Delete(menu);
            }
            else
            {
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.Message = "Menu not found";
            }
            return _responseDto;

        }

        public dynamic GetMenuRoles(int id)
        {
            var menuAssignedRoles = _unitOfWork.MenuRole.GetRolesAssignedToMenu(id);
            return menuAssignedRoles;
        }
    }
}

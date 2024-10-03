using AspNetCore;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

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
            var response = _unitOfWork.Menu.GetFirstOrDefault(p => p.Id == id);
            return response;
        }

        public ResponseDto<Menu> Create(Menu menu)
        {
            var response = _unitOfWork.Menu.CreateMenu(menu);
            return response;
        }

        public ResponseDto<Menu> Update(Menu u)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<Menu> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}

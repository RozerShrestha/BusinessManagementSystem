using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class BaseImpl : IBaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<MenuDto> MenuList(string roleName)
        {
            var menuDtoList=_unitOfWork.Base.MenuList(roleName);
            return menuDtoList;
        }

        public UserDto UserDetail(string userName)
        {
            var userDto= _unitOfWork.Base.UserDetail(userName);
            return userDto;
        }

        public dynamic RoleList()
        {
            var roleLIst = _unitOfWork.Base.RoleList();
            return roleLIst;
        }

        public RequestDto GetInitialRequestDtoFilter()
        {
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            RequestDto requestDto = new RequestDto
            {
                Status = AppointmentStat.All.ToString(),
                StartDate = new DateTime(2025, 01, 01),/*new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),*/
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDay)
            };
            return requestDto;
        }

        public RequestDto GetInitialRequestDtoFilterDashboard()
        {
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            RequestDto requestDto = new RequestDto
            {
                Status = AppointmentStat.All.ToString(),
                StartDate = new DateTime(2025,01,01),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDay)
            };
            return requestDto;
        }
    }
}

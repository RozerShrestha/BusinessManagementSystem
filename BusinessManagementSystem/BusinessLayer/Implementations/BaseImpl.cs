using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
using Org.BouncyCastle.Bcpg.OpenPgp;

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

        public RequestDto GetInitialRequestDtoFilter(string filter="")
        {
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            string startDateNep = NepaliDateService.EngToNep(DateTime.Now).ToString();
            string endDateNep = NepaliDateService.GetLastDayOfNepMonth(startDateNep);

            RequestDto requestDto = new RequestDto
            {
                Status = AppointmentStat.All.ToString(),
                StartDateNep=NepaliDateService.GetFirstDayOfNepMonth(startDateNep),
                EndDateNep=endDateNep,
                //StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                //EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDay)
            };
            return requestDto;
        }

        public RequestDto GetInitialRequestDtoFilterDashboard()
        {
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            string startDateNep = NepaliDateService.EngToNep(DateTime.Now).ToString();
            string endDateNep = NepaliDateService.GetLastDayOfNepMonth(startDateNep);
            //string endDateNep = NepaliDateService.GetLastDayOfNepMonth("2082-06-15");

            RequestDto requestDto = new RequestDto
            {
                Status = AppointmentStat.All.ToString(),
                StartDateNep = NepaliDateService.GetFirstDayOfNepMonth(startDateNep),
                EndDateNep = endDateNep,
                //StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                //EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDay)
            };
            return requestDto;
        }
    }
}

using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Dto.Chart;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.ViewModels;

namespace TattooAppointmentSystem.Services
{
    public interface IDashboard
    {
        ResponseDto<DashboardDto> GetDashboardInfo(RequestDto requestDto, int userId);
        ResponseDto<DashboardDto> GetDashboardInfoAllEmployee(RequestDto requestDto);
    }
}

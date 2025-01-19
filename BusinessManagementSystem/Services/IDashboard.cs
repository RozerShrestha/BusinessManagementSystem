using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.ViewModels;

namespace BusinessManagementSystem.Services
{
    public interface IDashboard
    {
        ResponseDto<DashboardDto> GetDashboardInfo(RequestDto requestDto, int userId);
        ResponseDto<DashboardDto> GetDashboardInfoAllEmployee(RequestDto requestDto);
    }
}
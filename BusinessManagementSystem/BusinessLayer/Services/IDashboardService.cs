using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Dto.Chart;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IDashboardService
    {
        string GetIncomeSegregation(RequestDto requestDto);
        string GetPaymentTipSegregation(RequestDto requestDto);
        ResponseDto<DashboardDto> GetDashboardInfo(RequestDto requestDto, int userId);
        ResponseDto<DashboardDto> GetDashboardInfoAllEmployee(RequestDto requestDto);
    }
}


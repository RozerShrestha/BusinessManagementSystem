using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IDashboardService
    {
        string GetIncomeSegregation(RequestDto requestDto);
        string GetPaymentTipSegregation(RequestDto requestDto);
        ResponseDto<DashboardDto> GetDashboardInfo(RequestDto requestDto, int userId);
        ResponseDto<DashboardDto> GetDashboardInfoAllEmployee(RequestDto requestDto);
    }
}

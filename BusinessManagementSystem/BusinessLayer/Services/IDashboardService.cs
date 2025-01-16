using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IDashboardService
    {
        string GetIncomeSegregation();
        string GetPaymentTipSegregation();
    }
}

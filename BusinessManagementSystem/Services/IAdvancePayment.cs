using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IAdvancePayment: IGeneric<AdvancePayment>
    {
        ResponseDto<AdvancePaymentDto> GetAdvancePaymentSettlement(RequestDto requestDto);
    }
}

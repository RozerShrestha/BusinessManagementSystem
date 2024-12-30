using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IPaymentService
    {
        ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto);
        ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto);
        ResponseDto<PaymentHistory> GetPaymentHistory(RequestDto requestDto);
        bool UpdatePaymentTipSettlement(PaymentTipSettlementDto paymentTipSettlementDto);

    }
}

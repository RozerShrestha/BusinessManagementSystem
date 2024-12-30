using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IPayment : IGeneric<Payment>
    {
        ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto);
        ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto);
        ResponseDto<PaymentHistory> CreatePaymentHistory(PaymentHistory paymentHistory);
        ResponseDto<PaymentHistory> GetPaymentHistory(RequestDto requestDto);

    }
}

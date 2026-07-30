using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IPayment : IGeneric<Payment>
    {
        ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto);
        dynamic GetAllPaymentsDashboard(RequestDto requestDto);
        dynamic GetAllPaymentSegregation(RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto);
        ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto);
        ResponseDto<PaymentHistory> CreatePaymentHistory(PaymentHistory paymentHistory);
        ResponseDto<PaymentHistory> GetPaymentHistory(RequestDto requestDto);

    }
}


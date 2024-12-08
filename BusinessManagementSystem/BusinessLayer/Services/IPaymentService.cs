using BusinessManagementSystem.Dto;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IPaymentService
    {
        ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto);
        ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto);
    }
}

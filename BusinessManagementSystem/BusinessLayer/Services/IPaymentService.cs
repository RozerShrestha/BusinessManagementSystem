using BusinessManagementSystem.Dto;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IPaymentService
    {
        ResponseDto<PaymentDto> GetAllPayments();
        ResponseDto<PaymentDto> GetMyPayments(int userId);
        ResponseDto<PaymentDto> GetMyPayments(Guid guid);
    }
}

using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IPayment : IGeneric<Payment>
    {
        ResponseDto<PaymentDto> GetAllPayments();
        ResponseDto<PaymentDto> GetMyPayments(int userId);
        ResponseDto<PaymentDto> GetMyPayments(Guid guid);

    }
}

using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IAdvancePaymentService
    {
        ResponseDto<AdvancePayment> CreateAdvancePayment(AdvancePayment advancePayment);
        ResponseDto<AdvancePayment> GetAllAdvancePayment(RequestDto requestDto);
        ResponseDto<AdvancePayment> GetMyAdvancePayment(RequestDto requestDto, int userId);
        ResponseDto<AdvancePayment> GetAdvancePayment(int id);
        ResponseDto<AdvancePayment> GetAdvancePayment(Guid guid);
        ResponseDto<AdvancePayment> UpdateAdvancePayment(AdvancePayment advancePayment);
        ResponseDto<AdvancePayment> DeleteAdvancePayment(int id);

        


    }
}


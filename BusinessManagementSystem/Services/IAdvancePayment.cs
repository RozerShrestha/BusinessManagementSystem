using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IAdvancePayment: IGeneric<AdvancePayment>
    {
        ResponseDto<AdvancePaymentDto> GetAdvancePaymentSettlement(RequestDto requestDto);
    }
}


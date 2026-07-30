using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface ITipService
    {
        ResponseDto<TipDto> GetAllTips(RequestDto requestDto);
        ResponseDto<TipDto> GetMyTips(int userId, RequestDto requestDto);
    }
}


using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface ITipService
    {
        ResponseDto<TipDto> GetAllTips(RequestDto requestDto);
        ResponseDto<TipDto> GetMyTips(int userId, RequestDto requestDto);
    }
}

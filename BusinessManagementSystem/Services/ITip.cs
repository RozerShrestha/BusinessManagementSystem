using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface ITip : IGeneric<Tip>
    {
        ResponseDto<TipDto> GetAllTips(RequestDto requestDto);
        ResponseDto<TipDto> GetMyTips(int userId, RequestDto requestDto);
    }
}

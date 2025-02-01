using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface ITip : IGeneric<Tip>
    {
        ResponseDto<TipDto> GetAllTips(RequestDto requestDto);
        dynamic GetAllTipsDashboard(RequestDto requestDto);
        dynamic GetTipsSegregation(RequestDto requestDto);
        ResponseDto<TipDto> GetMyTips(int userId, RequestDto requestDto);
        ResponseDto<TipSettlementDto> GetTipSettlement(RequestDto requestDto);
    }
}

using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface ITipService
    {
        ResponseDto<TipDto> GetAllTips();
        ResponseDto<TipDto> GetMyTips(int userId);
    }
}

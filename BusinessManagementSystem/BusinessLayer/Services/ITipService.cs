using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface ITipService
    {
        ResponseDto<Tip> GetAllTips();
        ResponseDto<Tip> GetMyTips(int userId);
    }
}

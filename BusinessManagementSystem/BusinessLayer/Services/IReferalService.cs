using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IReferalService
    {
        ResponseDto<Referal> GetAllReferal();
        ResponseDto<Referal> GetReferalById(int id);
        ResponseDto<Referal> GetReferalByStatus(string status);
        ResponseDto<Referal> CreateReferal(Referal referal);
        ResponseDto<Referal> UpdateReferal(Referal referal);
        ResponseDto<Referal> DeleteReferalById(int id);
        dynamic GetAllActiveReferalList();
    }
}


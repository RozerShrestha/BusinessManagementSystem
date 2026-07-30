using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.BusinessLayer.Services
{
    public interface IBasicConfigurationService
    {
        Task<ResponseDto<BasicConfiguration>> GetBasicConfig();
        Task<ResponseDto<BasicConfiguration>> Update(BasicConfiguration basicConfiguration);
    }
}


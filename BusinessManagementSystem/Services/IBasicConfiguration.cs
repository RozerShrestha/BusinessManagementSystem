using Azure;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;

namespace TattooAppointmentSystem.Services
{
    public interface IBasicConfiguration : IGeneric<BasicConfiguration>
    {
        Task<ResponseDto<BasicConfiguration>> UpdateBasicConfigurationDetail(BasicConfiguration basicConfiguration);
    }
}


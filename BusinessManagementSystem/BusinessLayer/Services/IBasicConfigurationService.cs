using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.BusinessLayer.Services
{
    public interface IBasicConfigurationService
    {
        Task<ResponseDto<BasicConfiguration>> GetBasicConfig();
        Task<ResponseDto<BasicConfiguration>> Update(BasicConfiguration basicConfiguration);
    }
}

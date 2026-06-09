using Azure;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IBasicConfiguration : IGeneric<BasicConfiguration>
    {
        Task<ResponseDto<BasicConfiguration>> UpdateBasicConfigurationDetail(BasicConfiguration basicConfiguration);
    }
}

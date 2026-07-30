using TattooAppointmentSystem.BusinessLayer.Services;
using TattooAppointmentSystem.Controllers;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;
using System.Runtime.CompilerServices;

namespace TattooAppointmentSystem.BusinessLayer.Implementations
{
    public class BasicConfigurationImpl : IBasicConfigurationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private ResponseDto<BasicConfiguration> _responseDto;

        public BasicConfigurationImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _responseDto = new ResponseDto<BasicConfiguration>();
        }
        public async Task<ResponseDto<BasicConfiguration>> GetBasicConfig()
        {
            var responseDto = await _unitOfWork.BasicConfiguration.GetSingleOrDefaultAsync();
            return responseDto;
        }

        public ResponseDto<BasicConfiguration> GetBasicConfig(int id)
        {
            var response = _unitOfWork.BasicConfiguration.GetFirstOrDefault(p => p.Id == id, null, false);
            return response;
        }
        
        public async Task<ResponseDto<BasicConfiguration>> Update(BasicConfiguration basicConfiguration)
        {
            var response=await _unitOfWork.BasicConfiguration.UpdateBasicConfigurationDetail(basicConfiguration);
            return response;
        }
    }
}


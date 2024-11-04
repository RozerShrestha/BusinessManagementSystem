using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.BusinessLayer.Implementations
{
    public class AppointmentImpl : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        ResponseDto<Appointment> _responseDto;
        ResponseDto<AppointmentDto> _responseAppointmentDto;

        public AppointmentImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<Appointment>();
            _responseAppointmentDto = new ResponseDto<AppointmentDto>();
            
        }
        public ResponseDto<AppointmentDto> CreateAppointment(Appointment appointment)
        {
            return _responseAppointmentDto;
        }

        public ResponseDto<AppointmentDto> DeleteAppointmentByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<AppointmentDto> DeleteAppointmentById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<AppointmentDto> GetAllAppointment()
        {
            throw new NotImplementedException();
        }

        public ResponseDto<AppointmentDto> GetAppointmentByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<AppointmentDto> GetAppointmentById(int id)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<AppointmentDto> GetAppointmentByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public ResponseDto<AppointmentDto> UpdateAppointment(Appointment appointment)
        {
            throw new NotImplementedException();
        }
    }
}

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
        int totalCost;

        public AppointmentImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _responseDto = new ResponseDto<Appointment>();
            _responseAppointmentDto = new ResponseDto<AppointmentDto>();
            
        }
        
        public ResponseDto<AppointmentDto> GetAllAppointment()
        {
            _responseDto = _unitOfWork.Appointment.GetAll(includeProperties: "User,Referal");
            foreach(var item in _responseDto.Datas)
            {
                AppointmentDto appointmentDto = new AppointmentDto();
                appointmentDto = _mapper.Map<AppointmentDto> (item);
                _responseAppointmentDto.Datas.Add(appointmentDto);
            }
            return _responseAppointmentDto;
        }

        public ResponseDto<AppointmentDto> GetAllAppointmentByArtist(int userId)
        {
            _responseDto = _unitOfWork.Appointment.GetAll(p => p.UserId == userId, includeProperties: "User,Referal");
            foreach (var item in _responseDto.Datas)
            {
                AppointmentDto appointmentDto = new AppointmentDto();
                appointmentDto = _mapper.Map<AppointmentDto>(item);
                _responseAppointmentDto.Datas.Add(appointmentDto);
            }
            return _responseAppointmentDto;
        }

        public ResponseDto<Appointment> GetAppointmentByGuid(Guid guid)
        {
            _responseDto = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == guid);
            return _responseDto;
        }

        public ResponseDto<Appointment> GetAppointmentById(int id)
        {
            _responseDto = _unitOfWork.Appointment.GetById(id);
            return _responseDto;
        }

        public ResponseDto<Appointment> GetAppointmentByStatus(string status)
        {
            _responseDto = _unitOfWork.Appointment.GetFirstOrDefault(p => p.Status == status);
            return _responseDto;
        }
        public ResponseDto<Appointment> CreateAppointment(Appointment appointment)
        {
            appointment.guid = Helper.Helpers.GenerateGUID();
            _responseDto = _unitOfWork.Appointment.Insert(appointment);
            return _responseDto;
        }

        public ResponseDto<Appointment> DeleteAppointmentByGuid(Guid guid)
        {
            var item = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == guid).Data;
            _responseDto = _unitOfWork.Appointment.Delete(item);
            return _responseDto;
        }

        public ResponseDto<Appointment> DeleteAppointmentById(int id)
        {
            var item = _unitOfWork.Appointment.GetById(id).Data;
            _responseDto = _unitOfWork.Appointment.Delete(item);
            return _responseDto;
        }

        public ResponseDto<Appointment> UpdateAppointment(Appointment appointment)
        {
            _responseDto = _unitOfWork.Appointment.Update(appointment);
            return _responseDto;
        }

        public string GetTotalCost(bool isForeigner, string category, int totalHours, int deposit, int discount, int discountInHour,out int totalCost)
        {
            double categoryCost=0;
            if (category == "Tattoo")
            {
               categoryCost = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.TattooPrice;
            }
            else if (category == "Dreadlock")
            {
                categoryCost= _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.DreadLockPrice;
            }
            else if (category == "Piercing")
            {
               categoryCost = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.PiercingPrice;
            }

            categoryCost = isForeigner ? categoryCost * 2 : categoryCost;

            totalCost = Convert.ToInt32(categoryCost) * (totalHours - discountInHour) - deposit - discount;

            string calculationDescription = $"Category: {category}({categoryCost}) \n Deposit: {deposit} \n Total Hours: {totalHours}-{discountInHour} \n Discount in Price: {discount} \n TotalCost: {totalCost}";

            return calculationDescription; 
        }

        
    }
}

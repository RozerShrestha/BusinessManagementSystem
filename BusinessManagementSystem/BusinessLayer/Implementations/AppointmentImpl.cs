 using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Newtonsoft.Json;
using System.Net;

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
            try
            {
                _responseDto = _unitOfWork.Appointment.GetAll(includeProperties: "User,Referal,Payment");
                if (_responseDto.Datas.Count > 0)
                {
                    foreach (var item in _responseDto.Datas)
                    {
                        AppointmentDto appointmentDto = new AppointmentDto();
                        appointmentDto = _mapper.Map<AppointmentDto>(item);
                        _responseAppointmentDto.Datas.Add(appointmentDto);
                    }
                }
                else
                {
                    _responseAppointmentDto.StatusCode=_responseDto.StatusCode;
                    _responseAppointmentDto.Message=_responseDto.Message;
                }
            }
            catch (Exception ex)
            {
                _responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseAppointmentDto.Message= ex.Message;
            }
            return _responseAppointmentDto;
        }
        public ResponseDto<AppointmentDto> GetAllAppointmentByArtist(int userId)
        {
            try
            {
                _responseDto = _unitOfWork.Appointment.GetAll(p => p.UserId == userId, includeProperties: "User,Referal,Payment");
                if (_responseDto.Datas.Count > 0)
                {
                    foreach (var item in _responseDto.Datas)
                    {
                        AppointmentDto appointmentDto = new AppointmentDto();
                        appointmentDto = _mapper.Map<AppointmentDto>(item);
                        _responseAppointmentDto.Datas.Add(appointmentDto);
                    }
                }
                else
                {
                    _responseAppointmentDto.StatusCode = _responseDto.StatusCode;
                    _responseAppointmentDto.Message = _responseDto.Message;
                }
            }
            catch (Exception ex)
            {
                _responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseAppointmentDto.Message = ex.Message;
            }
            
            return _responseAppointmentDto;
        }
        public ResponseDto<AppointmentDto> GetAppointmentByGuid(Guid guid)
        {
            try
            {
                _responseDto = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == guid, includeProperties: "Payment");
                if(_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _responseAppointmentDto.Data = _mapper.Map<AppointmentDto>(_responseDto.Data);
                }
                else
                {
                    _responseAppointmentDto.StatusCode=_responseDto.StatusCode;
                    _responseAppointmentDto.Message=_responseDto.Message;
                }
               
            }
            catch (Exception ex)
            {
                _responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseAppointmentDto.Message = ex.Message;
            }
            return _responseAppointmentDto;
        }
        public ResponseDto<AppointmentDto> GetAppointmentById(int id)
        {
            try
            {
                _responseDto = _unitOfWork.Appointment.GetById(id);
                if(_responseDto.StatusCode == HttpStatusCode.OK)
                {
                    _responseAppointmentDto.Data = _mapper.Map<AppointmentDto>(_responseDto.Data);
                }
                else
                {
                    _responseAppointmentDto.StatusCode=_responseDto.StatusCode;
                    _responseAppointmentDto.Message=_responseDto.Message;
                }
                
            }
            catch (Exception ex)
            {
                _responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseAppointmentDto.Message = ex.Message;
            }
           
            return _responseAppointmentDto;
        }
        public ResponseDto<Appointment> GetAppointmentByStatus(string status)
        {
            _responseDto = _unitOfWork.Appointment.GetFirstOrDefault(p => p.Status == status);
            return _responseDto;
        }       
        public ResponseDto<Appointment> CreateAppointment(AppointmentDto appointmentDto)
        {
            try
            {
                Appointment appointment = new Appointment();
                appointment = _mapper.Map<Appointment>(appointmentDto);
                appointment.guid = Helper.Helpers.GenerateGUID();
                appointment.Payment = CreatePayment(appointmentDto);
                if (appointmentDto.Status == "Completed")
                {
                    if (appointmentDto.TipAmount != 0)
                    {
                        appointment.Tips = CreateTip(appointmentDto);
                    }
                }
                _responseDto = _unitOfWork.Appointment.Insert(appointment);
                return _responseDto;
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Message = ex.Message;
                return _responseDto;
            }

        }
        public ResponseDto<Appointment> DeleteAppointmentByGuid(Guid guid)
        {
            var item = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == guid);
            if (item.StatusCode == HttpStatusCode.OK)
            {
                _responseDto = _unitOfWork.Appointment.Delete(item.Data);
            }
            else
            {
                _responseDto.StatusCode = item.StatusCode;
                _responseDto.Message = item.Message;
            }
            return _responseDto;
        }
        public ResponseDto<Appointment> DeleteAppointmentById(int id)
        {
            var item = _unitOfWork.Appointment.GetById(id);
            if(item.StatusCode == HttpStatusCode.OK)
            {
                _responseDto = _unitOfWork.Appointment.Delete(item.Data);
            }
            else
            {
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.Message = "Not Found";
            }
            
            return _responseDto;
        }
        public ResponseDto<Appointment> UpdateAppointment(AppointmentDto appointmentDto)
        {
            var item = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == appointmentDto.guid, includeProperties: "Payment");
            if(item.StatusCode == HttpStatusCode.OK)
            {
                item.Data.ClientName = appointmentDto.ClientName;
                item.Data.ClientPhoneNumber = appointmentDto.ClientPhoneNumber;
                item.Data.AppointmentDate = appointmentDto.AppointmentDate;
                item.Data.Category = appointmentDto.Category;
                item.Data.ReferalId = appointmentDto.ReferalId;
                item.Data.UserId = appointmentDto.UserId;
                item.Data.IsForeigner = appointmentDto.IsForeigner;
                item.Data.Status = appointmentDto.Status;
                item.Data.TattooDesign = appointmentDto.TattooDesign;
                item.Data.Placement = appointmentDto.Placement;
                item.Data.InkColorPreferance = appointmentDto.InkColorPreferance;
                item.Data.Allergies = appointmentDto.Allergies;
                item.Data.MedicalConditions = appointmentDto.MedicalConditions;
                item.Data.PainToleranceLevel = appointmentDto.PainToleranceLevel;
                item.Data.SessionNumber = appointmentDto.SessionNumber;
                item.Data.ConsentFormSigned = appointmentDto.ConsentFormSigned;
                item.Data.FollowUpRequired = appointmentDto.FollowUpRequired;
                item.Data.TotalHours = appointmentDto.TotalHours;
                item.Data.Payment = UpdatePayment(item.Data.Payment, appointmentDto);
                if (item.Data.Status == "Completed")
                {
                    if (appointmentDto.TipAmount != 0)
                    {
                        item.Data.Tips = CreateTip(appointmentDto);
                    }
                }
                _responseDto = _unitOfWork.Appointment.Update(item.Data);
            }
            else
            {
                _responseDto.StatusCode = item.StatusCode;
                _responseDto.Message = item.Message;
            }
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
        private List<Tip> CreateTip(AppointmentDto appointmentDto)
        {
            List<Tip> tipList = new List<Tip>();
            var tipUsers = _unitOfWork.Users.GetAll(p => p.DefaultTips == true).Datas;
            var tipArtistAssigned = _unitOfWork.Users.GetById(appointmentDto.UserId).Data;
            tipUsers.Add(tipArtistAssigned);
             
            int tipToDivideNumber=tipUsers.Count();
            var tipAmount = appointmentDto.TipAmount;
            var tipAmountForUsers = tipAmount / tipToDivideNumber;
            foreach (var tipuser in tipUsers)
            {
                Tip tip = new Tip();
                tip.TipAmount =(double)tipAmount;
                tip.AppointmentId = appointmentDto.UserId;
                tip.TipAmountForUsers =Math.Floor((double)tipAmountForUsers);
                tip.TipAssignedToUser = tipuser.Id;
                tipList.Add(tip);
            }  
            return tipList;
        }
        private Payment CreatePayment(AppointmentDto appointmentDto)
        {
            Payment payment = new Payment();
            payment = _mapper.Map<Payment>(appointmentDto);
            payment.PaymentToArtist = appointmentDto.TotalCost / 2;
            payment.PaymentToStudio= appointmentDto.TotalCost / 2;
            return payment;
        }
        private Payment UpdatePayment(Payment payment, AppointmentDto appointmentDto)
        {
            float artistPercentage = GetArtistPercentage(appointmentDto);
            float studioPercentage = 1 - artistPercentage;
            payment.Deposit = appointmentDto.Deposit;
            payment.Discount = appointmentDto.Discount;
            payment.DiscountInHour = appointmentDto.DiscountInHour;
            payment.TotalCost = appointmentDto.TotalCost;
            payment.PaymentMethod = appointmentDto.PaymentMethod;
            payment.PaymentToArtist =Math.Round(payment.TotalCost * artistPercentage);
            payment.PaymentToStudio = Math.Round(payment.TotalCost *studioPercentage);
            return payment;
        }

        private float GetArtistPercentage(AppointmentDto appointmentDto)
        {
           var item =_unitOfWork.Users.GetById(appointmentDto.UserId).Data;
            float artistPercentage = (float)item.Percentage / 100;
            return artistPercentage;
        }
    }
}

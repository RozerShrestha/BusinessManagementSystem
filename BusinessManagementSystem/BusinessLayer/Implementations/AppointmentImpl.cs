 using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using BusinessManagementSystem.Utility;
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
        public ResponseDto<AppointmentDto> GetAllAppointment(RequestDto requestDto)
        {
            try
            {
                requestDto.EndDate = requestDto.EndDate.AddDays(1);
                if (requestDto.Status == AppointmentStat.All.ToString())
                {
                    _responseDto = _unitOfWork.Appointment.GetAll(p => p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate,
                                        orderBy: p => p.AppointmentDate,
                                        orderByDescending: true,
                                        includeProperties: "User,Referal,Payment");
                }
                else
                {
                    _responseDto = _unitOfWork.Appointment.GetAll(p => p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate && p.Status == requestDto.Status,
                                        orderBy: p => p.AppointmentDate,
                                        orderByDescending: true,
                                        includeProperties: "User,Referal,Payment");
                }
                
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
        public ResponseDto<AppointmentDto> GetAllAppointmentByArtist(int userId, RequestDto requestDto)
        {
            try
            {
                requestDto.EndDate = requestDto.EndDate.AddDays(1);
                if (requestDto.Status == AppointmentStat.All.ToString())
                {
                    _responseDto = _unitOfWork.Appointment.GetAll(p => p.UserId == userId && p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate,
                    orderBy: p => p.AppointmentDate,
                    orderByDescending: true,
                    includeProperties: "User,Referal,Payment");
                }
                else
                {
                    _responseDto = _unitOfWork.Appointment.GetAll(p => p.UserId == userId && p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate && p.Status == requestDto.Status,
                    orderBy: p => p.AppointmentDate,
                    orderByDescending: true,
                    includeProperties: "User,Referal,Payment");

                }
                    
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
                    _responseAppointmentDto.Data.DbStatus = _responseDto.Data.Status;
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
                item.Data.ClientEmail = appointmentDto.ClientEmail;
                item.Data.AppointmentDate = appointmentDto.AppointmentDate;
                item.Data.Category = appointmentDto.Category;
                item.Data.ReferalId = appointmentDto.ReferalId;
                item.Data.UserId = appointmentDto.UserId;
                item.Data.IsForeigner = appointmentDto.IsForeigner;
                item.Data.Outlet=appointmentDto.Outlet;
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
                item.Data.Outlet = appointmentDto.Outlet;
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
        public string GetTotalCost(bool isForeigner, string category, double totalHours, int deposit, int discount, double discountInHour,out double totalCost)
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
        public RequestDto GetInitialRequestDtoFilter()
        {
            int lastDay = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            RequestDto requestDto = new RequestDto
            {
                Status = AppointmentStat.All.ToString(),
                StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, lastDay)
            };
            return requestDto;
        }
        private List<Tip> CreateTip(AppointmentDto appointmentDto)
        {
            List<Tip> tipList = new List<Tip>();
            var appointmentCreated = _unitOfWork.Users.GetById(appointmentDto.AppointmentCreatedId).Data;
            var tipUsers = _unitOfWork.Users.GetAll(p => p.DefaultTips == true).Datas;

            //to remove either of one tattoo admin
            tipUsers.RemoveAll(p => p.Id != appointmentCreated.Id && !p.PhoneNumber.Contains("9851217750"));

            //to add that artist who did the tattoo
            var tipArtistAssigned = _unitOfWork.Users.GetById(appointmentDto.UserId).Data;
            if (!tipUsers.Any(p => p.UserName == tipArtistAssigned.UserName))
            {
                tipUsers.Add(tipArtistAssigned);
            }
            //
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
            payment.TipAmount = appointmentDto.TipAmount;
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

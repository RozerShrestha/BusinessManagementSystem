 using AutoMapper;
using BusinessManagementSystem.BusinessLayer.Services;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Dto.Chart;
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
        public AppointmentImpl(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public ResponseDto<AppointmentDto> GetAllAppointment(RequestDto requestDto)
        {
            var responseAppointmentDto = new ResponseDto<AppointmentDto>();
            try
            {
                requestDto.EndDate = requestDto.EndDate.AddDays(1);
                ResponseDto<Appointment> responseDto;
                if (requestDto.Status == AppointmentStat.All.ToString())
                {
                    responseDto = _unitOfWork.Appointment.GetAll(p => p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate,
                                        orderBy: p => p.AppointmentDate,
                                        orderByDescending: true,
                                        includeProperties: "User,Referal,Payment");
                }
                else
                {
                    responseDto = _unitOfWork.Appointment.GetAll(p => p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate && p.Status == requestDto.Status,
                                        orderBy: p => p.AppointmentDate,
                                        orderByDescending: true,
                                        includeProperties: "User,Referal,Payment");
                }
                
                if (responseDto.Datas.Count > 0)
                {
                    foreach (var item in responseDto.Datas)
                    {
                        var appointmentDto = _mapper.Map<AppointmentDto>(item);
                        responseAppointmentDto.Datas.Add(appointmentDto);
                    }
                }
                else
                {
                    responseAppointmentDto.StatusCode=responseDto.StatusCode;
                    responseAppointmentDto.Message=responseDto.Message;
                }
            }
            catch (Exception ex)
            {
                responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                responseAppointmentDto.Message= ex.Message;
            }
             return responseAppointmentDto;
        }
        public ResponseDto<AppointmentDto> GetAllAppointmentByArtist(int userId, RequestDto requestDto)
        {
            var responseAppointmentDto = new ResponseDto<AppointmentDto>();
            try
            {
                requestDto.EndDate = requestDto.EndDate.AddDays(1);
                ResponseDto<Appointment> responseDto;
                if (requestDto.Status == AppointmentStat.All.ToString())
                {
                    responseDto = _unitOfWork.Appointment.GetAll(p => p.UserId == userId && p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate,
                    orderBy: p => p.AppointmentDate,
                    orderByDescending: true,
                    includeProperties: "User,Referal,Payment");
                }
                else
                {
                    responseDto = _unitOfWork.Appointment.GetAll(p => p.UserId == userId && p.AppointmentDate >= requestDto.StartDate && p.AppointmentDate <= requestDto.EndDate && p.Status == requestDto.Status,
                    orderBy: p => p.AppointmentDate,
                    orderByDescending: true,
                    includeProperties: "User,Referal,Payment");

                }
                    
                if (responseDto.Datas.Count > 0)
                {
                    foreach (var item in responseDto.Datas)
                    {
                        var appointmentDto = _mapper.Map<AppointmentDto>(item);
                        responseAppointmentDto.Datas.Add(appointmentDto);
                    }
                }
                else
                {
                    responseAppointmentDto.StatusCode = responseDto.StatusCode;
                    responseAppointmentDto.Message = responseDto.Message;
                }
            }
            catch (Exception ex)
            {
                responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                responseAppointmentDto.Message = ex.Message;
            }
            
            return responseAppointmentDto;
        }
        public ResponseDto<AppointmentDto> GetAppointmentByGuid(Guid guid)
        {
            var responseAppointmentDto = new ResponseDto<AppointmentDto>();
            try
            {
                var responseDto = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == guid, includeProperties: "Payment,User");
                if(responseDto.StatusCode == HttpStatusCode.OK)
                {
                    responseAppointmentDto.Data = _mapper.Map<AppointmentDto>(responseDto.Data);
                    responseAppointmentDto.Data.DbStatus = responseDto.Data.Status;
                }
                else
                {
                    responseAppointmentDto.StatusCode=responseDto.StatusCode;
                    responseAppointmentDto.Message=responseDto.Message;
                }
               
            }
            catch (Exception ex)
            {
                responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                responseAppointmentDto.Message = ex.Message;
            }
            return responseAppointmentDto;
        }
        public ResponseDto<AppointmentDto> GetAppointmentById(int id)
        {
            var responseAppointmentDto = new ResponseDto<AppointmentDto>();
            try
            {
                var responseDto = _unitOfWork.Appointment.GetById(id);
                if(responseDto.StatusCode == HttpStatusCode.OK)
                {
                    responseAppointmentDto.Data = _mapper.Map<AppointmentDto>(responseDto.Data);
                }
                else
                {
                    responseAppointmentDto.StatusCode=responseDto.StatusCode;
                    responseAppointmentDto.Message=responseDto.Message;
                }
                
            }
            catch (Exception ex)
            {
                responseAppointmentDto.StatusCode = HttpStatusCode.InternalServerError;
                responseAppointmentDto.Message = ex.Message;
            }
           
            return responseAppointmentDto;
        }
        public ResponseDto<Appointment> GetAppointmentByStatus(string status)
        {
            return _unitOfWork.Appointment.GetFirstOrDefault(p => p.Status == status);
        }       
        public ResponseDto<Appointment> CreateAppointment(AppointmentDto appointmentDto)
        {
            var responseDto = new ResponseDto<Appointment>();
            try
            {
                Appointment appointment = _mapper.Map<Appointment>(appointmentDto);
                appointment.guid = Helper.Helpers.GenerateGUID();
                appointment.Payment = CreatePayment(appointmentDto);
                if (appointmentDto.Status == "Completed")
                {
                    if (appointmentDto.TipAmount != 0)
                    {
                        appointment.Tips = CreateTip(appointmentDto);
                    }
                }
                responseDto = _unitOfWork.Appointment.Insert(appointment);
                return responseDto;
            }
            catch (Exception ex)
            {
                responseDto.StatusCode = HttpStatusCode.InternalServerError;
                responseDto.Message = ex.Message;
                return responseDto;
            }

        }
        public ResponseDto<Appointment> DeleteAppointmentByGuid(Guid guid)
        {
            var item = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == guid);
            if (item.StatusCode == HttpStatusCode.OK)
            {
                return _unitOfWork.Appointment.Delete(item.Data);
            }
            return new ResponseDto<Appointment>
            {
                StatusCode = item.StatusCode,
                Message = item.Message
            };
        }
        public ResponseDto<Appointment> DeleteAppointmentById(int id)
        {
            var item = _unitOfWork.Appointment.GetById(id);
            if(item.StatusCode == HttpStatusCode.OK)
            {
                return _unitOfWork.Appointment.Delete(item.Data);
            }
            return new ResponseDto<Appointment>
            {
                StatusCode = HttpStatusCode.NotFound,
                Message = "Not Found"
            };
        }
        public ResponseDto<Appointment> UpdateAppointment(AppointmentDto appointmentDto)
        {
            var item = _unitOfWork.Appointment.GetFirstOrDefault(p => p.guid == appointmentDto.guid, includeProperties: "Payment");
            if(item.StatusCode == HttpStatusCode.OK)
            {
                _mapper.Map(appointmentDto, item.Data);
                item.Data.Payment = UpdatePayment(item.Data.Payment, appointmentDto);

                if (item.Data.Status == "Completed")
                {
                    if (appointmentDto.TipAmount != 0)
                    {
                        item.Data.Tips = CreateTip(appointmentDto);
                    }
                }
                return _unitOfWork.Appointment.Update(item.Data);
            }
            return new ResponseDto<Appointment>
            {
                StatusCode = item.StatusCode,
                Message = item.Message
            };
        }
        public DueCostResponseDto GetDueCost(DueCostRequestDto request)
        {
            double categoryCost=0;
            if (request.Category == "Tattoo")
            {
               categoryCost = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.TattooPrice;
            }
            else if (request.Category == "Dreadlock")
            {
                categoryCost= _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.DreadLockPrice;
            }
            else if (request.Category == "Piercing")
            {
               var piercingData = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.PiercingPrice;
               
                var obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(piercingData);
                categoryCost = obj[request.Subcategory];
            }
            else if (request.Category == "EarPiercing")
            {
                var piercingData = _unitOfWork.BasicConfiguration.GetSingleOrDefault().Data.EarPiercingPrice;

                var obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(piercingData);
                categoryCost = obj[request.Subcategory];
            }

                categoryCost = request.IsForeigner ? categoryCost * 2 : categoryCost;

            double dueAmount = Convert.ToInt32(categoryCost) * (request.TotalHours - request.DiscountInHour) - request.Deposit - request.Discount - request.PaidAmount;
            double totalCost = Convert.ToInt32(request.Deposit + dueAmount + request.PaidAmount);

            string calculationDescription = $"Category: {request.Category}({categoryCost}) \n Deposit: {request.Deposit} \n Total Hours: {request.TotalHours}-{request.DiscountInHour} \n Discount in Price: {request.Discount} \n Due Amount: {dueAmount} \n Total Cost:{totalCost}";

            return new DueCostResponseDto
            {
                DueAmount = dueAmount,
                TotalCost = totalCost,
                CostDescription = calculationDescription
            };
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
            float artistPercentage = GetArtistPercentage(appointmentDto);
            float studioPercentage = 1 - artistPercentage;
            Payment payment = new Payment();
            payment = _mapper.Map<Payment>(appointmentDto);
            payment.PaymentToArtist = Math.Round(appointmentDto.TotalCost * artistPercentage);
            payment.PaymentToStudio= Math.Round(appointmentDto.TotalCost * studioPercentage);
            return payment;
        }
        private Payment UpdatePayment(Payment payment, AppointmentDto appointmentDto)
        {
            float artistPercentage = GetArtistPercentage(appointmentDto);
            float studioPercentage = 1 - artistPercentage;
            payment.Deposit = appointmentDto.Deposit;
            payment.Discount = appointmentDto.Discount;
            payment.DiscountInHour = appointmentDto.DiscountInHour;
            payment.DueAmount=appointmentDto.DueAmount;
            payment.PaidAmount=appointmentDto.PaidAmount;
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

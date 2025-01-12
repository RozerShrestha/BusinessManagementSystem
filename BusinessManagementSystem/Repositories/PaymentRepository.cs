using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Enums;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessManagementSystem.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPayment
    {
        ResponseDto<PaymentDto> _responsePaymentDto;
        ResponseDto<PaymentSettlementDto> _responsePaymentSettlementDto;
        ResponseDto<TipSettlementDto> _responseTipSettlementDto;
        ResponseDto<PaymentTipSettlementDto> _responsePaymentTipSettlementDto;
        ResponseDto<PaymentHistory> _responsePaymentHistory;

        public PaymentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responsePaymentDto = new ResponseDto<PaymentDto>();
            _responsePaymentSettlementDto = new ResponseDto<PaymentSettlementDto>();
            _responseTipSettlementDto = new ResponseDto<TipSettlementDto>();
            _responsePaymentTipSettlementDto = new ResponseDto<PaymentTipSettlementDto>();
            _responsePaymentHistory = new ResponseDto<PaymentHistory>();
        }
        public ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto)
        {
            var query = from p in _dbContext.Payments
                              join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                              join u in _dbContext.Users on p.UserId equals u.Id
                              where p.UpdatedAt>=requestDto.StartDate && p.UpdatedAt<=requestDto.EndDate
                              select new PaymentDto
                              {
                                  PaymentId= p.Id,
                                  AppointmentId=a.Id,
                                  AppointmentGuid=a.guid,
                                  UserId=u.Id,
                                  ArtistName=u.FullName,
                                  Deposit=p.Deposit,
                                  Discount=p.Discount,
                                  DiscountInHour=p.DiscountInHour,
                                  TotalCost=p.TotalCost,
                                  PaymentToStudio=p.PaymentToStudio,
                                  PaymentToArtist=p.PaymentToArtist,
                                  PaymentMethod=p.PaymentMethod,
                                  PaymentSettlement=p.PaymentSettlement,
                                  AppointmentStatus = a.Status,
                                  PaymentDate=a.UpdatedAt
                              };
            if(requestDto.Status!= AppointmentStat.All.ToString())  
            {
                query = (IQueryable<PaymentDto>)query.Where(k => k.AppointmentStatus == requestDto.Status);
            }
            query = query.OrderByDescending(x => x.PaymentDate);
            var paymentDto = query.ToList();

            if (paymentDto.Count > 0) _responsePaymentDto.Datas = paymentDto;
            else
            {
                _responsePaymentDto.StatusCode = HttpStatusCode.NotFound;
                _responsePaymentDto.Message = "Not Found";
            }
            _responsePaymentDto.Datas = paymentDto;
            return _responsePaymentDto;
        }
        public ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto)
        {
            var query = from p in _dbContext.Payments
                              join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                              join u in _dbContext.Users on p.UserId equals u.Id
                              where u.Id==userId && p.UpdatedAt >= requestDto.StartDate && p.UpdatedAt <= requestDto.EndDate
                              select new PaymentDto
                              {
                                  PaymentId = p.Id,
                                  AppointmentId = a.Id,
                                  AppointmentGuid=a.guid,
                                  UserId = u.Id,
                                  ArtistName = u.FullName,
                                  Deposit = p.Deposit,
                                  Discount = p.Discount,
                                  DiscountInHour = p.DiscountInHour,
                                  TotalCost = p.TotalCost,
                                  PaymentToStudio = p.PaymentToStudio,
                                  PaymentToArtist = p.PaymentToArtist,
                                  PaymentMethod = p.PaymentMethod,
                                  PaymentSettlement = p.PaymentSettlement,
                                  AppointmentStatus=a.Status,
                                  PaymentDate = (DateTime)a.UpdatedAt
                              };
            if (requestDto.Status != AppointmentStat.All.ToString())
            {
                query = (IQueryable<PaymentDto>)query.Where(k => k.AppointmentStatus == requestDto.Status);
            }
            query = query.OrderByDescending(x => x.PaymentDate);
            var paymentDto = query.ToList();
            if (paymentDto.Count > 0) _responsePaymentDto.Datas = paymentDto;
            else
            {
                _responsePaymentDto.StatusCode = HttpStatusCode.NotFound;
                _responsePaymentDto.Message = "Not Found";
            }
            _responsePaymentDto.Datas = paymentDto;
            return _responsePaymentDto;
        }
        public ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto)
        {
            var queryPayments =  from p in _dbContext.Payments
                              join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                              join u in _dbContext.Users on p.UserId equals u.Id
                              where u.Guid==guid && p.UpdatedAt >= requestDto.StartDate && p.UpdatedAt <= requestDto.EndDate
                              select new PaymentDto
                              {
                                  PaymentId = p.Id,
                                  AppointmentId = a.Id,
                                  UserId = u.Id,
                                  ArtistName = u.FullName,
                                  Deposit = p.Deposit,
                                  Discount = p.Discount,
                                  DiscountInHour = p.DiscountInHour,
                                  TotalCost = p.TotalCost,
                                  PaymentToStudio = p.PaymentToStudio,
                                  PaymentToArtist = p.PaymentToArtist,
                                  PaymentMethod = p.PaymentMethod,
                                  PaymentSettlement = p.PaymentSettlement,
                                  AppointmentStatus = a.Status,
                                  PaymentDate = (DateTime)a.UpdatedAt
                              };

            if (requestDto.Status == AppointmentStat.All.ToString())
            {
                queryPayments = (IQueryable<PaymentDto>)queryPayments.Where(k => k.AppointmentStatus == requestDto.Status).OrderByDescending(x => x.PaymentDate);
            }
            var paymentDto = queryPayments.ToList();
            if (paymentDto.Count > 0) _responsePaymentDto.Datas = paymentDto;
            else
            {
                _responsePaymentDto.StatusCode = HttpStatusCode.NotFound;
                _responsePaymentDto.Message = "Not Found";
            }
            _responsePaymentDto.Datas = paymentDto;
            return _responsePaymentDto;
        }
        public ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto)
        {
            var payS = GetPaymentSettlement(requestDto);
            var tipS = GetTipSettlement(requestDto);
            PaymentTipSettlementDto paymentTipSettlementDto = new PaymentTipSettlementDto
            {
                UserId=requestDto.UserId,
                PaymentSettlements = payS.Datas,
                TipSettlements = tipS.Datas,
                StartDate=requestDto.StartDate,
                EndDate=requestDto.EndDate,
                TotalPayments = payS.Datas.Sum(p => p.PaymentToArtist),
                TotalTips=tipS.Datas.Sum(p=>p.TipAmountForUser),
                GrandTotal= payS.Datas.Sum(p => p.PaymentToArtist) + tipS.Datas.Sum(p => p.TipAmountForUser)
            };
            
            if (payS.StatusCode != HttpStatusCode.OK)
            { 
                _responsePaymentTipSettlementDto.StatusCode = payS.StatusCode;
                _responsePaymentTipSettlementDto.Message = payS.Message;
            }
            if (tipS.StatusCode != HttpStatusCode.OK)
            {
                _responsePaymentTipSettlementDto.StatusCode = tipS.StatusCode;
                _responsePaymentTipSettlementDto.Message += tipS.Message;
            }
            _responsePaymentTipSettlementDto.Data = paymentTipSettlementDto;
            return _responsePaymentTipSettlementDto;
        }
        public ResponseDto<PaymentHistory> CreatePaymentHistory(PaymentHistory paymentHistory)
        {
            try
            {
                _dbContext.Add(paymentHistory);
                _dbContext.SaveChanges();
                _responsePaymentHistory.Data = paymentHistory;
            }
            catch (Exception ex)
            {
                _responsePaymentHistory.Message = ex.Message;
                _responsePaymentHistory.Data = paymentHistory;
                _responsePaymentHistory.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _responsePaymentHistory;
           
        }
        private ResponseDto<PaymentSettlementDto> GetPaymentSettlement(RequestDto requestDto)
        {
            try
            {
                var queryPayments = from u in _dbContext.Users
                            join a in _dbContext.Appointments on u.Id equals a.UserId
                            join p in _dbContext.Payments on a.Id equals p.AppointmentId

                            select new
                            {
                                User = u,
                                Appointment = a,
                                Payment = p
                            };

                // Apply conditions dynamically
                if (requestDto.StartDate != null)
                    queryPayments = queryPayments.Where(x => x.Payment.UpdatedAt >= requestDto.StartDate);
                if (requestDto.EndDate != null)
                    queryPayments = queryPayments.Where(x => x.Payment.UpdatedAt <= requestDto.EndDate);
                if (requestDto.UserId > 0)
                    queryPayments = queryPayments.Where(x => x.User.Id == requestDto.UserId);
                if (requestDto.Status!="All")
                    queryPayments = queryPayments.Where(x => x.Appointment.Status == requestDto.Status);
                if (requestDto.Settlement!= "ALL")
                    queryPayments = queryPayments.Where(x => x.Payment.PaymentSettlement == bool.Parse(requestDto.Settlement));

                // Project the result
                var paymentItems = queryPayments
                    .Select(x => new PaymentSettlementDto
                    {
                        UserId = x.User.Id,
                        AppointmentId = x.Appointment.Id,
                        PaymentId=x.Payment.Id,
                        ArtistName = x.User.FullName,
                        AppointmentDate = x.Appointment.AppointmentDate,
                        PaymentUpdatedDate=x.Payment.UpdatedAt,
                        ClientName = x.Appointment.ClientName,
                        TotalCost = x.Payment.TotalCost,
                        PaymentToStudio = x.Payment.PaymentToStudio,
                        PaymentToArtist = x.Payment.PaymentToArtist,
                        PaymentMethod = x.Payment.PaymentMethod,
                        Status = x.Appointment.Status,
                        PaymentSettlement = x.Payment.PaymentSettlement
                    })
                    .OrderByDescending(x => x.AppointmentDate)
                    .ToList();
                
                    _responsePaymentSettlementDto.Datas = paymentItems;
            }
            catch (Exception ex)
            {
                _responsePaymentSettlementDto.StatusCode = HttpStatusCode.NotFound;
                _responsePaymentSettlementDto.Message = ex.Message;
            }
            return _responsePaymentSettlementDto;
        }
        private ResponseDto<TipSettlementDto> GetTipSettlement(RequestDto requestDto)
        {
            try
            {
                var queryTips = from u in _dbContext.Users
                                join a in _dbContext.Appointments on u.Id equals a.UserId
                                join t in _dbContext.Tips on a.Id equals t.AppointmentId
                                select new
                                {
                                    User = u,
                                    Appointment = a,
                                    Tip = t
                                };

                // Apply conditions dynamically
                if (requestDto.StartDate != null)
                    queryTips = queryTips.Where(x => x.Tip.CreatedAt >= requestDto.StartDate);
                if (requestDto.EndDate != null)
                    queryTips = queryTips.Where(x => x.Tip.CreatedAt <= requestDto.EndDate);
                if (requestDto.UserId > 0)
                    queryTips = queryTips.Where(x => x.Tip.TipAssignedToUser == requestDto.UserId);
                if (requestDto.Status!="All")
                    queryTips = queryTips.Where(x => x.Appointment.Status == requestDto.Status);
                if (requestDto.Settlement!= "ALL")
                    queryTips = queryTips.Where(x => x.Tip.TipSettlement == bool.Parse(requestDto.Settlement));

                // Project the result
                var tipItems = queryTips
                    .Select(x => new TipSettlementDto
                    {
                        UserId = x.User.Id,
                        AppointmentId = x.Appointment.Id,
                        TipId=x.Tip.Id,
                        AppointmentDate = x.Appointment.AppointmentDate,
                        TipCreatedDate=x.Tip.CreatedAt,
                        ClientName = x.Appointment.ClientName,
                        ArtistName = x.User.FullName,
                        TipAmount=x.Tip.TipAmount,
                        TipAmountForUser=x.Tip.TipAmountForUsers,
                        TipSettlement=x.Tip.TipSettlement,
                        Status = x.Appointment.Status,
                    })
                    .OrderByDescending(x => x.AppointmentDate)
                    .ToList();
                    _responseTipSettlementDto.Datas = tipItems;
            }
            catch (Exception ex)
            {
                _responseTipSettlementDto.StatusCode = HttpStatusCode.NotFound;
                _responseTipSettlementDto.Message = ex.Message;
            }
            return _responseTipSettlementDto;
        }

        public ResponseDto<PaymentHistory> GetPaymentHistory(RequestDto requestDto)
        {
            try
            {
                var paymentHistory = (from p in _dbContext.PaymentHistories
                                      join u in _dbContext.Users
                                      on p.UserId equals u.Id
                                      select new PaymentHistory
                                      {
                                          Id = p.Id,
                                          UserId=p.UserId,
                                          ArtistName=u.FullName,
                                          TotalPayment=p.TotalPayment,
                                          TotalTips=p.TotalTips,
                                          GrandTotal=p.GrandTotal,
                                          PaidStatus=p.PaidStatus,
                                          PaymentFrom=p.PaymentFrom,
                                          PaymentTo=p.PaymentTo
                                      }).OrderByDescending(x=>x.PaymentFrom).AsQueryable();
                if (requestDto.StartDate != null)
                    paymentHistory = paymentHistory.Where(x => x.PaymentFrom >=DateOnly.FromDateTime(requestDto.StartDate));
                if (requestDto.EndDate != null)
                    paymentHistory = paymentHistory.Where(x => x.PaymentFrom <= DateOnly.FromDateTime(requestDto.EndDate));
                if (requestDto.UserId > 0)
                    paymentHistory = paymentHistory.Where(x => x.UserId == requestDto.UserId);

                _responsePaymentHistory.Datas = paymentHistory.ToList();
            }
            catch (Exception ex)
            {
                _responsePaymentHistory.Message = ex.Message;
                _responsePaymentHistory.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _responsePaymentHistory;
        }
    }
}

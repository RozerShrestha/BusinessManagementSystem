using TattooAppointmentSystem.Data;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Enums;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;
using TattooAppointmentSystem.Utility;
using System.Net;

namespace TattooAppointmentSystem.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPayment
    {
        private readonly TipRepository _tipRepository;
        private readonly AdvancePaymentRepository _advancePaymentRepository;

        public PaymentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _tipRepository = new TipRepository(dbContext);
            _advancePaymentRepository = new AdvancePaymentRepository(dbContext);
        }

        public ResponseDto<PaymentDto> GetAllPayments(RequestDto requestDto)
        {
            return GetPaymentDtos(requestDto, userFilter: null);
        }

        public ResponseDto<PaymentDto> GetMyPayments(int userId, RequestDto requestDto)
        {
            return GetPaymentDtos(requestDto, userFilter: q => q.Where(k => k.UserId == userId));
        }

        public ResponseDto<PaymentDto> GetMyPayments(Guid guid, RequestDto requestDto)
        {
            return GetPaymentDtos(requestDto, userFilter: q =>
                from dto in q
                join u in _dbContext.Users on dto.UserId equals u.Id
                where u.Guid == guid
                select dto);
        }

        public ResponseDto<PaymentTipSettlementDto> GetPaymentTipSettlement(RequestDto requestDto)
        {
            var responseDto = new ResponseDto<PaymentTipSettlementDto>();
            var payS = GetPaymentSettlement(requestDto);
            var tipS = _tipRepository.GetTipSettlement(requestDto);
            var advanceS = _advancePaymentRepository.GetAdvancePaymentSettlement(requestDto);

            var paymentTipSettlementDto = new PaymentTipSettlementDto
            {
                UserId = requestDto.UserId,
                PaymentSettlements = payS.Datas,
                TipSettlements = tipS.Datas,
                AdvancePaymentSettlements = advanceS.Datas,
                StartDate = requestDto.StartDate,
                EndDate = requestDto.EndDate,
                TotalPayments = payS.Datas.Sum(p => p.PaymentToArtist),
                TotalTips = tipS.Datas.Sum(p => p.TipAmountForUser),
                TotalAdvancePayments = advanceS.Datas.Sum(p => p.Amount),
                GrandTotal = payS.Datas.Sum(p => p.PaymentToArtist) + tipS.Datas.Sum(p => p.TipAmountForUser) - advanceS.Datas.Sum(p => p.Amount)
            };

            if (payS.StatusCode != HttpStatusCode.OK)
            {
                responseDto.StatusCode = payS.StatusCode;
                responseDto.Message = payS.Message;
            }
            if (tipS.StatusCode != HttpStatusCode.OK)
            {
                responseDto.StatusCode = tipS.StatusCode;
                responseDto.Message += tipS.Message;
            }
            responseDto.Data = paymentTipSettlementDto;
            return responseDto;
        }

        public ResponseDto<PaymentHistory> CreatePaymentHistory(PaymentHistory paymentHistory)
        {
            var responseDto = new ResponseDto<PaymentHistory>();
            try
            {
                _dbContext.Add(paymentHistory);
                _dbContext.SaveChanges();
                responseDto.Data = paymentHistory;
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.Data = paymentHistory;
                responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }
            return responseDto;
        }

        public ResponseDto<PaymentHistory> GetPaymentHistory(RequestDto requestDto)
        {
            var responseDto = new ResponseDto<PaymentHistory>();
            try
            {
                (DateTime firstDay, DateTime lastDay) = Helper.Helpers.GetYearFirstAndLastDate(DateTime.Today);
                var query = (from p in _dbContext.PaymentHistories
                             join u in _dbContext.Users on p.UserId equals u.Id
                             where p.CreatedAt >= firstDay && p.CreatedAt <= lastDay
                             select new PaymentHistory
                             {
                                 Id = p.Id,
                                 UserId = p.UserId,
                                 ArtistName = u.FullName,
                                 TotalPayment = p.TotalPayment,
                                 TotalTips = p.TotalTips,
                                 TotalAdvancePayment = p.TotalAdvancePayment,
                                 GrandTotal = p.GrandTotal,
                                 PaidStatus = p.PaidStatus,
                                 PaymentFrom = p.PaymentFrom,
                                 PaymentTo = p.PaymentTo,
                                 CreatedBy = p.CreatedBy,
                                 CreatedAt = p.CreatedAt
                             }).OrderByDescending(x => x.PaymentFrom).AsQueryable();

                if (requestDto.StartDate != DateTime.MinValue)
                    query = query.Where(x => x.PaymentFrom >= DateOnly.FromDateTime(requestDto.StartDate));
                if (requestDto.EndDate != DateTime.MinValue)
                    query = query.Where(x => x.PaymentFrom <= DateOnly.FromDateTime(requestDto.EndDate).AddDays(1));
                if (requestDto.UserId > 0)
                    query = query.Where(x => x.UserId == requestDto.UserId);

                responseDto.Datas = query.ToList();
            }
            catch (Exception ex)
            {
                responseDto.Message = ex.Message;
                responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }
            return responseDto;
        }

        public dynamic GetAllPaymentsDashboard(RequestDto requestDto)
        {
            return (from p in _dbContext.Payments
                    join u in _dbContext.Users on p.UserId equals u.Id
                    where p.PaymentSettlement == true
                          && p.CreatedAt >= requestDto.StartDate
                          && p.CreatedAt <= requestDto.EndDate.AddDays(1)
                    group p by new { u.FullName } into g
                    select new
                    {
                        FullName = g.Key.FullName,
                        TotalPaymentToArtist = g.Sum(p => p.PaymentToArtist)
                    }).ToList();
        }

        public dynamic GetAllPaymentSegregation(RequestDto requestDto)
        {
            return _dbContext.Payments
                .Where(p => p.PaymentSettlement == true
                            && p.UpdatedAt >= requestDto.StartDate
                            && p.UpdatedAt <= requestDto.EndDate.AddDays(1))
                .GroupBy(p => 1)
                .Select(g => new
                {
                    TotalPaymentToStudio = g.Sum(p => p.PaymentToStudio),
                    TotalPaymentToArtist = g.Sum(p => p.PaymentToArtist)
                })
                .FirstOrDefault();
        }

        #region Private Helpers

        private ResponseDto<PaymentDto> GetPaymentDtos(RequestDto requestDto, Func<IQueryable<PaymentDto>, IQueryable<PaymentDto>>? userFilter)
        {
            var responseDto = new ResponseDto<PaymentDto>();
            var query = from p in _dbContext.Payments
                        join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                        join u in _dbContext.Users on p.UserId equals u.Id
                        where a.CreatedAt >= requestDto.StartDate && a.CreatedAt <= requestDto.EndDate.AddDays(1)
                        select new PaymentDto
                        {
                            PaymentId = p.Id,
                            AppointmentId = a.Id,
                            AppointmentGuid = a.guid,
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
                            PaymentDate = a.CreatedAt,
                            PaymentDateNP = NepaliDateService.EngToNep((DateTime)a.CreatedAt).ToString()
                        };

            if (userFilter != null)
                query = userFilter(query);

            if (requestDto.Status != AppointmentStat.All.ToString())
                query = query.Where(k => k.AppointmentStatus == requestDto.Status);

            query = query.OrderByDescending(x => x.PaymentDate);

            var results = query.ToList();
            if (results.Count == 0)
            {
                responseDto.StatusCode = HttpStatusCode.NotFound;
                responseDto.Message = "Not Found";
            }
            responseDto.Datas = results;
            return responseDto;
        }

        private ResponseDto<PaymentSettlementDto> GetPaymentSettlement(RequestDto requestDto)
        {
            var responseDto = new ResponseDto<PaymentSettlementDto>();
            try
            {
                var query = from u in _dbContext.Users
                            join a in _dbContext.Appointments on u.Id equals a.UserId
                            join p in _dbContext.Payments on a.Id equals p.AppointmentId
                            select new { User = u, Appointment = a, Payment = p };

                if (requestDto.StartDate != null)
                    query = query.Where(x => x.Payment.UpdatedAt >= requestDto.StartDate);
                if (requestDto.EndDate != null)
                    query = query.Where(x => x.Payment.UpdatedAt <= requestDto.EndDate);
                if (requestDto.UserId > 0)
                    query = query.Where(x => x.User.Id == requestDto.UserId);
                if (requestDto.Status != "All")
                    query = query.Where(x => x.Appointment.Status == requestDto.Status);
                if (requestDto.Settlement != "ALL")
                    query = query.Where(x => x.Payment.PaymentSettlement == bool.Parse(requestDto.Settlement));

                responseDto.Datas = query
                    .Select(x => new PaymentSettlementDto
                    {
                        UserId = x.User.Id,
                        AppointmentId = x.Appointment.Id,
                        PaymentId = x.Payment.Id,
                        ArtistName = x.User.FullName,
                        AppointmentDate = x.Appointment.AppointmentDate,
                        PaymentUpdatedDate = x.Payment.UpdatedAt,
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
            }
            catch (Exception ex)
            {
                responseDto.StatusCode = HttpStatusCode.NotFound;
                responseDto.Message = ex.Message;
            }
            return responseDto;
        }

        #endregion
    }
}


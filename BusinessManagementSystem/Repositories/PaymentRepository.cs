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
        public ResponseDto<PaymentDto> _responsePaymentDto;
        public PaymentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responsePaymentDto = new ResponseDto<PaymentDto>();
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
                                  PaymentDate=(DateTime)a.UpdatedAt
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
            var query =  from p in _dbContext.Payments
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
                query = (IQueryable<PaymentDto>)query.Where(k => k.AppointmentStatus == requestDto.Status).OrderByDescending(x => x.PaymentDate);
            }
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
    }
}

using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using System.Net;

namespace BusinessManagementSystem.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPayment
    {
        public ResponseDto<PaymentDto> _responsePaymentDto;
        public PaymentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responsePaymentDto = new ResponseDto<PaymentDto>();
        }
        public ResponseDto<PaymentDto> GetAllPayments()
        {
            var paymentDto = (from p in _dbContext.Payments
                              join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                              join u in _dbContext.Users on p.UserId equals u.Id
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
                                  AppointmentStatus = a.Status
                              }).ToList();
            if (paymentDto.Count > 0) _responsePaymentDto.Datas = paymentDto;
            else
            {
                _responsePaymentDto.StatusCode = HttpStatusCode.NotFound;
                _responsePaymentDto.Message = "Not Found";
            }
            _responsePaymentDto.Datas = paymentDto;
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentDto> GetMyPayments(int userId)
        {
            var paymentDto = (from p in _dbContext.Payments
                              join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                              join u in _dbContext.Users on p.UserId equals u.Id
                              where u.Id==userId
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
                                  AppointmentStatus=a.Status
                              }).ToList();
            if (paymentDto.Count > 0) _responsePaymentDto.Datas = paymentDto;
            else
            {
                _responsePaymentDto.StatusCode = HttpStatusCode.NotFound;
                _responsePaymentDto.Message = "Not Found";
            }
            _responsePaymentDto.Datas = paymentDto;
            return _responsePaymentDto;
        }

        public ResponseDto<PaymentDto> GetMyPayments(Guid guid)
        {
            var paymentDto = (from p in _dbContext.Payments
                              join a in _dbContext.Appointments on p.AppointmentId equals a.Id
                              join u in _dbContext.Users on p.UserId equals u.Id
                              where u.Guid==guid
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
                                  AppointmentStatus = a.Status
                              }).ToList();
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

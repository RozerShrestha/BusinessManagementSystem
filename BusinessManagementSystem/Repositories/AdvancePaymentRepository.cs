using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BusinessManagementSystem.Repositories
{
    public class AdvancePaymentRepository: GenericRepository<AdvancePayment>, IAdvancePayment
    {
        public ResponseDto<AdvancePaymentDto> _responseDto;
        public AdvancePaymentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseDto=new ResponseDto<AdvancePaymentDto>();
        }

        public ResponseDto<AdvancePaymentDto> GetAdvancePaymentSettlement(RequestDto requestDto)
        {
            try
            {
                var queryAdvancePayment = from a in _dbContext.AdvancePayments
                                          join u in _dbContext.Users on a.UserId equals u.Id
                                          where a.Status==true
                                          select new
                                          {
                                              Advancepayment = a,
                                              User=u
                                          };

                bool stat = requestDto.Status == "Completed" ? true : false;
                // Apply conditions dynamically
                if (requestDto.StartDate != null)
                    queryAdvancePayment = queryAdvancePayment.Where(x => x.Advancepayment.PaidDate >= DateOnly.FromDateTime(requestDto.StartDate));
                if (requestDto.EndDate != null)
                    queryAdvancePayment = queryAdvancePayment.Where(x => x.Advancepayment.PaidDate <= DateOnly.FromDateTime(requestDto.EndDate));
                if (requestDto.UserId > 0)
                    queryAdvancePayment = queryAdvancePayment.Where(x => x.Advancepayment.UserId == requestDto.UserId);

                // Project the result
                var advancePaymentItem = queryAdvancePayment
                    .Select(x => new AdvancePaymentDto
                    {
                        Id=x.Advancepayment.Id,
                        FullName=x.User.FullName,
                        Amount=x.Advancepayment.Amount,
                        PaymentMethod=x.Advancepayment.PaymentMethod,
                        Reason=x.Advancepayment.Reason,
                        PaidDate=x.Advancepayment.PaidDate,
                        Status=x.Advancepayment.Status
                    })
                    .OrderByDescending(x => x.PaidDate)
                    .ToList();

                _responseDto.Datas = advancePaymentItem;
               
            }
            catch (Exception ex)
            {
                _responseDto.StatusCode = HttpStatusCode.NotFound;
                _responseDto.Message = ex.Message;
            }
            return _responseDto;
        }
    }
}

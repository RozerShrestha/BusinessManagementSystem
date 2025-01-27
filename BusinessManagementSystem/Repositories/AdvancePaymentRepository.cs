using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class AdvancePaymentRepository: GenericRepository<AdvancePayment>, IAdvancePayment
    {
        public ResponseDto<AdvancePayment> _responseDto;
        public AdvancePaymentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseDto=new ResponseDto<AdvancePayment>();
        }
    }
}

using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class ReferalRepository : GenericRepository<Referal>, IReferal
    {
        public ResponseDto<Referal> _responseDto;

        public ReferalRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseDto = new ResponseDto<Referal>();
        }


    }
}

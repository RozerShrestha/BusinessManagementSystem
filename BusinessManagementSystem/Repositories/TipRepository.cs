using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class TipRepository : GenericRepository<Tip>, ITip
    {
        public ResponseDto<Tip> _responseDto;

        public TipRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseDto = new ResponseDto<Tip>();
        }
    }
}

using TattooAppointmentSystem.Data;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Enums;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;

namespace TattooAppointmentSystem.Repositories
{
    public class ReferalRepository : GenericRepository<Referal>, IReferal
    {
        public ResponseDto<Referal> _responseDto;

        public ReferalRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            _responseDto = new ResponseDto<Referal>();
        }
        public dynamic ReferalList()
        {
            var referalList = _dbContext.Referals.Where(p=>p.Status == true).Select(p => new { Id = p.Id, Name = p.FullName }).ToList();
            return referalList;
        }

    }
}


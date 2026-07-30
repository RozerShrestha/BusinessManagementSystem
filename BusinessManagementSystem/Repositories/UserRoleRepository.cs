using TattooAppointmentSystem.Data;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Models;
using TattooAppointmentSystem.Services;

namespace TattooAppointmentSystem.Repositories
{
    public class UserRoleRepository:GenericRepository<UserRole>, IUserRole
    {
        private readonly ApplicationDBContext _db;
        public ResponseDto<UserRole> _responseDto;
        public UserRoleRepository(ApplicationDBContext db) : base(db) 
        {
            _responseDto = new ResponseDto<UserRole>();
            _db = db;
        }


    }
}


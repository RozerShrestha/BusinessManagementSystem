using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class MenuRoleRepository:GenericRepository<MenuRole>, IMenuRole
    {
        private readonly ApplicationDBContext _db;
        public ResponseDto<MenuRole> _responseDto;
        public MenuRoleRepository(ApplicationDBContext db) : base(db)
        {
            _responseDto = new ResponseDto<MenuRole>();
            _db = db;
        }
    }
}

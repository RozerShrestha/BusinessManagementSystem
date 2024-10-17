using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        public ResponseDto<User> _responseDto;
        public ResponseDto<UserRoleDto> _responseDto1;
        public UserRepository(ApplicationDBContext dbContext) : base(dbContext) 
        {
            _responseDto = new ResponseDto<User>();
            _responseDto1 = new ResponseDto<UserRoleDto>();
        }

        public List<User> GetAllActiveUsers()
        {
            List<User> activeUsers = _dbContext.Users.Where(p => p.Status == true).ToList();
            return activeUsers;
        }

        public List<User> GetAllInactiveUsers()
        {
            List<User> activeUsers = _dbContext.Users.Where(p => p.Status == false).ToList();
            return activeUsers;
        }

        public ResponseDto<UserRoleDto> GetAllUser(string filter)
        {

           _responseDto1.Datas = (from u in _dbContext.Users
                                join ur in _dbContext.UserRoles on u.Id equals ur.UserId
                                join r in _dbContext.Roles on ur.RoleId equals r.Id
                                where r.Name == filter
                                select new UserRoleDto
                                {
                                    User = u,
                                    RoleName = r.Name
                                }).ToList();
            
            return _responseDto1;
        }
    }
}

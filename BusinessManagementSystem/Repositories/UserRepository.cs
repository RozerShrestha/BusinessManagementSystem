using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Repositories
{
    public class UserRepository : GenericRepository<User>, IUser
    {
        public UserRepository(ApplicationDBContext dbContext) : base(dbContext) { }

        public async Task<List<User>> GetAllActiveUsers()
        {
            List<User> activeUsers = await _dbContext.Users.Where(p => p.Status == true).ToListAsync();
            return activeUsers;
        }

        public async Task<List<User>> GetAllInactiveUsers()
        {
            List<User> activeUsers = await _dbContext.Users.Where(p => p.Status == false).ToListAsync();
            return activeUsers;
        }
    }
}

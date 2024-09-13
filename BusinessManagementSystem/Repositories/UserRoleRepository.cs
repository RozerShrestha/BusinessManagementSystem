using BusinessManagementSystem.Data;
using BusinessManagementSystem.Models;
using BusinessManagementSystem.Services;

namespace BusinessManagementSystem.Repositories
{
    public class UserRoleRepository:GenericRepository<UserRole>, IUserRole
    {
        public UserRoleRepository(ApplicationDBContext dbContext) : base(dbContext) { }
    }
}

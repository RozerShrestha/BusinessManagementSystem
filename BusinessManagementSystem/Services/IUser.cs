using BusinessManagementSystem.Models;

namespace BusinessManagementSystem.Services
{
    public interface IUser:IGeneric<User>
    {
        Task<List<User>> GetAllActiveUsers();
        Task<List<User>> GetAllInactiveUsers();
    }
}

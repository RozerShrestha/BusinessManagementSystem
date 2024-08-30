using BusinessManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Services
{
    public interface IUnitOfWork
    {
        IUser Users { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();
        Task Save();
    }
}

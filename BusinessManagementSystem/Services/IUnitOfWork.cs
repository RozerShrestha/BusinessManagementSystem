using BusinessManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Services
{
    public interface IUnitOfWork
    {
        IUser Users { get; }
        IBase Base { get; }
        IDashboard Dashboard { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();

        Task Save();
    }
}

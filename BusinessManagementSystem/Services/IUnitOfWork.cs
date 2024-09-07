using BusinessManagementSystem.Models;
using BusinessManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Services
{
    public interface IUnitOfWork
    {
        IUser Users { get; }
        IRole Role { get; }
        IMenu Menu { get; }
        IBasicConfiguration BasicConfiguration { get; }
        
        IDashboard Dashboard { get; }
        IBase Base { get; }
        void CreateTransaction();
        void Commit();
        void Rollback();

        Task Save();
    }
}

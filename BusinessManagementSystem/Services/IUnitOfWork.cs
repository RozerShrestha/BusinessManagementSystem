using BusinessManagementSystem.Models;
using BusinessManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementSystem.Services
{
    public interface IUnitOfWork
    {
        IBase Base { get; }
        IUser Users { get; }
        IRole Role { get; }
        IMenu Menu { get; }
        IMenuRole MenuRole { get; }
        IUserRole UserRole { get; }
        IBasicConfiguration BasicConfiguration { get; }      
        IDashboard Dashboard { get; }
        IReferal Referal { get; }
        IAppointment Appointment { get; }
        ITip Tip { get; }
        IPayment Payment { get; }
        void BeginTransaction();
        void Commit();
        void Rollback();

        void SaveChanges();
    }
}

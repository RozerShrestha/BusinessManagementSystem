using BusinessManagementSystem.Services;
using BusinessManagementSystem.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using BusinessManagementSystem.Dto;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;
//using System.Data.Entity;

namespace BusinessManagementSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly bool _disposed;
        private string _errorMessage = string.Empty;
        private readonly ApplicationDBContext _dbContext = null;
        private IDbContextTransaction _objTran = null;

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            Users = new UserRepository(_dbContext);
            Base = new BaseRepository(_dbContext);
            //Dashboard = new DashboardRepository(_db);
            Role =new RoleRepository(_dbContext);
            //Menu = new MenuRoleRepository(_dbContext);
            MenuRole=new MenuRoleRepository(_dbContext);
            BasicConfiguration = new BasicConfigurationRepository(_dbContext);
        }

        public IUser Users { get; private set; }

        public IBase Base { get; private set; }
        public IDashboard Dashboard { get; private set; }
        public IRole Role { get; private set; }
        public IMenu Menu { get; private set; }
        public IMenuRole MenuRole { get; private set; }
        public IBasicConfiguration BasicConfiguration { get; private set; }
        
        public async void BeginTransactionAsync()
        {
            //It will Begin the transaction on the underlying connection
            _objTran =await _dbContext.Database.BeginTransactionAsync();
        }
        public async void CommitAsync()
        {
            //Commits the underlying store transaction
            await _objTran.CommitAsync();
            await _objTran.DisposeAsync();
        }
        public async void RollbackAsync()
        {
            //Rolls back the underlying store transaction
            await _objTran.RollbackAsync();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            await _objTran.DisposeAsync();
        }
        public async Task SaveChangesAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message, ex);
                //foreach (var validationErrors in ex.EntityValidationErrors)
                //{
                //    foreach (var validationError in validationErrors.ValidationErrors)
                //    {
                //        _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                //    }
                //}
                //throw new Exception(_errorMessage, ex);

            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected virtual void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}

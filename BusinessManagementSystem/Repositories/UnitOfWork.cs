using BusinessManagementSystem.Services;
using BusinessManagementSystem.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Validation;
using BusinessManagementSystem.Dto;
//using System.Data.Entity;

namespace BusinessManagementSystem.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly bool _disposed;
        private string _errorMessage = string.Empty;
        private readonly ApplicationDBContext _dbContext = null;
        private IDbContextTransaction _objTran = null;

        public IUser Users { get; private set; }

        public IBase Base { get; private set; }
        public IDashboard Dashboard { get; private set; }

        public IRole Role { get; private set; }

        public IMenu Menu { get; private set; }

        public IBasicConfiguration BasicConfiguration => throw new NotImplementedException();

        public UnitOfWork(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
            Users=new UserRepository(_dbContext);
            Base=new BaseRepository(_dbContext);
        }

        public void CreateTransaction()
        {
            //It will Begin the transaction on the underlying connection
            _objTran = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            //Commits the underlying store transaction
            _objTran.Commit();
        }

        public void Rollback()
        {
            //Rolls back the underlying store transaction
            _objTran.Rollback();
            //The Dispose Method will clean up this transaction object and ensures Entity Framework
            //is no longer using that transaction.
            _objTran.Dispose();
        }

        public async Task Save()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbEntityValidationException ex)
            {

                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        _errorMessage = _errorMessage + $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage} {Environment.NewLine}";
                    }
                }
                throw new Exception(_errorMessage, ex);
            }
        }

        //public async Task Save()
        //{
        //    try
        //    {
        //        //Calling DbContext Class SaveChanges method 
        //        await Context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        // Handle the exception, possibly logging the details
        //        // The InnerException often contains more specific details
        //        throw new Exception(ex.Message, ex);
        //    }
        //}

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

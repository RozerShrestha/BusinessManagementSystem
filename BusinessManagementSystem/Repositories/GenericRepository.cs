using TattooAppointmentSystem.Data;
using TattooAppointmentSystem.Dto;
using TattooAppointmentSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TattooAppointmentSystem.Repositories
{
    public class GenericRepository<T> : IGeneric<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private string _errorMessage = string.Empty;
        private readonly bool _isDisposed;
        protected readonly ApplicationDBContext _dbContext;
        private IDbContextTransaction _objTran;
        private ResponseDto<T> _responseDto;

        public GenericRepository(ApplicationDBContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
            _responseDto = new ResponseDto<T>();
        }
        public ResponseDto<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (tracked)
                {
                    query = _dbSet;
                }
                else
                {
                    query = _dbSet.AsNoTracking();
                }
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                if (query.Count() > 0)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Data = query.FirstOrDefault();
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                    _responseDto.Data = null;
                }

            }
            catch (Exception ex)
            {

                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _responseDto;
        }
        public async Task<ResponseDto<T>> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (!tracked)
                {
                    query = query.AsNoTracking();
                }
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                var count = await query.CountAsync();
                if (count > 0)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Data =await query.FirstOrDefaultAsync();
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }

            }
            catch (Exception ex)
            {

                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _responseDto;
        }
        public ResponseDto<T> GetSingleOrDefault(string? includeProperties = null, bool tracked = false)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (!tracked)
                {
                    query = _dbSet.AsNoTracking();
                }
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                if (query.Count() > 0)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Data = query.SingleOrDefault();
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }
            }
            catch (Exception ex)
            {

                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _responseDto;
        }
        public async Task<ResponseDto<T>> GetSingleOrDefaultAsync(string? includeProperties = null, bool tracked = false)
        {
            try
            {
                IQueryable<T> query = _dbSet;

                if (!tracked)
                {
                    query = query.AsNoTracking();
                }

                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }

                var count = await query.CountAsync();
                if (count > 0)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Data = await query.SingleOrDefaultAsync();
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _responseDto;
        }
        public ResponseDto<T> GetAll(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false, string? includeProperties = null, bool tracked = false)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (!tracked)
                    query = _dbSet.AsNoTracking();
                if (filter != null)
                    query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                if (orderBy != null)
                {
                    query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
                }

                foreach (var item in query)
                {

                    PropertyInfo[] properties = item.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(item, HttpUtility.HtmlEncode(property.GetValue(item)));
                        }
                    }

                }

                if (query.Count() > 0)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Datas = query.ToList();
                }

                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }

            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message + ex.InnerException;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _responseDto;
        }
        public async Task<ResponseDto<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false, string? includeProperties = null, bool tracked = false)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (!tracked)
                    query = _dbSet.AsNoTracking();
                if (filter != null)
                    query = query.Where(filter);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                if (orderBy != null)
                {
                    query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);
                }

                foreach (var item in query)
                {

                    PropertyInfo[] properties = item.GetType().GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType == typeof(string))
                        {
                            property.SetValue(item, HttpUtility.HtmlEncode(property.GetValue(item)));
                        }
                    }

                }

                var count = await query.CountAsync();
                if (count > 0)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Datas = await query.ToListAsync();
                }

                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }

            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message + ex.InnerException;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }

            return _responseDto;
        }
        public ResponseDto<T> Insert(T entity)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
                _responseDto.Data = entity;
                _responseDto.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                _responseDto.Message = "Failed due to: " + ex.Message + "Inner Exception:" + ex.InnerException;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }
        public async Task<ResponseDto<T>> InsertAsync(T entity)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
                _responseDto.Data = entity;
                _responseDto.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _responseDto.Message = "Failed due to: " + ex.Message + "Inner Exception:" + ex.InnerException;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }
        public ResponseDto<T> Update(T entity)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbSet.Update(entity);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Data = entity;
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }
        public async Task<ResponseDto<T>> UpdateAsync(T entity)
        {
            try
            {
               await _dbContext.Database.BeginTransactionAsync();
               _dbSet.Update(entity);
               await _dbContext.SaveChangesAsync();
               await _dbContext.Database.CommitTransactionAsync();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Data = entity;
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }
        public ResponseDto<T> UpdateAll(List<T> entities)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbSet.UpdateRange(entities);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Datas = entities;
            }
            catch (Exception ex)
            {
                _dbContext.Database.RollbackTransaction();
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Datas = entities;
            }
            return _responseDto;
        }
        public async Task<ResponseDto<T>> UpdateAllAsync(List<T> entities)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                _dbSet.UpdateRange(entities);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Datas = entities;
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Datas = entities;
            }
            return _responseDto;
        }
        public ResponseDto<T> Delete(T entity)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Data = entity;


            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }
        public async Task<ResponseDto<T>> DeleteAsync(T entity)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                _dbSet.RemoveRange(entity);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Data = entity;


            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }
        public ResponseDto<T> DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                _dbContext.Database.BeginTransaction();
                _dbSet.RemoveRange(entities);
                _dbContext.SaveChanges();
                _dbContext.Database.CommitTransaction();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Datas = entities.ToList();

            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Datas = entities.ToList();
            }
            return _responseDto;
        }
        public async Task<ResponseDto<T>> DeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                await _dbContext.Database.BeginTransactionAsync();
                _dbSet.RemoveRange(entities);
                await _dbContext.SaveChangesAsync();
                await _dbContext.Database.CommitTransactionAsync();
                _responseDto.StatusCode = HttpStatusCode.OK;
                _responseDto.Datas = entities.ToList();
            }
            catch (Exception ex)
            {
                await _dbContext.Database.RollbackTransactionAsync();
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Datas = entities.ToList();
            }
            return _responseDto;
        }
        public ResponseDto<T> GetById(int id)
        {
            try
            {
                var item = _dbSet.Find(id);
                if (item != null)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Data = item;
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _responseDto;
        }
        public async Task<ResponseDto<T>> GetByIdAsync(int id)
        {
            try
            {
                var item = await _dbSet.FindAsync(id);
                if (item != null)
                {
                    _responseDto.StatusCode = HttpStatusCode.OK;
                    _responseDto.Data = item;
                }
                else
                {
                    _responseDto.StatusCode = HttpStatusCode.NotFound;
                    _responseDto.Message = "Not Found";
                }
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex.Message;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
            }
            return _responseDto;
        }

        
    }
}

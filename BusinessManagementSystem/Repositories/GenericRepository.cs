using BusinessManagementSystem.Data;
using BusinessManagementSystem.Dto;
using BusinessManagementSystem.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Web;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BusinessManagementSystem.Repositories
{
    public class GenericRepository<T> : IGeneric<T> where T : class
    {
        protected readonly DbSet<T> _dbSet;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        protected readonly ApplicationDBContext _dbContext;
        private ResponseDto<T> _responseDto;

        public GenericRepository(ApplicationDBContext context)
        {
            _dbContext = context;
            _dbSet = context.Set<T>();
            _responseDto = new ResponseDto<T>();
        }

        public async Task<ResponseDto<T>> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true)
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
                    _responseDto.Data = await query.FirstOrDefaultAsync();
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

        public async Task<ResponseDto<T>> GetSingleOrDefault(string? includeProperties = null, bool tracked = true)
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

        public async Task<ResponseDto<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = true)
        {
            try
            {
                IQueryable<T> query = _dbSet;
                if (filter != null)
                    query = query.Where(filter);



                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
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
                    _responseDto.Datas =await query.ToListAsync();
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

        public async Task<ResponseDto<T>> InsertAsync(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                await _dbContext.SaveChangesAsync();
                _responseDto.Data = entity;
                _responseDto.StatusCode = HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                _responseDto.Message = "Failed due to: " + ex;
                _responseDto.StatusCode = HttpStatusCode.InternalServerError;
                _responseDto.Data = entity;
            }
            return _responseDto;
        }

        public async Task<ResponseDto<T>> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
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
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
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

        public async Task<ResponseDto<T>> DeleteRange(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                await _dbContext.SaveChangesAsync();
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

        public async Task<ResponseDto<T>> GetByIdAsync(int? id)
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

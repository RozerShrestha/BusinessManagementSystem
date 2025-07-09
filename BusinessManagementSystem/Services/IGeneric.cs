using BusinessManagementSystem.Dto;
using System.Linq.Expressions;

namespace BusinessManagementSystem.Services
{
    public interface IGeneric<T> where T : class
    {
        ResponseDto<T> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        ResponseDto<T> GetSingleOrDefault(string? includeProperties = null, bool tracked = false);
        Task<ResponseDto<T>> GetSingleOrDefaultAsync(string? includeProperties = null, bool tracked = false);
        ResponseDto<T> GetAll(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false, string? includeProperties = null, bool tracked = false);
        Task<ResponseDto<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Expression<Func<T, object>>? orderBy = null, bool orderByDescending = false, string? includeProperties = null, bool tracked = false);
        ResponseDto<T> GetById(int id);
        ResponseDto<T> Insert(T Entity);
        ResponseDto<T> Update(T Entity);
        ResponseDto<T> UpdateAll(List<T> entities);
        ResponseDto<T> Delete(T entity);
        ResponseDto<T> DeleteRange(IEnumerable<T> entities);
    }
}
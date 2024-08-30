using BusinessManagementSystem.Dto;
using System.Linq.Expressions;

namespace BusinessManagementSystem.Services
{
    public interface IGeneric<T> where T : class
    {
        public Task<ResponseDto<T>> GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
        public Task<ResponseDto<T>> GetSingleOrDefault(string? includeProperties = null, bool tracked = true);
        public Task<ResponseDto<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked=true);
        public Task<ResponseDto<T>> GetByIdAsync(int? id);
        public Task<ResponseDto<T>> InsertAsync(T Entity);
        public Task<ResponseDto<T>> UpdateAsync(T Entity);
        public Task<ResponseDto<T>> DeleteAsync(T entity);
        public Task<ResponseDto<T>> DeleteRange(IEnumerable<T> entities);
    }
}

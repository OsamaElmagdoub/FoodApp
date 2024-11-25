using FoodApp.Data.Entities;
using FoodApp.Repository.Specification;
using System.Linq.Expressions;

namespace FoodApp.Repository.Interface
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteById(int id);
        Task<int> SaveChangesAsync();
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate);
        Task<IQueryable<T>> GetAsyncToInclude(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecification<T> Spec);
        Task<T?> GetByIdWithSpecAsync(ISpecification<T> Spec);
        Task<int> GetCountWithSpecAsync(ISpecification<T> Spec);
        Task<List<T>> ListAsync(ISpecification<T> spec);
    }
}

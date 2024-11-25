using FoodApp.Data.Entities;

namespace FoodApp.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task<int> SaveChangesAsync();

    }
}

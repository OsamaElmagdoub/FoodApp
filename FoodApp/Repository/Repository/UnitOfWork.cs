using FoodApp.Data.Entities;
using FoodApp.Repository.Interface;
using ProjectManagementSystem.Data.Context;
using System.Collections;

namespace FoodApp.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repository;
        private readonly ApplicationDBContext _dBContext;

        public UnitOfWork(
            ApplicationDBContext dBContext)
        {
            _dBContext = dBContext;
            _repository = new Hashtable();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            var key = typeof(T);

            if (!_repository.ContainsKey(key))
            {
                var value = new GenericRepository<T>(_dBContext);
                _repository.Add(key, value);
            }

            return (_repository[key] as IGenericRepository<T>)!;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dBContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _dBContext.Dispose();
        }
    }
}

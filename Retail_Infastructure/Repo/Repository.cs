using Microsoft.EntityFrameworkCore;
using Retail_Application.Interfaces;
using Retail_Infastructure.Context;

namespace Retail_Infastructure.Repo
{
    // vì IRepo này nó quản lý các model của database ,  nhưng các class con của nó sử dụng Entity (Domain) thế nên ta cần map từ Entity sang Model
    public class Repository<TEntity, TModel> : IRepository<TEntity> where TEntity : class where TModel : class
    {
        private readonly ManagementRetailContext _dbContext;    // database 

        public Repository(ManagementRetailContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
        }

        public Task UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found.");
            }
            _dbContext.Set<TEntity>().Remove(entity);
        }
    }
}
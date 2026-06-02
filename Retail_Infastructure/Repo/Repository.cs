using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Retail_Application.Interfaces;
using Retail_Infastructure.Context;

namespace Retail_Infastructure.Repo
{
    // vì IRepo này nó quản lý các model của database ,  nhưng các class con của nó sử dụng Entity (Domain) thế nên ta cần map từ Entity sang Model
    public class Repository<TEntity, TModel> : IRepository<TEntity> where TEntity : class where TModel : class
    {
        protected ManagementRetailContext _dbContext;    // database 
        protected readonly IMapper _mapper;

        public Repository(ManagementRetailContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var models = await _dbContext.Set<TModel>().ToListAsync();
            return _mapper.Map<IEnumerable<TEntity>>(models);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            var model = await _dbContext.Set<TModel>().FindAsync(id);
            return _mapper.Map<TEntity?>(model);
        }

        public async Task AddAsync(TEntity entity)
        {
            var model = _mapper.Map<TModel>(entity);
            await _dbContext.Set<TModel>().AddAsync(model);
        }

        public Task UpdateAsync(TEntity entity)
        {
            var model = _mapper.Map<TModel>(entity);
            _dbContext.Set<TModel>().Update(model);
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);

            if (entity == null)
            {
                throw new Exception($"Entity with id {id} not found.");
            }

            var model = _mapper.Map<TModel>(entity);
            _dbContext.Set<TModel>().Remove(model);
        }
    }
}
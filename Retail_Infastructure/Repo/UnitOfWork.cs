using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage;
using Retail.Application.Interfaces;
using Retail_Application.Interfaces;
using Retail_Infastructure.Context;
using Retail_Infastructure.Repo;

namespace Retail.Infastructure.Repo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ManagementRetailContext _dbContext;
        private readonly IMapper _mapper;


        public ICategoryRepository categoryRepository { get; private set; }

        public IProductRepository productRepository { get; private set; }

        public IStockTransactionRepository stockTransactionRepository { get; private set; }

        IDbContextTransaction _transaction;

        public UnitOfWork(ManagementRetailContext managementRetailContext, IMapper mapper)
        {
            _dbContext = managementRetailContext;
            _mapper = mapper;
            categoryRepository = new CategoryRepository(_dbContext, _mapper);
            productRepository = new ProductRepository(_dbContext, _mapper);
            stockTransactionRepository = new StockTransactionRepository(_dbContext, _mapper);
        }


        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                return;
            }
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();

        }
    }
}

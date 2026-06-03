using Retail.Application.Factories;
using Retail.Application.Interfaces;
using Retail_Domain.Entities;
using Retail_Application.Interfaces;

namespace Retail.Tests;

internal sealed class FakeUnitOfWork : IUnitOfWork
{
    public FakeCategoryRepository CategoryRepository { get; } = new();
    public FakeProductRepository ProductRepository { get; } = new();
    public FakeStockTransactionRepository StockTransactionRepository { get; } = new();

    public int SaveChangesCallCount { get; private set; }
    public int BeginTransactionCallCount { get; private set; }
    public int CommitTransactionCallCount { get; private set; }
    public int RollbackTransactionCallCount { get; private set; }

    public ICategoryRepository categoryRepository => CategoryRepository;
    public IProductRepository productRepository => ProductRepository;
    public IStockTransactionRepository stockTransactionRepository => StockTransactionRepository;

    public Task<int> SaveChangesAsync()
    {
        SaveChangesCallCount++;
        return Task.FromResult(1);
    }

    public Task BeginTransactionAsync()
    {
        BeginTransactionCallCount++;
        return Task.CompletedTask;
    }

    public Task CommitTransactionAsync()
    {
        CommitTransactionCallCount++;
        return Task.CompletedTask;
    }

    public Task RollbackTransactionAsync()
    {
        RollbackTransactionCallCount++;
        return Task.CompletedTask;
    }
}

internal sealed class FakeProductRepository : FakeRepository<ProductEntity>, IProductRepository
{
    public Task<IEnumerable<ProductEntity>> GetProductsByCategoryId(int id)
    {
        return Task.FromResult(Items.Where(product => product.CategoryId == id));
    }
}

internal sealed class FakeCategoryRepository : FakeRepository<CategoryEntity>, ICategoryRepository
{
}

internal sealed class FakeStockTransactionRepository : FakeRepository<StockTransactionEntity>, IStockTransactionRepository
{
}

internal class FakeRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    public List<TEntity> Items { get; } = new();
    public List<TEntity> UpdatedItems { get; } = new();
    public List<int> DeletedIds { get; } = new();

    public Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return Task.FromResult(Items.AsEnumerable());
    }

    public Task<TEntity?> GetByIdAsync(int id)
    {
        var property = typeof(TEntity).GetProperty("Id");
        var item = Items.FirstOrDefault(entity => property?.GetValue(entity) is int value && value == id);
        return Task.FromResult(item);
    }

    public Task AddAsync(TEntity entity)
    {
        Items.Add(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity)
    {
        UpdatedItems.Add(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        DeletedIds.Add(id);
        return Task.CompletedTask;
    }
}

internal sealed class FakeStockTransactionFactory : IStockTransactionFactory
{
    public StockTransactionEntity CreateImportTransaction(int productId, int quantity, string? note)
    {
        return new StockTransactionEntity
        {
            ProductId = productId,
            Quantity = quantity,
            Note = note,
            TransactionType = "Import",
            TransactionDate = new DateTime(2026, 6, 3)
        };
    }
}

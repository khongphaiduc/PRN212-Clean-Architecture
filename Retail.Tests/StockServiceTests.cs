using Retail.Infastructure.ServicesImpl;
using Retail_Domain.Entities;

namespace Retail.Tests;

public class StockServiceTests
{
    [Fact]
    public async Task ImportStockAsync_WhenQuantityIsInvalid_ThrowsAndDoesNotSave()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new StockService(unitOfWork, new FakeStockTransactionFactory());

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.ImportStockAsync(productId: 1, quantity: 0, note: "invalid"));

        Assert.Contains("Số lượng nhập phải lớn hơn 0", exception.Message);
        Assert.Equal(0, unitOfWork.SaveChangesCallCount);
        Assert.Empty(unitOfWork.StockTransactionRepository.Items);
    }

    [Fact]
    public async Task ImportStockAsync_WhenProductDoesNotExist_ThrowsAndDoesNotSave()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new StockService(unitOfWork, new FakeStockTransactionFactory());

        var exception = await Assert.ThrowsAsync<Exception>(
            () => service.ImportStockAsync(productId: 99, quantity: 5, note: null));

        Assert.Equal("Không tìm thấy sản phẩm.", exception.Message);
        Assert.Equal(0, unitOfWork.SaveChangesCallCount);
        Assert.Empty(unitOfWork.StockTransactionRepository.Items);
    }

    [Fact]
    public async Task ImportStockAsync_WhenValid_UpdatesQuantityCreatesTransactionAndSavesOnce()
    {
        var unitOfWork = new FakeUnitOfWork();
        var product = new ProductEntity(10, "Monitor", 2500000, 3, 1);
        unitOfWork.ProductRepository.Items.Add(product);
        var service = new StockService(unitOfWork, new FakeStockTransactionFactory());

        await service.ImportStockAsync(productId: 10, quantity: 4, note: "first import");

        Assert.Equal(7, product.Quantity);
        Assert.Single(unitOfWork.ProductRepository.UpdatedItems);

        var transaction = Assert.Single(unitOfWork.StockTransactionRepository.Items);
        Assert.Equal(10, transaction.ProductId);
        Assert.Equal(4, transaction.Quantity);
        Assert.Equal("first import", transaction.Note);
        Assert.Equal("Import", transaction.TransactionType);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
    }
}

using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Retail.Application.DTOs;
using Retail.Infastructure.Mappings;
using Retail.Infastructure.ServicesImpl;
using Retail_Domain.Entities;

namespace Retail.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task AddProductAsync_AddsMappedProductAndSavesChanges()
    {
        var unitOfWork = new FakeUnitOfWork();
        var service = new ProductService(CreateMapper(), unitOfWork);

        await service.AddProductAsync(new ProductDto
        {
            Name = "Keyboard",
            Price = 450000,
            Quantity = 7,
            CategoryId = 2
        });

        var product = Assert.Single(unitOfWork.ProductRepository.Items);
        Assert.Equal("Keyboard", product.Name);
        Assert.Equal(450000, product.Price);
        Assert.Equal(7, product.Quantity);
        Assert.Equal(2, product.CategoryId);
        Assert.Equal(1, unitOfWork.SaveChangesCallCount);
    }

    [Fact]
    public async Task GetAllProductsAsync_ReturnsMappedProducts()
    {
        var unitOfWork = new FakeUnitOfWork();
        unitOfWork.ProductRepository.Items.Add(new ProductEntity(1, "Mouse", 120000, 4, 3));
        var service = new ProductService(CreateMapper(), unitOfWork);

        var products = await service.GetAllProductsAsync();

        var product = Assert.Single(products);
        Assert.Equal(1, product.Id);
        Assert.Equal("Mouse", product.Name);
        Assert.Equal(120000, product.Price);
        Assert.Equal(4, product.Quantity);
        Assert.Equal(3, product.CategoryId);
    }

    private static IMapper CreateMapper()
    {
        var expression = new MapperConfigurationExpression();
        expression.AddProfile<ProdutProfile>();
        var config = new MapperConfiguration(expression, NullLoggerFactory.Instance);
        return config.CreateMapper();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Domain.Entities
{
    public class ProductEntity
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; } // Số lượng tồn kho hiện tại
        public int CategoryId { get; private set; }

        // Navigation properties
        public virtual CategoryEntity CategoryEntity { get; private set; } = null!;

        public readonly List<StockTransactionEntity> _stockTransactions = new();
        public virtual IReadOnlyCollection<StockTransactionEntity> StockTransactions => _stockTransactions.AsReadOnly();

        private ProductEntity() { }

        public ProductEntity(string name, decimal price, int categoryId, int initialQuantity = 0)
        {
            UpdateName(name);
            UpdatePrice(price);

            if (categoryId <= 0)
                throw new ArgumentException("Mã danh mục không hợp lệ.", nameof(categoryId));
            CategoryId = categoryId;

            if (initialQuantity < 0)
                throw new ArgumentException("Số lượng ban đầu không được âm.", nameof(initialQuantity));
            Quantity = initialQuantity;
        }

        public void UpdateName(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new ArgumentException("Tên sản phẩm không được để trống.", nameof(newName));
            Name = newName;
        }

        public void UpdatePrice(decimal newPrice)
        {
            if (newPrice < 0)
                throw new ArgumentException("Giá sản phẩm không được nhỏ hơn 0.", nameof(newPrice));
            Price = newPrice;
        }

        // Nghiệp vụ NHẬP KHO (Tăng số lượng và tự tạo Transaction)
        public void ReceiveStock(int quantity, string? note)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng nhập phải lớn hơn 0.", nameof(quantity));

            Quantity += quantity;
            _stockTransactions.Add(new StockTransactionEntity(Id, quantity, "Inbound", note));
        }

        // Nghiệp vụ XUẤT KHO (Giảm số lượng và tự tạo Transaction)
        public void ShipStock(int quantity, string? note)
        {
            if (quantity <= 0)
                throw new ArgumentException("Số lượng xuất phải lớn hơn 0.", nameof(quantity));

            if (Quantity < quantity)
                throw new InvalidOperationException("Số lượng hàng trong kho không đủ để xuất.");

            Quantity -= quantity;
            _stockTransactions.Add(new StockTransactionEntity(Id, quantity, "Outbound", note));
        }
    }
}

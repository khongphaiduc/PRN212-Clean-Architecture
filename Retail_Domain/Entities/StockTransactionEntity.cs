using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Domain.Entities
{
    public class StockTransactionEntity
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public string? Note { get; private set; }
        public string TransactionType { get; private set; } = null!; // "Inbound" hoặc "Outbound"

        public virtual ProductEntity Product { get; private set; } = null!;

        private StockTransactionEntity() { }

        // Constructor internal để đảm bảo chỉ có Product Entity (Aggregate Root) mới được tạo Transaction này
        internal StockTransactionEntity(int productId, int quantity, string transactionType, string? note)
        {
            ProductId = productId;
            Quantity = quantity;
            TransactionType = transactionType;
            Note = note;
            TransactionDate = DateTime.UtcNow; // Luôn lấy giờ chuẩn UTC ở tầng Domain
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Domain.Entities
{
    // Domain Entity không nên chứa navigation của EF.
    public class StockTransactionEntity
    {
        public int Id { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public string? Note { get; private set; }
        public string TransactionType { get; private set; } = null!; // "Inbound" hoặc "Outbound"
        private StockTransactionEntity() { }

        public StockTransactionEntity(int id, int productId, int quantity, DateTime transactionDate, string? note, string transactionType)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            TransactionDate = transactionDate;
            Note = note;
            TransactionType = transactionType;

        }

    }
}

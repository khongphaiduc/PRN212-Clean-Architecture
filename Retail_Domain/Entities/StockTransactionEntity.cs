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
        public int Id { get;  set; }
        public int ProductId { get;  set; }
        public int Quantity { get;  set; }
        public DateTime TransactionDate { get; set; }
        public string? Note { get; set; }
        public string TransactionType { get; set; } = null!; // "Inbound" hoặc "Outbound"
        public StockTransactionEntity() { }

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

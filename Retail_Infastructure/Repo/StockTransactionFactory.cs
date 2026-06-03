using Retail.Application.Factories;
using Retail_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.Repo
{
    public class StockTransactionFactory : IStockTransactionFactory
    {
        public StockTransactionEntity CreateImportTransaction(int productId, int quantity, string? note)
        {
            return new StockTransactionEntity
            {
                ProductId = productId,
                Quantity = quantity,
                Note = note,
                TransactionType = "Import",
                TransactionDate = DateTime.Now
            };
        }
    }
}

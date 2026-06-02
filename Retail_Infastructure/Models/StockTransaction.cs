using System;
using System.Collections.Generic;

namespace Retail_Infastructure.Models;

public partial class StockTransaction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? Note { get; set; }

    public string TransactionType { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}

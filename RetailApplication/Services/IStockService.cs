using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Application.Services
{
    public interface IStockService
    {
        Task ImportStockAsync(int productId, int quantity, string? note);
    }
}

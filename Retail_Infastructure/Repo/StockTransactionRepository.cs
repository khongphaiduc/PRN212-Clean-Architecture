using AutoMapper;
using Retail_Application.Interfaces;
using Retail_Domain.Entities;
using Retail_Infastructure.Context;
using Retail_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail_Infastructure.Repo
{
    public class StockTransactionRepository : Repository<StockTransactionEntity, StockTransaction>, IStockTransactionRepository
    {
        public StockTransactionRepository(ManagementRetailContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }
    }
}

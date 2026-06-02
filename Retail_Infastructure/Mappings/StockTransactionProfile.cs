using AutoMapper;
using Retail_Domain.Entities;
using Retail_Infastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.Mappings
{
    public class StockTransactionProfile : Profile
    {
        public StockTransactionProfile()
        {
            CreateMap<StockTransaction, StockTransactionEntity>()
                .ConstructUsing(s => new StockTransactionEntity(s.Id, s.ProductId, s.Quantity, s.TransactionDate, s.Note, s.TransactionType));
            CreateMap<StockTransactionEntity, StockTransaction>();

        }
    }
}

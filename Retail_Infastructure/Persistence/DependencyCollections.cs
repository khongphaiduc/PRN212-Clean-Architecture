using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retail.Application.Services;
using Retail.Infastructure.Mappings;
using Retail.Infastructure.ServicesImplement;
using Retail_Application.Interfaces;
using Retail_Infastructure.Context;
using Retail_Infastructure.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Retail.Infastructure.Persistence
{
    public static class DependencyCollections
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            Env.Load();

            services.AddDbContext<ManagementRetailContext>(options =>
                options.UseSqlServer(configuration["SQLConnectString"]));

            // Services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            //  AutoMapper 
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}

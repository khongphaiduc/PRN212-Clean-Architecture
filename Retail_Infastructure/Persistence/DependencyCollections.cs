using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retail.Application.Factories;
using Retail.Application.Interfaces;
using Retail.Application.Services;
using Retail.Infastructure.Mappings;
using Retail.Infastructure.Repo;
using Retail.Infastructure.ServicesImpl;
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
            Env.Load(Path.Combine(AppContext.BaseDirectory, "config", ".env"));

            services.AddDbContext<ManagementRetailContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("SQLConnectString")));

            // Services
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IStockTransactionRepository, StockTransactionRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IStockTransactionFactory, StockTransactionFactory>();




            //  AutoMapper 
            services.AddAutoMapper(cfg => { }, AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }

    }
}

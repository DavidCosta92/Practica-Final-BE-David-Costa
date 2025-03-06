using FinalProjectBakary.Domain.Entities;
using FinalProjectBakary.Persistence;
using FinalProjectBakary.Persistence.Repositories;
using FinalProjectBakary.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectBakary
{
    public static class Startup
    {
        public static IServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString = config.GetConnectionString("DefaultConnection");

            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));
            
            serviceCollection.AddTransient<OrderRepository>();
            serviceCollection.AddTransient<OfficeRepository>();
            serviceCollection.AddTransient<BreadRepository>();
            serviceCollection.AddTransient<MenuRepository>();
            serviceCollection.AddTransient<OfficeManager>();
            serviceCollection.AddTransient<ConsoleManager>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}

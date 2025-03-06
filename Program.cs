using FinalProjectBakary.Domain.Entities;
using FinalProjectBakary.Persistence;
using FinalProjectBakary;
using FinalProjectBakary.View;
using Microsoft.Extensions.DependencyInjection;
using FinalProjectBakary.Persistence.Repositories;



var serviceProvider = Startup.ConfigureServices();
using var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
var consoleManager = serviceProvider.GetRequiredService<ConsoleManager>();
consoleManager.Init();



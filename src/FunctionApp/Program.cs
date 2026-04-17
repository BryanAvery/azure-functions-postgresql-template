using FunctionApp.Application.Interfaces;
using FunctionApp.Application.Services;
using FunctionApp.Configuration;
using FunctionApp.Infrastructure.Database;
using FunctionApp.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration((_, config) =>
    {
        config
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
            .AddJsonFile("local.settings.json", optional: true, reloadOnChange: false)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.Configure<PostgresOptions>(context.Configuration.GetSection(PostgresOptions.SectionName));

        services.AddScoped<IDbConnectionFactory, NpgsqlConnectionFactory>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<ICustomerService, CustomerService>();
    })
    .Build();

host.Run();

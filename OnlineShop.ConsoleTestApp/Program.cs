// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShop.Library.Options;

Console.WriteLine("Hello, World!");

var builder = new HostBuilder()
    .ConfigureServices((hostContaxt, services) =>
    {
        var configurationBuilder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false);

        IConfiguration configuration = configurationBuilder.Build();

        services.Configure<IdentityServerApiOptions>(configuration.GetSection(IdentityServerApiOptions.SectionName));
        services.Configure<ServiceAddressOptions>(configuration.GetSection(ServiceAddressOptions.SectionName));
    })
    .ConfigureLogging(logging =>
    {
        logging.AddConsole();
        logging.SetMinimumLevel(LogLevel.Information);
    })
    .UseConsoleLifetime();

var host = builder.Build();

using(var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occured: {ex.Message}");
    }
}

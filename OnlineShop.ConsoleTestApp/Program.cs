using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OnlineShop.ConsoleTestApp;
using OnlineShop.Library.Clients.IdentityServer;
using OnlineShop.Library.Clients.UserManagmentService;
using OnlineShop.Library.Options;

var builder = new HostBuilder()
    .ConfigureServices((hostContaxt, services) =>
    {
        services.AddTransient<AuthenticationServiceTest>();
        services.AddHttpClient<UsersClient>();
        services.AddHttpClient<RolesClient>();
        services.AddHttpClient<IdentityServerClient>();

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
        var service = services.GetRequiredService<AuthenticationServiceTest>();

        string usersResult = await service.RunUsersClientTests(Environment.GetCommandLineArgs());
        string rolesResult = await service.RunRolesClientTests(Environment.GetCommandLineArgs());

        Console.WriteLine(usersResult);
        Console.WriteLine(rolesResult);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occured: {ex.Message}");
    }
}

Console.ReadKey();

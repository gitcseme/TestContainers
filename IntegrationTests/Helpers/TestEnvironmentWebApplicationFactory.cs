using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;
using WebApi.EFConfig;

namespace IntegrationTests.Helpers;

public class TestEnvironmentWebApplicationFactory 
    : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly MsSqlContainer _container = new MsSqlBuilder()
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .WithEnvironment("ACCEPT_EULA", "Y")
            .WithPassword("Strong_password_123!")
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    //.UntilPortIsAvailable(1433)
                    .UntilCommandIsCompleted("/opt/mssql-tools18/bin/sqlcmd", "-C", "-Q", "SELECT 1;")
            )
            .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var dbContextServiceDescriptor = services.SingleOrDefault(s => 
                    s.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

            if (dbContextServiceDescriptor is not null)
            {
                // Remove the actual dbcontext registration.
                services.Remove(dbContextServiceDescriptor);
            }

            // Register dbcontext with container connection string
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                string containerConnectionString = _container.GetConnectionString();
                options.UseSqlServer(containerConnectionString);
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();
    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _container.StopAsync();
    }
}

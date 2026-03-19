using GymErp.Common;
using GymErp.Domain.Subscriptions.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GymErp.Tenant;

public class SubscriptionsDbContextFactory(
    IConfiguration configuration,
    IServiceBus serviceBus)
    : IEfDbContextFactory<SubscriptionsDbContext>
{
    public async Task<SubscriptionsDbContext> CriarAsync(string codigoTenant)
    {
        var postgresTenantStringConnection = new PostgresTenantStringConnection(configuration);
        var connectionString = postgresTenantStringConnection.GetTenantsConfigStringConnection();
        var options = new DbContextOptionsBuilder<SubscriptionsDbContext>()
            .UseNpgsql(connectionString)
            .Options;
        var context = new SubscriptionsDbContext(options, serviceBus);

        // Garanti schema inicial (workshop/dev). Sem migrations, a primeira escrita falha sem a tabela.
        await context.Database.EnsureCreatedAsync();
        return context;
    }
}

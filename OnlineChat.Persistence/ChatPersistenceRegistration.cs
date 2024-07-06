using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineChat.Persistence;

public static class ChatPersistenceRegistration
{
    private const string _connectionStringName = "OnlineChat";

    public static void AddOnlineChatPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(_connectionStringName) ??
            throw new AggregateException($"Connection string: {_connectionStringName} is not found");

        services.AddDbContext<OnlineChatDbContext>(options =>
        {
            options.UseSqlServer(
                connectionString,
                sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsHistoryTable(
                        OnlineChatDbContext.OnlineChatDbMigrationHistory,
                        OnlineChatDbContext.OnlineChatDbSchema
                        );
                });
        });
    }
}

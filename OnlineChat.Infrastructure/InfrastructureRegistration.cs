using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineChat.Core.Common;
using OnlineChat.Core.Domain.Groups.Common;
using OnlineChat.Core.Domain.Messages.Common;
using OnlineChat.Core.Domain.Users.Common;
using OnlineChat.Infrastructure.Core.Common;
using OnlineChat.Infrastructure.Core.Domain.Groups;
using OnlineChat.Infrastructure.Core.Domain.Messages;
using OnlineChat.Infrastructure.Core.Domain.Users;
using System.Reflection;


namespace OnlineChat.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        //SignalR
        services.AddSignalRCore();

        //mediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        //repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IUserGroupRepository, UserGroupRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        //checkers
        services.AddScoped<IGroupMustExistChecker, GroupMustExistChecker>();
        services.AddScoped<IUserMustExistChecker, UserMustExistChecker>();
    }
}

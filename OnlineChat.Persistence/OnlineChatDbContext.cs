using Microsoft.EntityFrameworkCore;
using OnlineChat.Core.Domain.Users.Models;
using OnlineChat.Core.Domain.Groups.Models;
using OnlineChat.Core.Domain.Messages.Models;
using OnlineChat.Persistence.EntityConfigurations;

namespace OnlineChat.Persistence;

public class OnlineChatDbContext(DbContextOptions<OnlineChatDbContext> options) : DbContext(options)
{
    internal const string OnlineChatDbSchema = "online-chatdb";
    internal const string OnlineChatDbMigrationHistory = "_ChatDbMigrationHistory";

    public DbSet<Group> Groups { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserGroup> UserGroup {  get; set; }

    public DbSet<Message> Messages { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(OnlineChatDbSchema);
        modelBuilder.ApplyConfiguration(new GroupEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserGroupEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
    }
}
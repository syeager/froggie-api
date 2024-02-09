using Froggie.Data.Accounts;
using Froggie.Data.Groups;
using Froggie.Data.Tasks;
using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.EntityFramework.Identity;

namespace Froggie.Data;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
internal sealed class FroggieDb(DbContextOptions<FroggieDb> options)
    : DomainContext<FroggieDb, Account>(options)
{
    public new DbSet<User> Users { get; init; } = null!;
    public DbSet<Account> Accounts => base.Users;
    public DbSet<Task> Tasks { get; init; } = null!;
    public DbSet<Group> Groups { get; init; } = null!;
    public DbSet<GroupUser> GroupUsers { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GroupUser>().Property(ugm => ugm.UserId).HasConversion<IdValueConverter<User>>();
        builder.Entity<GroupUser>().Property(ugm => ugm.GroupId).HasConversion<IdValueConverter<Group>>();

        builder.IdEntity<Task>();
        builder.IdEntity<Group>();
        builder.IdEntity<User>();
        builder.Entity<Account>().HasOne(a => a.User);

        builder.ApplyConfiguration(new TaskEntityConfiguration());
        builder.ApplyConfiguration(new GroupEntityConfiguration());
        builder.ApplyConfiguration(new UserEntityConfiguration());
    }
}

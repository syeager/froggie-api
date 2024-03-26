using Froggie.Data.Groups;
using Froggie.Data.Tasks;
using Froggie.Data.Users;
using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Domain;

namespace Froggie.Data;

internal sealed class FroggieDb(DbContextOptions<FroggieDb> options)
    : Database<FroggieDb>(options), IDomainContext
{
    public DbSet<User> Users { get; init; } = null!;
    public DbSet<Task> Tasks { get; init; } = null!;
    public DbSet<Group> Groups { get; init; } = null!;
    public DbSet<GroupUser> GroupUsers { get; init; } = null!;
    public DbSet<TaskAssignee> TaskAssignees { get; init; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<GroupUser>().Property(ugm => ugm.UserId).HasConversion<IdValueConverter<User>>();
        builder.Entity<GroupUser>().Property(ugm => ugm.GroupId).HasConversion<IdValueConverter<Group>>();

        builder.Entity<TaskAssignee>().Property(ta => ta.TaskId).HasConversion<IdValueConverter<Task>>();
        builder.Entity<TaskAssignee>().Property(ta => ta.UserId).HasConversion<IdValueConverter<User>>();

        builder.IdEntity<Task>();
        builder.IdEntity<Group>();
        builder.IdEntity<User>();

        builder.ApplyConfiguration(new TaskEntityConfiguration());
        builder.ApplyConfiguration(new GroupEntityConfiguration());
        builder.ApplyConfiguration(new UserEntityConfiguration());
    }
}

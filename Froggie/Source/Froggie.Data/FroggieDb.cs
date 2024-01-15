using Froggie.Data.Groups;
using Froggie.Data.Tasks;
using Froggie.Data.Users;
using LittleByte.AutoMapper.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace Froggie.Data;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
internal sealed class FroggieDb : DomainContext<FroggieDb, UserDao, IdentityRole<Guid>>
{
    public DbSet<TaskDao> Tasks { get; init; } = null!;
    public DbSet<GroupDao> Groups { get; init; } = null!;
    public DbSet<UserGroupMap> UserGroupMaps { get; init; } = null!;

    public FroggieDb(IMapper mapper, DbContextOptions<FroggieDb> options)
        : base(mapper, options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserGroupMap>().HasKey(ugm => new {ugm.UserId, ugm.GroupId});
    }
}
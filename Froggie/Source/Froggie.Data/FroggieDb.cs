using AutoMapper;
using Froggie.Data.Tasks.Models;
using Froggie.Data.Users.Models;
using LittleByte.Common.Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Data;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
internal sealed class FroggieDb : DomainContext<FroggieDb, UserDao, IdentityRole<Guid>>
{
    public DbSet<TaskDao> Tasks { get; init; } = null!;

    public FroggieDb(IMapper mapper, DbContextOptions<FroggieDb> options)
        : base(mapper, options) { }
}
using AutoMapper;
using Froggie.Data.Tasks.Models;
using LittleByte.Infra.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Froggie.Data;

internal sealed class FroggieDb : DomainContext<FroggieDb, IdentityUser<Guid>, IdentityRole<Guid>>
{
    public DbSet<TaskDao> Tasks { get; set; } = null!;

    public FroggieDb(IMapper mapper, DbContextOptions<FroggieDb> options)
        : base(mapper, options)
    {
    }
}
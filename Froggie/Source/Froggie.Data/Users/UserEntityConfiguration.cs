using Froggie.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Froggie.Data.Users;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> configuration)
    {
        configuration.Property(t => t.Name).HasConversion(n => n.Value, s => new UserName(s));
    }
}
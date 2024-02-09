using Froggie.Domain.Groups;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Froggie.Data.Groups;

internal sealed class GroupEntityConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> configuration)
    {
        configuration.Property(t => t.Name).HasConversion(n => n.Value, s => new GroupName(s));
        configuration.HasMany(g => g.Users).WithMany()
            .UsingEntity<GroupUser>
            (
                l => l.HasOne(ug => ug.User).WithMany(),
                r => r.HasOne(ug => ug.Group).WithMany()
            );
    }
}
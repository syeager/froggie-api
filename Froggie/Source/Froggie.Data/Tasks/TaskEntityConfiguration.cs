using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Froggie.Data.Tasks;

internal sealed class TaskEntityConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> configuration)
    {
        configuration.Property(t => t.Title).HasConversion(t => t.Value, s => new Title(s));
        configuration.Property(t => t.CreatorId).HasConversion<IdValueConverter<User>>();
        configuration.Property(t => t.DueDate);
        configuration.Property(t => t.GroupId).HasConversion<IdValueConverter<Group>>();
    }
}
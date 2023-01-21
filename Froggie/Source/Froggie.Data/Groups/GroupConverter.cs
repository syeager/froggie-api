using AutoMapper;
using Froggie.Domain.Groups;

namespace Froggie.Data.Groups;

internal sealed class GroupConverter : ITypeConverter<GroupDao, Group>
{
    private readonly IGroupFactory groupFactory;

    public GroupConverter(IGroupFactory groupFactory)
    {
        this.groupFactory = groupFactory;
    }

    public Group Convert(GroupDao source, Group destination, ResolutionContext context) => groupFactory.Create(source.Id, source.Name);
}
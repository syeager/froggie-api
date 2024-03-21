using Froggie.Domain.Groups;
using LittleByte.Common;

namespace Froggie.Test;

public static class ValidGroup
{
    public static readonly GroupName Name = new(new string('a', GroupNameRules.LengthMin));

    public static Group New() => Group.Create(
        new Id<Group>(),
        Name,
        []);
}
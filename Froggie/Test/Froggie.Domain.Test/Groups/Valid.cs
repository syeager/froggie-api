using Froggie.Domain.Groups;
using LittleByte.Common;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Groups
    {
        public static readonly GroupName Name = new(new string('a', GroupNameRules.LengthMin));

        public static Group New()
        {
            return Group.Create(
                new Id<Group>(),
                Name,
                []);
        }
    }
}
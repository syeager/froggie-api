using Froggie.Domain.Groups;
using LittleByte.Common;
using LittleByte.Validation.Test;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Groups
    {
        public static readonly Name Name = new(new string('a', NameRules.LengthMin));

        public static Group New()
        {
            return Group.Create(Validator.WillPass<Group>(),
                new Id<Group>(),
                Name);
        }
    }
}
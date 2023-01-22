using Froggie.Domain.Groups;
using LittleByte.Common.Domain;
using LittleByte.Test.Validation;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Groups
    {
        public static Name Name = new(new string('a', NameRules.LengthMin));

        public static Group New()
        {
            return Group.Create(Validator.WillPass<Group>(),
                new Id<Group>(),
                Name);
        }
    }
}
using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Test;

public static class ValidUser
{
    public static readonly UserName Name = new(new string('a', UserNameRules.LengthMin));
    public static readonly UserName Name2 = new(new string('b', UserNameRules.LengthMin));

    public static User New() => New(Name);

    public static User New(string name) => User.Create(new Id<User>(), new UserName(name));
}
namespace Froggie.Domain.Groups;

public sealed class UserAlreadyInGroup() : Result(false, "User is already in this group.");
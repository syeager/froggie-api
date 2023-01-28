using Froggie.Domain.Users;

namespace Froggie.Domain.Groups;

public sealed class UserNotInGroupException : Exception
{
    public Id<User> UserId { get; }
    public Id<Group> GroupId { get; }

    public UserNotInGroupException(Id<User> userId, Id<Group> groupId)
    : base("User not found in group")
    {
        UserId = userId;
        GroupId = groupId;
    }
}
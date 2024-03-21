using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Test;

public static class ValidTask
{
    public static readonly DateTimeOffset DueDate = DateTimeOffset.MaxValue;

    public static readonly Title Title = new(new string('a', TitleRules.LengthMin));

    public static Task New(Guid creatorId, Guid groupId) =>
        Task.Create(new Id<Task>(), Title, new Id<User>(creatorId), DueDate, new Id<Group>(groupId));

    public static IReadOnlyList<Task> New(int count, Guid creatorId, Guid groupId)
    {
        var tasks = new List<Task>();
        tasks.Init(count, i => Task.Create(new Id<Task>(), new Title($"{Title}-{i}"),
            new Id<User>(creatorId), DueDate, new Id<Group>(groupId)));
        return tasks;
    }
}
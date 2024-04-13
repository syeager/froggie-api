using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common;

namespace Froggie.Test;

public static class ValidTask
{
    public static readonly DateTimeOffset DueDate = DateTimeOffset.MaxValue;

    public static readonly Title Title = new(new string('a', TitleRules.LengthMin));

    public static Task New(Guid? id = null, string? title = null, Guid? creatorId = null,
                           DateTimeOffset? dueDate = null, Guid? groupId = null, bool? isCompleted = null) =>
        Task.Create(
            id?.ToId<Task>() ?? new Id<Task>(),
            title is null ? Title : new Title(title),
            creatorId?.ToId<User>() ?? new Id<User>(),
            dueDate ?? DueDate,
            groupId?.ToId<Group>() ?? new Id<Group>(),
            isCompleted ?? false
        );

    public static Task New(Guid creatorId, Guid groupId, string? title = null) =>
        Task.Create(new Id<Task>(), title is null ? Title : new Title(title), new Id<User>(creatorId), DueDate,
            new Id<Group>(groupId), false);

    public static IReadOnlyList<Task> New(int count, Guid creatorId, Guid groupId)
    {
        var tasks = new List<Task>();
        tasks.Init(count, i => Task.Create(new Id<Task>(), new Title($"{Title}-{i}"),
            new Id<User>(creatorId), DueDate, new Id<Group>(groupId), false));
        return tasks;
    }

    public static IReadOnlyList<Task> New(params string[] titles)
    {
        var tasks = new List<Task>();
        tasks.Init(titles.Length, i => New(Guid.NewGuid(), Guid.NewGuid(), titles[i]));
        return tasks;
    }
}
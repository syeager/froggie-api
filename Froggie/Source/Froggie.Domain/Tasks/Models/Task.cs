using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    public Id<User> CreatorId { get; }
    public DateTimeOffset DueDate { get; }
    public Id<Group> GroupId { get; }
    public Title Title { get; }

    private Task(Id<Task> id, Title title, Id<User> creatorId, DateTimeOffset dueDate, Id<Group> groupId)
        : base(id)
    {
        Title = title;
        CreatorId = creatorId;
        DueDate = dueDate;
        GroupId = groupId;
    }

    internal static Task Create(Id<Task> id,
                                Title title,
                                Id<User> creatorId,
                                DateTimeOffset dueDate,
                                Id<Group> groupId)
    {
        var task = new Task(id, title, creatorId, dueDate, groupId);
        var validator = new TaskValidator();
        validator.SignOrThrow(task);

        return task;
    }
}
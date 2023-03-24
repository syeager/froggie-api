using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    public Title Title { get; }
    public Id<User> CreatorId { get; }
    public DateTime DueDate { get; }
    public Id<Group> GroupId { get; }

    private Task(Id<Task> id, Title title, Id<User> creatorId, DateTime dueDate, Id<Group> groupId)
        : base(id)
    {
        Title = title;
        CreatorId = creatorId;
        DueDate = dueDate;
        GroupId = groupId;
    }

    internal static Task Create(IModelValidator<Task> validator,
                                Id<Task> id,
                                Title title,
                                Id<User> creatorId,
                                DateTime dueDate,
                                Id<Group> groupId)
    {
        var task = new Task(id, title, creatorId, dueDate, groupId);
        validator.SignOrThrow(task);

        return task;
    }
}
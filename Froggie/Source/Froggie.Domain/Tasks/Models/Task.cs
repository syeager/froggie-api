using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    public Title Title { get; }
    public Id<User> CreatorId { get; }
    public DateTime DueDate { get; }

    private Task(Id<Task> id, Title title, Id<User> creatorId, DateTime dueDate)
        : base(id)
    {
        Title = title;
        CreatorId = creatorId;
        DueDate = dueDate;
    }

    internal static Task Create(IModelValidator<Task> validator,
                                Id<Task> id,
                                Title title,
                                Id<User> creatorId,
                                DateTime dueDate)
    {
        var task = new Task(id, title, creatorId, dueDate);
        validator.SignOrThrow(task);

        return task;
    }
}
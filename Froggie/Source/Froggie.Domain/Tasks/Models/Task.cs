using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    public Title Title { get; }
    public Id<User> CreatorId { get; }

    private Task(Id<Task> id, Title title, Id<User> creatorId)
        : base(id)
    {
        Title = title;
        CreatorId = creatorId;
    }

    internal static Task Create(ModelValidator<Task> validator, Id<Task> id, Title title, Id<User> creatorId)
    {
        var task = new Task(id, title, creatorId);
        validator.SignOrThrow(task);

        return task;
    }
}
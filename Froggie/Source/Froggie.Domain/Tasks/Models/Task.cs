using Froggie.Domain.Groups;
using Froggie.Domain.Users;

namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    private readonly List<DomainModel<User>> assignees;
    public IReadOnlyCollection<DomainModel<User>> Assignees => assignees;

    public Id<User> CreatorId { get; }
    public DateTimeOffset DueDate { get; }
    public Id<Group> GroupId { get; }
    public Title Title { get; }

    private Task(Id<Task> id,
                 Title title,
                 Id<User> creatorId,
                 DateTimeOffset dueDate,
                 Id<Group> groupId)
        : base(id)
    {
        Title = title;
        CreatorId = creatorId;
        DueDate = dueDate;
        GroupId = groupId;
        assignees = [];
    }

    internal static Task Create(Id<Task> id,
                                Title title,
                                Id<User> creatorId,
                                DateTimeOffset dueDate,
                                Id<Group> groupId,
                                IEnumerable<DomainModel<User>>? assignees = null)
    {
        var task = new Task(id, title, creatorId, dueDate, groupId);
        task.assignees.AddRange(assignees ?? []);
        var validator = new TaskValidator();
        validator.SignOrThrow(task);

        return task;
    }

    internal void AddAssignee(DomainModel<User> assignee)
    {
        var log = this.NewLogger();
        log
            .Push("User.Id", assignee)
            .Info("Adding assignee to task");

        if(!assignees.Contains(assignee))
        {
            assignees.Add(assignee);
        }
        else
        {
            log.Debug("User is already assigned to this task");
        }
    }
}
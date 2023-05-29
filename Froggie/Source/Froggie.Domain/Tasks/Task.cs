using Froggie.Domain.Groups;
using Froggie.Domain.Users;
using LittleByte.Validation;

namespace Froggie.Domain.Tasks;

public sealed class Task : DomainModel<Task>
{
    private readonly List<Id<User>> assignees;

    public Title Title { get; }
    public Id<User> CreatorId { get; }
    public DateTimeOffset DueDate { get; }
    public Id<Group> GroupId { get; }
    public IReadOnlyCollection<Id<User>> Assignees => assignees;

    private Task(Id<Task> id, Title title, Id<User> creatorId, DateTimeOffset dueDate, Id<Group> groupId, List<Id<User>> assignees)
        : base(id)
    {
        Title = title;
        CreatorId = creatorId;
        DueDate = dueDate;
        GroupId = groupId;
        this.assignees = assignees;
    }

    internal static Task Create(IModelValidator<Task> validator,
                                Id<Task> id,
                                Title title,
                                Id<User> creatorId,
                                DateTimeOffset dueDate,
                                Id<Group> groupId,
                                IEnumerable<Id<User>>? assignees = null)
    {
        var assigneeList = assignees != null ? new List<Id<User>>(assignees) : new();
        var task = new Task(id, title, creatorId, dueDate, groupId, assigneeList);
        validator.SignOrThrow(task);

        return task;
    }

    internal void AddAssignee(Id<User> assignee)
    {
        var log = this.NewLogger();

        if(assignee == Id<User>.Empty)
        {
            throw new ArgumentNullException(nameof(assignee), "Can't assign empty user to task");
        }

        log
            .Push(assignee)
            .Info("Adding assignee to task");

        if(!assignees.Contains(assignee))
        {
            assignees.Add(assignee);
        }
        else
        {
            log.Info("User is already assigned to this task");
        }
    }
}
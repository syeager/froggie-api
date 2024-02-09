namespace Froggie.Domain.Tasks;

public sealed class UserNeedsToBeInGroupToCreateTask()
    : Result<Task>(false, "User can't create a task in a group they are not a member of");
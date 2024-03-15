namespace Froggie.Domain.Tasks;

public sealed class UserNeedsToBeInTaskGroupToBeAssignee(): Result<Task>(false, "User can't be assigned to a task in a group they are not a member of");
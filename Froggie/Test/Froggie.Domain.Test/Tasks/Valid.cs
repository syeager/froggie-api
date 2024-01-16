using Froggie.Domain.Groups;
using Froggie.Domain.Tasks;
using Froggie.Domain.Users;
using LittleByte.Common;
using LittleByte.Validation;
using LittleByte.Validation.Test;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Tasks
    {
        public static readonly DateTimeOffset DueDate = DateTimeOffset.MaxValue;

        public static readonly Title Title = new(new string('a', TitleRules.LengthMin));
        private static readonly IModelValidator<Task> validator = Validator.WillPass<Task>();

        public static Task New(Guid creatorId, Guid groupId) =>
            Task.Create(validator, new Id<Task>(), Title, new Id<User>(creatorId), DueDate, new Id<Group>(groupId));

        public static IReadOnlyList<Task> New(int count, Guid creatorId, Guid groupId)
        {
            var tasks = new List<Task>();
            tasks.Init(count, i => Task.Create(validator, new Id<Task>(), new Title($"{Title}-{i}"),
                new Id<User>(creatorId), DueDate, new Id<Group>(groupId)));
            return tasks;
        }
    }
}
using Froggie.Domain.Tasks;
using LittleByte.Common.Domain;
using LittleByte.Common.Extensions;
using LittleByte.Common.Validation;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Tasks
    {
        public static readonly Title Title = new(new string('a', TitleRules.LengthMin));
        public static readonly DateTime DueDate = DateTime.MaxValue;

        public static Task New(Guid creatorId) =>
            Task.Create(new SuccessModelValidator<Task>(), new Id<Task>(), Title, creatorId, DueDate);

        public static IReadOnlyList<Task> New(int count, Guid creatorId)
        {
            return new List<Task>().Init(count,
                i => Task.Create(new SuccessModelValidator<Task>(), new Id<Task>(), new Title($"{Title}-{i}"),
                    creatorId, DueDate));
        }
    }
}
using Froggie.Domain.Tasks;
using LittleByte.Common.Domain;
using Task = Froggie.Domain.Tasks.Task;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Tasks
    {
        public static readonly Title Title = new(new string('a', TitleRules.LengthMin));

        public static Task New()
        {
            return Task.Create(new Id<Task>(), Title);
        }
    }
}
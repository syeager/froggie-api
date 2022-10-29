using Froggie.Domain.Tasks.Validators;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Task
    {
        public static readonly string Title = new('a', TitleRules.LengthMin);
    }
}
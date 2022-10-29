using Froggie.Domain.Tasks;

// ReSharper disable once CheckNamespace
namespace Froggie.Domain.Test;

public static partial class Valid
{
    public static class Tasks
    {
        public static readonly string Title = new('a', TitleRules.LengthMin);
    }
}
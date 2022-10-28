using Froggie.Domain.Tasks.Validators;

namespace Froggie.Domain.Test.Tasks;

public static class Valid
{
    public static class Task
    {
        public static readonly string Title = new('a', TitleRules.LengthMin);
    }
}
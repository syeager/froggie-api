using FluentValidation;
using FluentValidation.Validators;
using LittleByte.Common;

namespace Froggie.Domain.Groups;

internal sealed class NameValidator<T> : PropertyValidator<T, Name>
{
    public override string Name => nameof(NameValidator<X>);

    public override bool IsValid(ValidationContext<T> context, Name value) =>
        value.Value.Length is>= NameRules.LengthMin and<= NameRules.LengthMax;
}

public static class NameRules
{
    public const int LengthMax = 25;
    public const int LengthMin = 5;
}
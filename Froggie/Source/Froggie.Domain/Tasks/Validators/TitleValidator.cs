using FluentValidation;
using FluentValidation.Validators;
using Froggie.Domain.Tasks.Models;
using LittleByte.Common;

namespace Froggie.Domain.Tasks.Validators;

[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
[ExcludeFromCodeCoverage]
internal static class TitleValidatorExtension
{
    public static IRuleBuilderOptions<T, Title> IsTitle<T>(this IRuleBuilder<T, Title> @this)
    {
        return @this.SetValidator(new TitleValidator<T>());
    }
}

internal class TitleValidator<T> : PropertyValidator<T, Title>
{
    public override string Name => nameof(TitleValidator<X>);

    public override bool IsValid(ValidationContext<T> context, Title value)
    {
        return value.Value.Length is>= TitleRules.LengthMin and<= TitleRules.LengthMax;
    }
}

public static class TitleRules
{
    public const int LengthMax = 100;
    public const int LengthMin = 1;
}
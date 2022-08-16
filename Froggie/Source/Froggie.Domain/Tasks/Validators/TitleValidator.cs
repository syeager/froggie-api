using FluentValidation;
using FluentValidation.Validators;
using Froggie.Domain.Tasks.Models;
using LittleByte.Core.Common;

namespace Froggie.Domain.Tasks.Validators;

internal static class TitleValidatorExtension
{
    public static IRuleBuilderOptions<T, Title> IsTitle<T>(this IRuleBuilder<T, Title> @this) =>
        @this.SetValidator(new TitleValidator<T>());
}

internal class TitleValidator<T> : PropertyValidator<T, Title>
{
    public override string Name => nameof(TitleValidator<X>);
 
    public override bool IsValid(ValidationContext<T> context, Title value) => value.Value.Length is >= TitleRules.LengthMin and <= TitleRules.LengthMax;
}

internal static class TitleRules
{
    public const int LengthMin = 1;
    public const int LengthMax = 100;
}

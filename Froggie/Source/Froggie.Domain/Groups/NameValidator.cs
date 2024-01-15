using FluentValidation;
using FluentValidation.Validators;

namespace Froggie.Domain.Groups;

[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
[ExcludeFromCodeCoverage]
internal static class NameValidatorExtension
{
    public static IRuleBuilderOptions<T, Name> IsName<T>(this IRuleBuilder<T, Name> @this) =>
        @this.SetValidator(new NameValidator<T>());
}

internal sealed class NameValidator<T> : PropertyValidator<T, Name>
{
    public override string Name => nameof(NameValidator<T>);

    public override bool IsValid(ValidationContext<T> context, Name value) =>
        value.Value.Length is>= NameRules.LengthMin and<= NameRules.LengthMax;
}

public static class NameRules
{
    public const string PersonalName = "Personal";
    public const int LengthMax = 25;
    public const int LengthMin = 5;
}
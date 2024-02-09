using FluentValidation;
using FluentValidation.Validators;

namespace Froggie.Domain.Users;

[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
[ExcludeFromCodeCoverage]
internal static class NameValidatorExtension
{
    public static IRuleBuilderOptions<T, UserName> IsName<T>(this IRuleBuilder<T, UserName> @this) =>
        @this.SetValidator(new NameValidator<T>());
}

internal sealed class NameValidator<T> : PropertyValidator<T, UserName>
{
    public override string Name => nameof(NameValidator<T>);

    public override bool IsValid(ValidationContext<T> context, UserName value) =>
        value.Value.Length is>= UserNameRules.LengthMin and<= UserNameRules.LengthMax;
}

public static class UserNameRules
{
    public const int LengthMax = 25;
    public const int LengthMin = 3;
}
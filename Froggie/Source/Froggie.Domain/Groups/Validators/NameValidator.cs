using FluentValidation;
using FluentValidation.Validators;

namespace Froggie.Domain.Groups;

[SuppressMessage("ReSharper", "UnusedMethodReturnValue.Global")]
[ExcludeFromCodeCoverage]
internal static class NameValidatorExtension
{
    public static IRuleBuilderOptions<T, GroupName> IsName<T>(this IRuleBuilder<T, GroupName> @this) =>
        @this.SetValidator(new NameValidator<T>());
}

internal sealed class NameValidator<T> : PropertyValidator<T, GroupName>
{
    public override string Name => nameof(NameValidator<T>);

    public override bool IsValid(ValidationContext<T> context, GroupName value) =>
        value.Value.Length is>= GroupNameRules.LengthMin and<= GroupNameRules.LengthMax;
}

public static class GroupNameRules
{
    public const int LengthMax = 25;
    public const int LengthMin = 5;
    public static readonly GroupName PersonalName = new("Personal");
}
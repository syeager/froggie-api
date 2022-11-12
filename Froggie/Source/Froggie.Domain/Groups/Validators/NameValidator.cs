using FluentValidation;
using FluentValidation.Validators;
using LittleByte.Common;

namespace Froggie.Domain.Groups;

internal sealed class NameValidator<T> : PropertyValidator<T, Name>
{
    public override string Name => nameof(NameValidator<X>);

    public override bool IsValid(ValidationContext<T> context, Name value) => true;

}
using FluentValidation;
using FluentValidation.Validators;
using Froggie.Domain.Groups.Models;
using LittleByte.Common;

namespace Froggie.Domain.Groups.Validators;

internal sealed class NameValidator<T> : PropertyValidator<T, Name>
{
    public override string Name => nameof(NameValidator<X>);

    public override bool IsValid(ValidationContext<T> context, Name value) => throw new NotImplementedException();

}
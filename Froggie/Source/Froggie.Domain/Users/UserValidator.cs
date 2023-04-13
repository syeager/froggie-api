using FluentValidation;

namespace Froggie.Domain.Users;

public sealed class UserValidator : ModelValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Id).IsNotEmpty();
        RuleFor(u => u.Email).Must(email => email.Value.Contains('@'));
        RuleFor(u => u.Name);
    }
}
using LittleByte.Validation;

namespace Froggie.Domain.Users;

public sealed class UserValidator : ModelValidator<User>
{
    public UserValidator()
    {
        RuleFor(u => u.Id).IsNotEmpty();
        RuleFor(u => u.Name).IsName();
    }
}
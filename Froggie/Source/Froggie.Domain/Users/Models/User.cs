﻿using Froggie.Domain.Users.Validators;

namespace Froggie.Domain.Users.Models;

public sealed class User : DomainModel<User>
{
    public Email Email { get; }
    public Name Name { get; }

    private User(Id<User> id, Email email, Name name)
        : base(id)
    {
        Email = email;
        Name = name;
    }

    public static User Create(Id<User> id, Email email, Name name)
    {
        var user = new User(id, email, name);
        var validator = new UserValidator();
        validator.SignOrThrow(user);
        return user;
    }
}
﻿using System.Net;
using Froggie.Api.Users;
using Froggie.Data;
using Froggie.Domain;
using Froggie.Domain.Test;
using LittleByte.Test.Categories;
using Microsoft.Extensions.DependencyInjection;

namespace Froggie.Api.Test.Integration.Users;

public sealed class RegisterUserTest : IntegrationTest
{
    protected override void AddServices(IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
            .AddDomain()
            .AddPersistence()
            .AddTransient<CreateUserController>();
    }

    [Test]
    public async ValueTask RegisterNewUser()
    {
        var controller = services.GetRequiredService<CreateUserController>();
        var request = new CreateUserRequest
        {
            Email = Valid.Users.Email.Value,
            Name = Valid.Users.Name.Value,
            Password = Valid.Users.Password.Value
        };

        var response = await controller.Create(request);

        Assert.AreEqual((int)HttpStatusCode.OK, response.StatusCode);
        Assert.NotNull(response.Obj);
        Assert.IsFalse(response.IsError);
    }
}
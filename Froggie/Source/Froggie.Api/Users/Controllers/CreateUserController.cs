using System.Net;
using AutoMapper;
using Froggie.Api.Users.Requests;
using Froggie.Api.Users.Responses;
using Froggie.Domain.Users.Models;
using Froggie.Domain.Users.Services;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Users.Controllers;

public sealed class CreateUserController : UserController
{
    private readonly ILogInService logInService;
    private readonly IMapper mapper;
    private readonly IUserRegisterService registerService;
    private readonly ISaveContextCommand saveCommand;

    public CreateUserController(ILogInService logInService,
                                IUserRegisterService registerService,
                                IMapper mapper,
                                ISaveContextCommand saveCommand)
    {
        this.logInService = logInService;
        this.registerService = registerService;
        this.mapper = mapper;
        this.saveCommand = saveCommand;
    }

    // TODO: Should have RegisterResponse.
    [HttpPost(Routes.Create)]
    [ResponseType(HttpStatusCode.OK, typeof(LogInResponse))]
    public async ValueTask<ApiResponse<LogInResponse>> Create(CreateUserRequest request)
    {
        var email = new Email(request.Email);
        var name = new Name(request.Name);
        var password = new Password(request.Password);

        await registerService.RegisterAsync(email, name, password);

        var logInResult = await logInService.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await saveCommand.CommitChangesAsync();

        return new OkResponse<LogInResponse>(response);
    }
}
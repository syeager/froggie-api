using System.Net;
using AutoMapper;
using Froggie.Domain.Users;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Users;

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
        var user = await registerService.RegisterAsync(request.Email, request.Name, request.Password);

        // TODO: There has to be a better way to get the password. Maybe RegisterAsync should return a RegisterResult?
        var password = new Password(request.Password);
        
        var logInResult = await logInService.LogInAsync(user.Email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await saveCommand.CommitChangesAsync();

        return new OkResponse<LogInResponse>(response);
    }
}
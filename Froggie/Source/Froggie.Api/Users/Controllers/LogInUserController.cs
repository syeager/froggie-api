using System.Net;
using AutoMapper;
using Froggie.Domain.Users;
using LittleByte.Common.AspNet.Responses;
using LittleByte.Common.Infra.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Froggie.Api.Users;

public sealed class LogInUserController : UserController
{
    private readonly ILogInService logInService;
    private readonly IMapper mapper;
    private readonly ISaveContextCommand saveCommand;

    public LogInUserController(ILogInService logInService,
                               IMapper mapper,
                               ISaveContextCommand saveCommand)
    {
        this.logInService = logInService;
        this.mapper = mapper;
        this.saveCommand = saveCommand;
    }

    [HttpPost(Routes.LogIn)]
    [ResponseType(HttpStatusCode.OK, typeof(LogInResponse))]
    public async ValueTask<ApiResponse<LogInResponse>> Create(LogInUserRequest request)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password);

        var logInResult = await logInService.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await saveCommand.CommitChangesAsync();

        return new OkResponse<LogInResponse>(response);
    }
}
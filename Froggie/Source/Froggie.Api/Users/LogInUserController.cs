using Froggie.Domain.Users;
using LittleByte.Common.Infra.Commands;

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
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<LogInResponse>> LogIn(LogInUserRequest request)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password);

        var logInResult = await logInService.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await saveCommand.CommitChangesAsync();

        return response.Succeeded
            ? new OkResponse<LogInResponse>(response)
            : new BadRequestResponse<LogInResponse>(response);
    }
}
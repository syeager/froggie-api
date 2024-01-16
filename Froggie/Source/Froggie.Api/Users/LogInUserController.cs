using Froggie.Domain.Users;
using LittleByte.AspNet;
using LittleByte.EntityFramework;

namespace Froggie.Api.Users;

public sealed class LogInUserController(
    ILogInService inService,
    IMapper mapper,
    ISaveContextCommand command)
    : UserController
{
    [HttpPost(Routes.LogIn)]
    [ResponseType(HttpStatusCode.OK, typeof(LogInResponse))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<LogInResponse>> LogIn(LogInUserRequest request)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password);

        var logInResult = await inService.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await command.CommitChangesAsync();

        return response.Succeeded
            ? new OkResponse<LogInResponse>(response)
            : new BadRequestResponse<LogInResponse>(response);
    }
}
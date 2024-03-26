using Froggie.Accounts;
using LittleByte.EntityFramework;

namespace Froggie.Api.Accounts;

public sealed class LogInUserController(
    ILogInService logIn,
    IMapper mapper,
    ISaveContextCommand command)
    : AccountController
{
    [HttpPost(Routes.LogIn)]
    [ResponseType(HttpStatusCode.OK, typeof(LogInResponse))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<LogInResponse>> LogIn(LogInUserRequest request)
    {
        var email = new Email(request.Email);
        var password = new Password(request.Password);

        var logInResult = await logIn.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await command.CommitChangesAsync();

        return response.Succeeded
            ? new OkResponse<LogInResponse>(response)
            : new BadRequestResponse<LogInResponse>(response);
    }
}
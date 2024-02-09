using Froggie.Data.Accounts;
using LittleByte.EntityFramework;

namespace Froggie.Api.Accounts;

public sealed class CreateAccountController(
    ILogInService logInService,
    IAccountRegisterService registerService,
    IMapper mapper,
    ISaveContextCommand command)
    : AccountController
{
    // TODO: Should have RegisterResponse.
    [HttpPost(Routes.Create)]
    [ResponseType(HttpStatusCode.OK, typeof(LogInResponse))]
    [ResponseType(HttpStatusCode.BadRequest)]
    public async ValueTask<ApiResponse<LogInResponse>> Create(CreateAccountRequest request)
    {
        var user = await registerService.RegisterAsync(request.Email, request.Name, request.Password);

        // TODO: There has to be a better way to get the password. Maybe RegisterAsync should return a RegisterResult?
        var password = new Password(request.Password);
        var email = new Email(request.Email);
        var logInResult = await logInService.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        await command.CommitChangesAsync();

        // TODO: Should be CreatedResponse.
        return new OkResponse<LogInResponse>(response);
    }
}
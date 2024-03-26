using Froggie.Accounts;
using Froggie.Api.Users;
using LittleByte.EntityFramework;

namespace Froggie.Api.Accounts;

public sealed class CreateAccountController(
    ILogInService logInService,
    IRegisterUserService registerUser,
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
        var registerResult = await registerUser.RegisterAsync(request.Email, request.Name, request.Password);

        var password = new Password(request.Password);
        var email = new Email(request.Email);
        var logInResult = await logInService.LogInAsync(email, password);
        var response = mapper.Map<LogInResponse>(logInResult);

        // TODO: Improve this so 2 LogInResponses don't have to be made.
        response = new LogInResponse
        {
            AccessToken = response.AccessToken,
            Errors = response.Errors,
            Succeeded = response.Succeeded,
            User = mapper.Map<UserDto>(registerResult.Value!.User),
        };

        await command.CommitChangesAsync();

        // TODO: Should be CreatedResponse.
        return new OkResponse<LogInResponse>(response);
    }
}
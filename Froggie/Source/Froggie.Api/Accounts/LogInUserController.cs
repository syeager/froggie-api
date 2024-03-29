using Froggie.Accounts;
using Froggie.Api.Users;
using Froggie.Domain.Users;
using LittleByte.Domain;

namespace Froggie.Api.Accounts;

public sealed class LogInUserController(
    ILogInService logIn,
    IMapper mapper,
    IFindByIdQuery<User> findUser)
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
        if(logInResult.Succeeded is false)
        {
            return new BadRequestResponse<LogInResponse>(response);
        }

        var user = await findUser.FindRequiredAsync(logInResult.Account!.Id.ToId<User>());
        response = new LogInResponse
        {
            AccessToken = response.AccessToken,
            Errors = response.Errors,
            Succeeded = response.Succeeded,
            User = mapper.Map<UserDto>(user),
        };

        return new OkResponse<LogInResponse>(response);
    }
}